using System.Collections.Generic;
using Systems;
using Systems.EventHub;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Managers
{
    public enum ESM_UIClearPolicy
    {
        DoNotClear,
        ClearAllExceptTop,
        ClearOnlySecond,
        ClearAllExceptLoadingUI,
        ClearAll,
    }
    
    public class SM_UIManager : MonoBehaviour, ISM_ManagerBase
    {
        [SerializeField] private GameObject hudPrefab;
        private GameObject currentHUD;
        [SerializeField] private Transform canvasTransform;

        private Stack<GameObject> _uiStack = new();
        
        [Inject] public void Construct(SignalBus signalBus)
        {
            signalBus.Subscribe<Signal_InitializeManagers>(x => InitManager(x.EventHub));
            // ReSharper disable once Unity.NoNullPropagation
            SM_GameManager.Instance?.RegisterManager(ESM_Manager.UIManager, this);
            canvasTransform = GameObject.FindWithTag("Canvas")?.transform;
        }
        
        public void InitManager(SM_ManagerEventHub eventHub)
        {
            eventHub.OnSceneChanged += OnStartScene;
        }

        public void DestroyManager()
        {
            
        }

        private void OnStartScene(Scene scene, LoadSceneMode mode)
        {
            SM_Log.INFO("Start First Scene");
            
            ShowHUD();
        }
        private void ShowHUD()
        {
            if (currentHUD == null && hudPrefab != null && canvasTransform != null)
            {
                currentHUD = Instantiate(hudPrefab, canvasTransform);
            }
            else if (currentHUD != null)
            {
                currentHUD.SetActive(true);
            }
        }

        public void PushUI(GameObject ui)
        {
            var canvas = GameObject.FindWithTag("Canvas")?.transform;
            ui.transform.SetParent(canvas, false);
            ui.transform.SetAsLastSibling();
            ui.SetActive(true);
            _uiStack.Push(ui);
        }

        public void PopUI(ESM_UIClearPolicy clearPolicy)
        {
            if (_uiStack.Count == 0)
            {
                return;
            }
            
            switch (clearPolicy)
            {
                case ESM_UIClearPolicy.ClearAllExceptTop:
                {
                    GameObject topUI = _uiStack.Pop();
                    
                    while (_uiStack.Count > 0)
                    {
                        GameObject ui = _uiStack.Pop();
                        Destroy(ui);
                    }
                    _uiStack.Clear();

                    if (topUI != null)
                    {
                        _uiStack.Push(topUI);
                    }
                    break;
                }
                case ESM_UIClearPolicy.ClearAllExceptLoadingUI:
                {
                    GameObject loadingUI = null;
                    
                    while (_uiStack.Count > 0)
                    {
                        GameObject ui = _uiStack.Pop();
                        if (ui.CompareTag("LoadingUI"))
                        {
                            loadingUI = ui;
                            continue;
                        }
                        Destroy(ui);
                    }
                    _uiStack.Clear();

                    if (loadingUI)
                    {
                        _uiStack.Push(loadingUI);
                    }
                    break;
                }
                case ESM_UIClearPolicy.ClearOnlySecond:
                {
                    if (_uiStack.Count < 2)
                    {
                        return;
                    }
                    
                    GameObject topUI = _uiStack.Pop();
                    GameObject secondUI = _uiStack.Pop();
                    Destroy(secondUI);

                    if (topUI != null)
                    {
                        _uiStack.Push(topUI);
                    }
                    break;
                }
                case ESM_UIClearPolicy.ClearAll:
                {
                    while (_uiStack.Count > 0)
                    {
                        GameObject ui = _uiStack.Pop();
                        Destroy(ui);
                    }
                    _uiStack.Clear();
                    break;
                }
                default:
                {
                    break;
                }
            }
        }
    }
}