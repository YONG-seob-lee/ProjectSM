using System.Collections.Generic;
using Interface;
using Managers;
using Systems;
using UnityEngine;

namespace Table
{
    [CreateAssetMenu(fileName = "BGDataTable", menuName = "Tables/BG Data Table")]
    public class SM_Bg_DataTable : ScriptableObject, ISM_DataTable
    {
        public Dictionary<int, SM_BgEntry> DataMap = new();
        public void RegisterData()
        {
            TextAsset textAsset = SM_SystemLibrary.CreateTextAsset("Tables/BG");
            if (textAsset != null)
            {
                SM_SystemLibrary.CreateTableObject(textAsset, DataMap);
            }
        }
        
        public void Clear() => DataMap.Clear();

        public string GetBgPath(int bgKey)
        {
            SM_BgEntry bgEntry = DataMap.GetValueOrDefault(bgKey);
            if (bgEntry != null)
            {
                SM_TableManager tableManager = (SM_TableManager)SM_GameManager.Instance.GetManager(ESM_Manager.TableManager);
                if (!tableManager)
                {
                    SM_Log.ASSERT(false, "[TableManager] is not exist!");
                    return null;
                }

                return tableManager.GetPath(bgEntry.PrefabKey);
            }

            return null;
        }
    }

    public class SM_BgEntry : SM_Data
    {
        public int Key;
        public string BgName;
        public int PrefabKey;
    }
}