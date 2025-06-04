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
        }
        [Inject]
        private void Construct()
        {
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
            StartCoroutine(HandleSceneTransition(command));
        }

        private IEnumerator HandleSceneTransition(SM_SceneCommand command)
        {
            yield return StartFadeOut(command);
            yield return WaitForLoading(command);
            yield return StartFadeIn(command);
            yield return UnloadLoading();
            command.OnTransitionComplete?.Invoke();
        }

        private IEnumerator StartFadeOut(SM_SceneCommand command)
        {
            GameObject loadingUI = SM_SystemLibrary.GetLoadingUI(command.LoadingType);
            if (loadingUI == null)
            {
                SM_Log.WARNING($"There is no UI ({command.LoadingType.ToString()})");
            }
            
            SM_UIManager uiManager = (SM_UIManager)SM_GameManager.Instance.GetManager(ESM_Manager.UIManager);
            uiManager?.PushUI(loadingUI);
            
            if (loadingUI.TryGetComponent<SM_SceneFader>(out var loadingFader))
            {
                yield return loadingFader.FadeIn(command.FadeDuration);
            }
        }
        
        private IEnumerator WaitForLoading(SM_SceneCommand command)
        {
            SM_TableManager tableManager = (SM_TableManager)SM_GameManager.Instance.GetManager(ESM_Manager.TableManager);
            
            float minLoadingTime = tableManager ? tableManager.GetParameter(ESM_CommonType.LOADING_TIME) : 2f;
            float timer = 0f;
            bool unloadFinished = false;
            
            // 1. 씬 언로드 시작
            yield return StartCoroutine(UnloadScene(command.ClearPolicy, () => unloadFinished = true));

            // 2. 최소 로딩시간 보장.
            while (timer < minLoadingTime)
            {
                timer += Time.unscaledDeltaTime;
                yield return null;
            }

            // 3. 아직 로드할게 남았다면 기다림
            while (!unloadFinished)
            {
                yield return null;
            }
        }
        
        private IEnumerator UnloadScene(ESM_UIClearPolicy clearPolicy, Action onComplete)
        {
            SM_UIManager uiManager = (SM_UIManager)SM_GameManager.Instance.GetManager(ESM_Manager.UIManager);
            if (!uiManager)
            {
                yield break;
            }
            
            uiManager.PopUI(clearPolicy);
            onComplete?.Invoke();
            yield return null;
        }

        private IEnumerator StartFadeIn(SM_SceneCommand command)
        {
            SM_UIManager uiManager = (SM_UIManager)SM_GameManager.Instance.GetManager(ESM_Manager.UIManager);
            SM_TableManager tableManager = (SM_TableManager)SM_GameManager.Instance.GetManager(ESM_Manager.TableManager);
            if (!uiManager || !tableManager)
            {
                SM_Log.ASSERT(false, "UIManager or TableManager are not exist!!");
                yield break;
            }

            SM_UI_DataTable uiTable = (SM_UI_DataTable)tableManager.GetTable(ESM_TableType.UI);
            if (!uiTable)
            {
                SM_Log.ASSERT(false, "UI Table is not exist!!");
            }
            
            GameObject nextScene = uiTable.GetUI(command.NextSceneName);
            if (!nextScene)
            {
                SM_Log.ASSERT(false, $"There is No Next Scene(UI). ({command.NextSceneName}). Please Check UITable");
            }
            uiManager.PushUI(nextScene);
            
            if (nextScene.TryGetComponent<SM_SceneFader>(out var nextFader))
            {
                yield return nextFader.FadeIn(command.FadeDuration); // UI 페이드인
            }
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
        
        private IEnumerator PlayTransitionEffect(SM_SceneCommand command, bool isOut)
        {
            switch (command.TransitionStyle)
            {
                case ESM_TransitionType.Fade:
                {
                    var fader = FindObjectOfType<SM_SceneFader>();
                    bool done = false;
                    if (fader)
                    {
                        if (isOut)
                        {
                            yield return fader.FadeIn(command.FadeDuration);
                        }
                        else
                        {
                            yield return fader.FadeOut(command.FadeDuration);
                        }
                    }

                    while (!done)
                    {
                        yield return null;
                    }
                    break;    
                }
                default:
                    break;
            }
        }
    }
}