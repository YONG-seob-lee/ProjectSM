using Firebase.Auth;
using Firebase.Extensions;
using Installer;
using Managers;
using Systems.EventHub;
using UnityEngine;
using Zenject;

namespace Systems.Server
{
    public class SM_FirebaseManager : MonoBehaviour, ISM_ManagerBase
    {
        private SM_ManagerEventHub _eventHub;
        private FirebaseAuth _auth;
        public void Construct(SignalBus signalBus)
        {
            signalBus.Subscribe<Signal_InitializeManagers>(x => InitManager(x.EventHub));
            
            // ReSharper disable once Unity.NoNullPropagation
            SM_GameManager.Instance?.RegisterManager(ESM_Manager.FirebaseManager, this);
        }
        public void InitManager(SM_ManagerEventHub eventHub)
        {
            _eventHub = eventHub;
            
            _auth = FirebaseAuth.DefaultInstance;

            _auth.SignInAnonymouslyAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    SM_Log.ERROR("로그인 실패");
                    return;
                }

                AuthResult result = task.Result;
                FirebaseUser user = result.User;
                SM_Log.INFO($"로그인 성공 : {user.UserId}");
            });
        }
        public void DestroyManager()
        {
            throw new System.NotImplementedException();
        }
    }
}