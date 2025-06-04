using System.Collections.Generic;
using Interface;
using Managers;
using Systems;
using UnityEngine;

namespace Table
{
    [CreateAssetMenu(fileName = "PrefabFileDataTable", menuName = "Tables/PrefabFile Data Table")]
    public class SM_PrefabFile_DataTable : ScriptableObject, ISM_DataTable
    {
        public Dictionary<int, SM_PrefabFileEntry> DataMap = new();

        public void RegisterData()
        {
            TextAsset textAsset = SM_SystemLibrary.CreateTextAsset("Tables/PrefabFile");
            if (textAsset != null)
            {
                SM_SystemLibrary.CreateTableObject(textAsset, DataMap);
            }
        }

        public void Clear() => DataMap.Clear();

        public string GetPath(int prefabKey)
        {
            if (DataMap.TryGetValue(prefabKey, out var prefabFileEntry) == false)
            {
                return null;
            }
            
            SM_TableManager tableManager = (SM_TableManager)SM_GameManager.Instance.GetManager(ESM_Manager.TableManager);
            if (!tableManager)
            {
                return null;
            }
            
            SM_PathDirectory_DataTable directoryTable = (SM_PathDirectory_DataTable)tableManager.GetTable(ESM_TableType.Directory);
            if (directoryTable)
            {
                string directory = directoryTable.GetDirectory(prefabFileEntry.Directory_Key);
                return directory + "/" + prefabFileEntry.Prefab_FileName;
            }

            return null;
        }
    }

    public class SM_PrefabFileEntry : SM_Data
    {
        public int Key;
        public int Directory_Key;
        public string Prefab_FileName;
    }
}