using System.Collections.Generic;
using Interface;
using Systems;
using UnityEngine;

namespace Table
{
    [CreateAssetMenu(fileName = "EnemyUnitDataTable", menuName = "Tables/Enemy Unit Data Table")]
    public class SM_EnemyUnit_DataTable : ScriptableObject, ISM_DataTable
    {
        public Dictionary<int, SM_EnemyEntry> DataMap = new ();
        public void RegisterData()
        {
            TextAsset textAsset = SM_SystemLibrary.CreateTextAsset("Tables/EnemyUnit");
            if (textAsset != null)
            {
                SM_SystemLibrary.CreateTableObject(textAsset, DataMap);
            }
        }

        public void Clear() => DataMap.Clear();

        public string GetPath() => null;
    }

    public class SM_EnemyEntry : SM_Data
    {
        public int Key;
        public string EnemyName;
        public int PrefabKey;
        public int Level;
    }
}