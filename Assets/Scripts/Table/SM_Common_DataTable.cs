using System.Collections.Generic;
using System.Linq;
using Interface;
using Systems;
using UnityEngine;

namespace Table
{
    public enum ESM_CommonType
    {
        LOADING_TIME = 0,
        FADE_DURATION_TIME,
        KBOARD_UP,
        KBOARD_DOWN,
        KBOARD_LEFT,
        KBOARD_RIGHT,
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

        public T GetParameter<T>(ESM_CommonType commonType, ESM_ParamType paramType)
        {
            SM_CommonEntry commonEntry = DataMap.FirstOrDefault(x => x.Value.TypeName == commonType.ToString()).Value;

            object result = null;
            
            switch (paramType)
            {
                case ESM_ParamType.Param01:
                    result = commonEntry.Parameter01;
                    break;
                case ESM_ParamType.Param02:
                    result = commonEntry.Parameter02;
                    break;
                case ESM_ParamType.Param03:
                    result = commonEntry.Parameter03;
                    break;
                default:
                    break;
            }

            if (result is T typedResult)
            {
                return typedResult;
            }
            
            SM_Log.ERROR($"타입 변환 실패: {result?.GetType().Name} → {typeof(T).Name}");
            return default;
        }

        public void Clear() => DataMap.Clear();
    }

    public class SM_CommonEntry : SM_Data
    {
        public int Key;
        public string TypeName;
        public float Parameter01;
        public string Parameter02;
        public int Parameter03;
    }
}