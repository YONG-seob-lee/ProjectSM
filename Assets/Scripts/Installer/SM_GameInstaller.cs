using Managers;
using Systems.Controller;
using Systems.EventHub;
using Systems.EventSignals;
using UnityEngine;
using Zenject;

namespace Installer
{
    public class SM_GameInstaller : MonoInstaller
    {
        [SerializeField] private SM_GameManager gameManager;
        [SerializeField] private SM_PlayerController playerController;
        
        [SerializeField] private SM_SceneManager sceneManager;
        [SerializeField] private SM_UIManager uiManager;
        [SerializeField] private SM_TableManager tableManager;
        [SerializeField] private SM_InputManager inputManager;
        [SerializeField] private SM_ModeManager modeManager;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
        
            Container.DeclareSignal<SceneLoadedSignal>();
            Container.Bind<SM_ManagerEventHub>().AsSingle();
            Container.BindSignal<SceneLoadedSignal>().ToMethod<SM_ManagerEventHub>(x => x.OnSceneLoadedSignalReceived).FromResolve();

            Container.DeclareSignal<Signal_InitializeManagers>();
            
            Container.Bind<SM_GameManager>().FromInstance(gameManager).AsSingle();
            
            // tip. 하이라키 오브젝트와 바인딩 해야함.
            Container.Bind().FromInstance(sceneManager).AsSingle();
            Container.Bind().FromInstance(uiManager).AsSingle();
            Container.Bind().FromInstance(tableManager).AsSingle();
            Container.Bind().FromInstance(inputManager).AsSingle();
            Container.Bind().FromInstance(modeManager).AsSingle();
            
            Container.Bind<SM_PlayerController>().FromInstance(playerController).AsSingle();
        }
    }

    public class Signal_InitializeManagers
    {
        public SM_ManagerEventHub EventHub;

        public Signal_InitializeManagers(SM_ManagerEventHub eventHub)
        {
            EventHub = eventHub;
        }
    }
}