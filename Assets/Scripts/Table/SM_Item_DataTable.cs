using System.Collections.Generic;
using Interface;
using Systems;
using UnityEngine;

namespace Table
{
    [CreateAssetMenu(fileName = "ItemDataTable", menuName = "Tables/Item Data Table")]
    public class SM_Item_DataTable : ScriptableObject, ISM_DataTable
    {
        public Dictionary<int, SM_ItemEntry> DataMap = new();

        public void RegisterData()
        {
            TextAsset textAsset = SM_SystemLibrary.CreateTextAsset("Tables/ItemData");
            if (textAsset != null)
            {
                SM_SystemLibrary.CreateTableObject(textAsset, DataMap);
            }
        }

        public void Clear() => DataMap.Clear();
    }

    public class SM_ItemEntry : SM_Data
    {
        public int Key;
        public string ItemName;
        public int ItemType;
    }
}