using System;
using System.Collections.Generic;
using Installer;
using Systems;
using Systems.EventHub;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Managers
{
    public enum ESM_UIType
    {
        Regular,
        Loading,
        Modal,
        Popup,
    }

    public enum ESM_UIClearPolicy
    {
        ClearAllExceptLoading,
        ClearTopOnly,
        DoNotClear
    }
    
    public class SM_UIManager : MonoBehaviour, ISM_ManagerBase
    {
        [SerializeField] private GameObject hudPrefab;
        private GameObject currentHUD;
        [SerializeField] private Transform canvasTransform;

        private Stack<GameObject> _uiStack = new();
        
        [Inject]
        private void Construct()
        {
            SM_GameManager.Instance?.RegisterManager(ESM_Manager.UIManager, this);
            canvasTransform = GameObject.FindWithTag("Canvas")?.transform;
        }

        [Inject] public void Construct(SignalBus signalBus)
        {
            signalBus.Subscribe<Signal_InitializeManagers>(x => InitManager(x.EventHub));
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
            ui.transform.SetParent(GameObject.FindWithTag("Canvas")?.transform, false);
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
                case ESM_UIClearPolicy.ClearAllExceptLoading:
                {
                    GameObject loadingUI = _uiStack.Pop();

                    while (_uiStack.Count > 0)
                    {
                        GameObject ui = _uiStack.Pop();
                        Destroy(ui);
                    }
                    
                    _uiStack.Clear();
                    _uiStack.Push(loadingUI);
                    break;
                }
                case ESM_UIClearPolicy.ClearTopOnly:
                {
                    if (_uiStack.Count < 2)
                    {
                        return;
                    }
                    
                    GameObject loadingUI = _uiStack.Pop();
                    GameObject topScene = _uiStack.Pop();
                    Destroy(topScene);
                    _uiStack.Push(loadingUI);
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