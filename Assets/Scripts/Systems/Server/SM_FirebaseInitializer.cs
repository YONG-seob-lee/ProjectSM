using Firebase;
using Firebase.Extensions;
using Installer;
using Managers;
using Systems.EventHub;
using UnityEngine;
using Zenject;

namespace Systems.Server
{
    public class SM_FirebaseInitializer : MonoBehaviour, ISM_ManagerBase
    {
        private SM_ManagerEventHub _eventHub;

        public void Construct(SignalBus signalBus)
        {
            signalBus.Subscribe<Signal_InitializeManagers>(x => InitManager(x.EventHub));
            
            // ReSharper disable once Unity.NoNullPropagation
            SM_GameManager.Instance?.RegisterManager(ESM_Manager.FirebaseManager, this);
        }
        public void InitManager(SM_ManagerEventHub eventHub)
        {
            _eventHub = eventHub;
            
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                var result = task.Result;
                if (result == DependencyStatus.Available)
                {
                    SM_Log.INFO("Firebase 초기화 성공");
                }
                else
                {
                    SM_Log.ERROR($"Firebase 초기화 실패 : {result}");
                }
            });
        }

        public void DestroyManager()
        {
            throw new System.NotImplementedException();
        }
    }
}