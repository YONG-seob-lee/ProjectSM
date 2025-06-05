using System.Collections.Generic;
using Interface;
using Systems;
using UnityEngine;

namespace Table
{
    [CreateAssetMenu(fileName = "ModeDataTable", menuName = "Tables/Mode Data Table")]
    public class SM_Mode_DataTable : ScriptableObject, ISM_DataTable
    {
        public Dictionary<int, SM_ModeEntry> DataMap = new();
        public void RegisterData()
        {
            TextAsset textAsset = SM_SystemLibrary.CreateTextAsset("Tables/ModeSequence");
            if (textAsset != null)
            {
                SM_SystemLibrary.CreateTableObject(textAsset, DataMap);
            }
        }

        public void Clear() => DataMap.Clear();

        public string GetModeName(int modeID)
        {
            throw new System.NotImplementedException();
        }

        public SM_ModeEntry GetModeData(int key)
        {
            return DataMap.GetValueOrDefault(key);
        }
    }

    public class SM_ModeEntry : SM_Data
    {
        public int Key;
        public string ModeType;
        public int TriggerType;
        public int DurationTime;
    }
}