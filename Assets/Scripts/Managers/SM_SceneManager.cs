using System;
using System.Collections;
using Installer;
using Systems;
using Systems.EventHub;
using Systems.EventSignals;
using Table;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Managers
{
    public class SM_SceneManager : MonoBehaviour, ISM_ManagerBase
    {
        [Inject] private SignalBus _signalBus;
        
        [Inject] public void Construct(SignalBus signalBus)
        {
            signalBus.Subscribe<Signal_InitializeManagers>(x => InitManager(x.EventHub));
            _signalBus = signalBus;
            SM_GameManager.Instance?.RegisterManager(ESM_Manager.SceneManager, this);
        }
        public void InitManager(SM_ManagerEventHub eventHub)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        
        public void DestroyManager()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        
        public void LoadGameScene()
        {
            SceneManager.LoadScene("BootScene");
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SM_Log.INFO($"씬 로드 완료 : {scene.name}");
            _signalBus.Fire(new SceneLoadedSignal(scene, mode));
        }

        public string GetCurrentScene()
        {
            return SceneManager.GetActiveScene().name;
        }

        // Scene Change
        public void RequestSceneChange(SM_SceneCommand command)
        {
            switch (command.TransitionStyle)
            {
                case ESM_TransitionType.Direct:
                {
                    // 1. UI Clear
                    UnloadScene(command.ClearPolicy);
                
                    // 2. Diract Show UI
                    GameObject nextScene = GetNextScene(command.NextSceneName);
            
                    if (!nextScene)
                    {
                        SM_Log.ASSERT(false, $"There is No Next Scene(UI). ({command.NextSceneName}). Please Check UITable");
                    }
                    SM_UIManager uiManager = (SM_UIManager)SM_GameManager.Instance.GetManager(ESM_Manager.UIManager);
                    uiManager.PushUI(nextScene);
                    break;
                }
                case ESM_TransitionType.Fade:
                case ESM_TransitionType.OnlyFadeIn:
                {
                    StartCoroutine(HandleSceneTransition(command));
                    break;
                }
                default:
                    break;
            }
        }
        private IEnumerator HandleSceneTransition(SM_SceneCommand command)
        {
            if (command.TransitionStyle == ESM_TransitionType.Fade)
            {
                // 1. Make Loading Scene
                GameObject loadingScene = null;
                yield return MakeLoadingScene(command, loadingScene);
            
                // 2. Start FadeOut
                yield return StartFadeOut(loadingScene, command.FadeDuration);
            
                // 3. Process intermediate course
                GameObject nextScene = null;
                yield return WaitForLoading(command, scene => nextScene = scene);
            
                // 4. Start FadeIn
                yield return StartFadeIn(nextScene, command.FadeDuration);  
            
                // 5. Destroy LoadingScene
                yield return UnloadLoading();
            
                // 6. Complete Event
                command.OnTransitionComplete?.Invoke();   
            }
            else if (command.TransitionStyle == ESM_TransitionType.OnlyFadeIn)
            {
                GameObject nextScene =  MakeNextScene(command);
                if (nextScene)
                {
                    yield return null;
                }
                
                yield return StartFadeIn(nextScene, command.FadeDuration);  
                yield return UnloadLoading();
                command.OnTransitionComplete?.Invoke();  
            }
        }
        private IEnumerator MakeLoadingScene(SM_SceneCommand command, GameObject loadingUI)
        {
            loadingUI = SM_SystemLibrary.GetLoadingUI(command.LoadingType);
            if (loadingUI == null)
            {
                SM_Log.WARNING($"There is no UI ({command.LoadingType.ToString()})");
                yield break;
            }
            
            SM_UIManager uiManager = (SM_UIManager)SM_GameManager.Instance.GetManager(ESM_Manager.UIManager);
            uiManager?.PushUI(loadingUI);

            yield return null;
        }

        private IEnumerator StartFadeOut(GameObject loadingUI, float fadeDuration)
        {
            if (loadingUI.TryGetComponent<SM_SceneFader>(out var loadingFader))
            {
                yield return loadingFader.FadeIn(fadeDuration);
            }
        }
        
        private IEnumerator WaitForLoading(SM_SceneCommand command, Action<GameObject> onLoaded)
        {
            SM_TableManager tableManager = (SM_TableManager)SM_GameManager.Instance.GetManager(ESM_Manager.TableManager);
            
            float minLoadingTime = tableManager ? tableManager.GetParameter<float>(ESM_CommonType.LOADING_TIME) : 2f;
            float timer = 0f;
            bool unloadFinished = false;
            
            // 1. 씬 언로드 시작
            yield return StartCoroutine(UnloadScene(command.ClearPolicy));

            // 2. 다음 씬 로드 시작
            GameObject nextScene = MakeNextScene(command);
            onLoaded?.Invoke(nextScene);
            yield return null;

            // 3. Mode 바인딩 to Scene
            yield return StartCoroutine(MakeMode(command.NextSceneName, () => unloadFinished = true));
            
            // 4. 최소 로딩시간 보장.
            while (timer < minLoadingTime)
            {
                timer += Time.unscaledDeltaTime;
                yield return null;
            }

            // 5. 아직 로드할게 남았다면 기다림
            while (!unloadFinished)
            {
                yield return null;
            }
        }
        private GameObject MakeNextScene(SM_SceneCommand command)
        {
            GameObject nextScene = GetNextScene(command.NextSceneName);
            
            if (!nextScene)
            {
                SM_Log.ASSERT(false, $"There is No Next Scene(UI). ({command.NextSceneName}). Please Check UITable");
                return null;
            }
            SM_UIManager uiManager = (SM_UIManager)SM_GameManager.Instance.GetManager(ESM_Manager.UIManager);
            uiManager.PushUI(nextScene);
            
            return nextScene;
        }

        private IEnumerator MakeMode(string nextSceneName, Action onComplete = null)
        {
            SM_ModeManager modeManager = (SM_ModeManager)SM_GameManager.Instance.GetManager(ESM_Manager.ModeManager);
            if(!modeManager)
            {
                SM_Log.ASSERT(false, "[ModeManager] is not exist!! ");
                yield break;
            }

            modeManager.ClearMode();
            modeManager.RegisterMode(nextSceneName);
            onComplete?.Invoke();
            yield return null;
        }
        
        private IEnumerator StartFadeIn(GameObject nextScene, float fadeDuration)
        {
            if (nextScene.TryGetComponent<SM_SceneFader>(out var nextFader))
            {
                yield return nextFader.FadeIn(fadeDuration); // UI 페이드인
            }
        }
        private IEnumerator UnloadScene(ESM_UIClearPolicy clearPolicy)
        {
            SM_UIManager uiManager = (SM_UIManager)SM_GameManager.Instance.GetManager(ESM_Manager.UIManager);
            if (!uiManager)
            {
                yield break;
            }
            
            uiManager.PopUI(clearPolicy);
            yield return null;
        }
        
        private GameObject GetNextScene(string commandNextSceneName)
        {
            SM_UIManager uiManager = (SM_UIManager)SM_GameManager.Instance.GetManager(ESM_Manager.UIManager);
            SM_TableManager tableManager = (SM_TableManager)SM_GameManager.Instance.GetManager(ESM_Manager.TableManager);
            if (!uiManager || !tableManager)
            {
                SM_Log.ASSERT(false, "UIManager or TableManager are not exist!!");
                return null;
            }

            SM_UI_DataTable uiTable = (SM_UI_DataTable)tableManager.GetTable(ESM_TableType.UI);
            if (!uiTable)
            {
                SM_Log.ASSERT(false, "UI Table is not exist!!");
            }
            
            return uiTable.GetUI(commandNextSceneName);
        }

        private IEnumerator UnloadLoading()
        {
            SM_UIManager uiManager = (SM_UIManager)SM_GameManager.Instance.GetManager(ESM_Manager.UIManager);
            if (uiManager == null)
            {
                SM_Log.ASSERT(false, "UIManager is not exist!!");
                yield break;
            }
            
            uiManager.PopUI(ESM_UIClearPolicy.ClearTopOnly);
        }
    }
}