using UnityEngine;
using Application = UnityEngine.Device.Application;
using File = System.IO.File;

namespace Systems.SaveSystem
{
    public static class SM_SaveSyatem
    {
        private static readonly string SavePath = Application.persistentDataPath + "/save.json";

        public static void Save(SM_SaveData data)
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(SavePath, json);
        }

        public static SM_SaveData Load()
        {
            if (!File.Exists(SavePath))
            {
                
            }

            string json = File.ReadAllText(SavePath);
            return JsonUtility.FromJson<SM_SaveData>(json);
        }
    }
}