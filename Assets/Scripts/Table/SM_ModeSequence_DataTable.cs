using System.Collections.Generic;
using Interface;
using Systems;
using Systems.Scene;
using UnityEngine;

namespace Table
{
    [CreateAssetMenu(fileName = "ModeSequenceDataTable", menuName = "Tables/Mode Sequence Data Table")]
    public class SM_ModeSequence_DataTable : ScriptableObject, ISM_DataTable
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

        public Queue<int> GetModeSequence(string sceneName)
        {
            // 1. 해당 씬이름과 같은걸 가져오고
            // 2. 내림차순한다음
            // 3. 리턴
            return null;
        }
    }
}