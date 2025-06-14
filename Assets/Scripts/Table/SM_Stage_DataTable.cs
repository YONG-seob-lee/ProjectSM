using System.Collections.Generic;
using Interface;
using Systems;
using UnityEngine;

namespace Table
{
    [CreateAssetMenu(fileName = "StageDataTable", menuName = "Tables/Stage Data Table")]
    public class SM_Stage_DataTable : ScriptableObject, ISM_DataTable
    {
        public Dictionary<int, SM_StageEntry> DataMap = new();
        public void RegisterData()
        {
            TextAsset textAsset = SM_SystemLibrary.CreateTextAsset("Tables/Stage");
            if (textAsset != null)
            {
                SM_SystemLibrary.CreateTableObject(textAsset, DataMap);
            }
        }

        public void Clear() => DataMap.Clear();

        public List<int> GetModeSequence(string sceneName)
        {
            // 1. 해당 씬이름과 같은걸 가져오고
            // 2. 내림차순한다음
            // 3. 리턴
            return null;
        }
    }

    public class SM_StageEntry : SM_Data
    {
        public int Key;
        public string SceneName;
        public int StageIndex;
        public List<int> SequenceMode;
    }
}