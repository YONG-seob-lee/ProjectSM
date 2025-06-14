using System.Collections.Generic;
using Interface;
using Managers;
using Systems;
using UnityEngine;

namespace Table
{
    [CreateAssetMenu(fileName = "PlayerUnitDataTable", menuName = "Tables/Player Unit Data Table")]
    public class SM_PlayerUnit_DataTable: ScriptableObject, ISM_DataTable
    {
        public Dictionary<int, SM_PlayerEntry> DataMap = new ();
        public void RegisterData()
        {
            TextAsset textAsset = SM_SystemLibrary.CreateTextAsset("Tables/EnemyUnit");
            if (textAsset != null)
            {
                SM_SystemLibrary.CreateTableObject(textAsset, DataMap);
            }
        }

        public string GetPlayerPath(int playerId)
        {
            SM_PlayerEntry playerEntry = DataMap.GetValueOrDefault(playerId);
            if (playerEntry != null)
            {
                SM_TableManager tableManager = (SM_TableManager)SM_GameManager.Instance.GetManager(ESM_Manager.TableManager);
                if (!tableManager)
                {
                    SM_Log.ASSERT(false, "[TableManager] is not exist!");
                    return null;
                }

                return tableManager.GetPath(playerEntry.PrefabKey);
            }

            return null;
        }

        public void Clear() => DataMap.Clear();
    }

    public class SM_PlayerEntry : SM_Data
    {
        public int Key;
        public string PlayerName;
        public int PrefabKey;
    }
}