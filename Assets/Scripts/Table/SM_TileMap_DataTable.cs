using System.Collections.Generic;
using Interface;
using Managers;
using Systems;
using UnityEngine;

namespace Table
{
    [CreateAssetMenu(fileName = "TileMapDataTable", menuName = "Tables/TileMap Data Table")]
    public class SM_TileMap_DataTable : ScriptableObject, ISM_DataTable
    {
        public Dictionary<int, SM_TileMapEntry> DataMap = new();
        public void RegisterData()
        {
            TextAsset textAsset = SM_SystemLibrary.CreateTextAsset("Tables/TileMap");
            if (textAsset != null)
            {
                SM_SystemLibrary.CreateTableObject(textAsset, DataMap);
            }
        }

        public void Clear() => DataMap.Clear();
        
        public string GetTileMapPath(int bgKey)
        {
            SM_TileMapEntry TileMapEntry = DataMap.GetValueOrDefault(bgKey);
            if (TileMapEntry != null)
            {
                SM_TableManager tableManager = (SM_TableManager)SM_GameManager.Instance.GetManager(ESM_Manager.TableManager);
                if (!tableManager)
                {
                    SM_Log.ASSERT(false, "[TableManager] is not exist!");
                    return null;
                }

                return tableManager.GetPath(TileMapEntry.PrefabKey);
            }

            return null;
        }
    }

    public class SM_TileMapEntry : SM_Data
    {
        public int Key;
        public string MapName;
        public int PrefabKey;
    }
}