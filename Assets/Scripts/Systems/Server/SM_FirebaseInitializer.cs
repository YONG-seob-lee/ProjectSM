using Firebase;
using Firebase.Extensions;
using UnityEngine;

namespace Systems.Server
{
    public class SM_FirebaseInitializer : MonoBehaviour
    {
        private void Start()
        {
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
    }
}