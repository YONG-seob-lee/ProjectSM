using Firebase.Database;
using Firebase.Extensions;

namespace Systems.Server
{
    public class SM_FirebaseDBManager
    {
        private DatabaseReference _dbRef;

        private void Start()
        {
            _dbRef = FirebaseDatabase.DefaultInstance.RootReference;
            
            // 데이터 저장
            SavePlayerName("dydtjql123", "뇽서비");
            
            // 데이터 읽기
            LoadPlayerName("dydtjql123");
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