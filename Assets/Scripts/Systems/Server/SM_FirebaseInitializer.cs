using Firebase;
using Firebase.Extensions;
using Systems.EventHub;
using UnityEngine;
using Zenject;

namespace Systems.Server
{
    public class SM_FirebaseInitializer : MonoBehaviour
    {
        [Inject] private SignalBus _signalBus;
        
        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                var result = task.Result;
                if (result == DependencyStatus.Available)
                {
                    SM_Log.INFO("Firebase 초기화 성공");
                    _signalBus.Fire<Signal_FirebaseReady>();
                }
                else
                {
                    SM_Log.ERROR($"Firebase 초기화 실패 : {result}");
                }
            });
        }
    }
}