using System.Collections.Generic;
using Interface;
using Systems;
using UnityEngine;

namespace Table
{
    [CreateAssetMenu(fileName = "ModeDataTable", menuName = "Tables/Mode Data Table")]
    public class SM_Mode_DataTable : ScriptableObject, ISM_DataTable
    {
        public Dictionary<int, SM_ItemEntry> DataMap = new();
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
    }
}