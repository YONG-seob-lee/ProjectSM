using Managers;
using Systems.Controller;
using Systems.EventHub;
using Systems.EventSignals;
using Systems.Server;
using UnityEngine;
using Zenject;

namespace Installer
{
    public class SM_GameInstaller : MonoInstaller
    {
        // Client
        [SerializeField] private SM_GameManager gameManager;
        [SerializeField] private SM_PlayerController playerController;
        
        [SerializeField] private SM_SceneManager sceneManager;
        [SerializeField] private SM_UIManager uiManager;
        [SerializeField] private SM_TableManager tableManager;
        [SerializeField] private SM_InputManager inputManager;
        [SerializeField] private SM_StageManager stageManager;
        [SerializeField] private SM_UserSettingManager userSettingManager;
        [SerializeField] private SM_UnitManager unitManager;
        [SerializeField] private SM_GridManager gridManager;
        
        // Server
        [SerializeField] private SM_FirebaseInitializer firebaseInitializer;
        [SerializeField] private SM_FirebaseDBManager dBManager;
        [SerializeField] private SM_FirebaseManager firebaseManager;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
        
            // Manager Event Hub Signal
            Container.DeclareSignal<SceneLoadedSignal>();
            Container.DeclareSignal<Signal_FirebaseReady>();
            Container.DeclareSignal<Signal_InitializeManagers>();

            var eventHub = new SM_ManagerEventHub();
            SM_GlobalEventHub.InitializeEventHub(eventHub);
            Container.Bind<SM_ManagerEventHub>().FromInstance(eventHub).AsSingle();
            Container.BindSignal<SceneLoadedSignal>().ToMethod<SM_ManagerEventHub>(x => x.OnSceneLoadedSignalReceived).FromResolve();

            // Stage Event Hub Signal
            Container.DeclareSignal<Signal_StageClear>();
            Container.DeclareSignal<Signal_HitDamaged>();

            var stageHub = new SM_StageEventHub();
            SM_GlobalEventHub.InitializeStageHub(stageHub);
            Container.Bind<SM_StageEventHub>().FromInstance(stageHub).AsSingle();
            
            
            Container.Bind<SM_GameManager>().FromInstance(gameManager).AsSingle();
            Container.Bind<SM_PlayerController>().FromInstance(playerController).AsSingle();

            
            // tip. 하이라키 오브젝트와 바인딩 해야함.
            Container.Bind().FromInstance(sceneManager).AsSingle();
            Container.Bind().FromInstance(uiManager).AsSingle();
            Container.Bind().FromInstance(tableManager).AsSingle();
            Container.Bind().FromInstance(inputManager).AsSingle();
            Container.Bind().FromInstance(stageManager).AsSingle();
            Container.Bind().FromInstance(userSettingManager).AsSingle();
            Container.Bind().FromInstance(unitManager).AsSingle();
            Container.Bind().FromInstance(gridManager).AsSingle();
            
            Container.Bind<SM_FirebaseInitializer>().FromInstance(firebaseInitializer).AsSingle();
            Container.Bind().FromInstance(firebaseManager).AsSingle();
            Container.Bind().FromInstance(dBManager).AsSingle();
        }
    }
}