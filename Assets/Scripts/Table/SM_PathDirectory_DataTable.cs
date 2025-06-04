using System.Collections.Generic;
using Interface;
using Systems;
using UnityEngine;

namespace Table
{
    [CreateAssetMenu(fileName = "PathDirectoryDataTable", menuName = "Tables/Directory Data Table")]
    public class SM_PathDirectory_DataTable : ScriptableObject, ISM_DataTable
    {
        public Dictionary<int, SM_DirectoryEntry> DataMap = new();
        public void RegisterData()
        {
            TextAsset textAsset = SM_SystemLibrary.CreateTextAsset("Tables/PathDirectory");
            if (textAsset != null)
            {
                SM_SystemLibrary.CreateTableObject(textAsset, DataMap);
            }
        }

        public void Clear() => DataMap.Clear();

        public string GetDirectory(int directoryKey)
        {
            if (DataMap.TryGetValue(directoryKey, out var directoryEntry))
            {
                return directoryEntry.Directory;
            }

            return null;
        }
    }

    public class SM_DirectoryEntry : SM_Data
    {
        public int Key;
        public string Directory;
    }
}