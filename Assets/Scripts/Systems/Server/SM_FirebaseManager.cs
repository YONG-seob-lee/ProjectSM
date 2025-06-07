using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine;

namespace Systems.Server
{
    public class SM_FirebaseManager : MonoBehaviour
    {
        private FirebaseAuth _auth;

        private void Start()
        {
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
    }
}