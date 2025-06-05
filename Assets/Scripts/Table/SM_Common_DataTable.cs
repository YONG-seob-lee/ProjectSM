using System.Collections.Generic;
using System.Linq;
using Interface;
using Systems;
using UnityEngine;

namespace Table
{
    public enum ESM_CommonType
    {
        LOADING_TIME,
        FADE_DURATION_TIME
    }

    public enum ESM_ParamType
    {
        Param01,
        Param02,
        Param03,
    }
    [CreateAssetMenu(fileName = "CommonDataTable", menuName = "Tables/Common Data Table")]
    public class SM_Common_DataTable : ScriptableObject, ISM_DataTable
    {
        public Dictionary<int, SM_CommonEntry> DataMap = new ();
        public void RegisterData()
        {
            TextAsset textAsset = SM_SystemLibrary.CreateTextAsset("Tables/Common");
            if (textAsset != null)
            {
                SM_SystemLibrary.CreateTableObject(textAsset, DataMap);
            }
        }

        public float GetParameter(ESM_CommonType commonType, ESM_ParamType paramType)
        {
            SM_CommonEntry commonEntry = DataMap.FirstOrDefault(x => x.Value.TypeName == commonType.ToString()).Value;

            switch (paramType)
            {
                case ESM_ParamType.Param01:
                    return commonEntry.Parameter01;
                case ESM_ParamType.Param02:
                    return commonEntry.Parameter02;
                case ESM_ParamType.Param03:
                    return commonEntry.Parameter03;
                default:
                    return -1f;
            }
        }

        public void Clear() => DataMap.Clear();
    }

    public class SM_CommonEntry : SM_Data
    {
        public int Key;
        public string TypeName;
        public float Parameter01;
        public float Parameter02;
        public float Parameter03;
    }
}