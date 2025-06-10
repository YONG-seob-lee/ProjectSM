using Firebase.Database;
using Firebase.Extensions;
using Installer;
using Managers;
using Systems.EventHub;
using UnityEngine;
using Zenject;

namespace Systems.Server
{
    public class SM_FirebaseDBManager : MonoBehaviour, ISM_ManagerBase
    {
        private SM_ManagerEventHub _eventHub;
        private DatabaseReference _dbRef;
        
        [Inject]
        public void Construct(SignalBus signalBus)
        {
            signalBus.Subscribe<Signal_InitializeManagers>(x => InitManager(x.EventHub));
            
            // ReSharper disable once Unity.NoNullPropagation
            SM_GameManager.Instance?.RegisterManager(ESM_Manager.DBManager, this);
        }
        public void InitManager(SM_ManagerEventHub eventHub)
        {
            _eventHub = eventHub;
            _dbRef = FirebaseDatabase.DefaultInstance.RootReference;
            
            // 데이터 저장
            SavePlayerName("dydtjql123", "뇽서비");
            
            // 데이터 읽기
            LoadPlayerName("dydtjql123");
        }
        public void DestroyManager()
        {
            throw new System.NotImplementedException();
        }
        public void SavePlayerName(string userId, string nickname)
        {
            _dbRef.Child("users").Child(userId).Child("nickname").SetValueAsync(nickname)
                .ContinueWithOnMainThread(task =>
                {
                    if (task.IsCompleted)
                    {
                        SM_Log.INFO("닉네임 저장 완료");
                    }
                });
        }

        public void LoadPlayerName(string userId)
        {
            _dbRef.Child("users").Child(userId).Child("nickname").GetValueAsync()
                .ContinueWithOnMainThread(task =>
                {
                    if (task.IsCompleted)
                    {
                        DataSnapshot snapshot = task.Result;
                        SM_Log.INFO($"불러온닉네임 : {snapshot.Value}");
                    }
                });
        }
    }
}