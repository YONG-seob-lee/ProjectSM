using System.Collections.Generic;
using Installer;
using Systems.EventHub;
using UI;
using UnityEngine;
using Zenject;

// 싱글플레이 / 탑뷰 카메라 / 하나의 플레이어만 존재
// PC 플랫폼(steam) / 콘텐츠 확장 예정
// 대부분의 오브젝트는 적 또는 투사체
// 씬 구조는 고정 또는 적은 수의 변환

namespace Managers
{
    public class SM_GameManager : MonoBehaviour
    {
        public static SM_GameManager Instance { get; private set; }

        // SingleBus
        [Inject] private SignalBus _signalBus;
        
        // EventHub
        [Inject] private SM_ManagerEventHub _eventHub;
        
        // Managers
        private Dictionary<ESM_Manager, MonoBehaviour> _managers = new Dictionary<ESM_Manager, MonoBehaviour>();
        [Inject]
        private void Construct(SignalBus signalBus)
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            _signalBus = signalBus;
        }
        
        

        private void Start()
        {
            Start_Internal();

            //DebugFade();
            
            StartLogo();
        }

        private void DebugFade()
        {
            if (GetManager(ESM_Manager.UIManager) is SM_UIManager uiManager)
            {
                GameObject instance = Instantiate(Resources.Load<GameObject>(SM_FadeDebugPanel.GetPath()));
                uiManager.PushUI(instance);
            }
        }

        public void RegisterManager(ESM_Manager key, MonoBehaviour instance)
        {
            _managers[key] = instance;
        }
        public MonoBehaviour GetManager(ESM_Manager eManager)
        {
            return _managers.TryGetValue(eManager, out MonoBehaviour manager) ? manager : null;
        }
        
        private void Start_Internal()
        {
            #if !UNITY_EDITOR
            _signalBus.Fire(new Signal_FirebaseReady());
            #endif
            _signalBus.Fire(new Signal_InitializeManagers(_eventHub));
    
            if(GetManager(ESM_Manager.SceneManager) is SM_SceneManager sceneManager)
            {
                if (sceneManager.GetCurrentScene() != "BootScene")
                {
                    sceneManager.LoadGameScene();
                }
            }
        }

        private void StartLogo()
        {
            if(GetManager(ESM_Manager.SceneManager) is SM_SceneManager sceneManager)
            {
                var command = new SM_SceneCommand(
                    next: SM_LogoPanel.GetName(),
                    transitionStyle: ESM_TransitionType.Direct,
                    clearPolicy: ESM_UIClearPolicy.ClearAll);
                
                sceneManager.RequestSceneChange(command);
            }
        }
    }   
}