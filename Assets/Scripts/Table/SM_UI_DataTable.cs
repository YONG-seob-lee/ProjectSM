using System.Collections.Generic;
using System.Linq;
using Interface;
using Managers;
using Systems;
using UnityEngine;

namespace Table
{
[CreateAssetMenu(fileName = "UIDataTable", menuName = "Tables/UI Data Table")]
    public class SM_UI_DataTable : ScriptableObject, ISM_DataTable
    {
        public Dictionary<int, SM_UIEntry> DataMap = new ();

        public void Construct() { }

        public void RegisterData()
        {
            TextAsset textAsset = SM_SystemLibrary.CreateTextAsset("Tables/UIData");
            if(textAsset != null)
            {
                SM_SystemLibrary.CreateTableObject(textAsset, DataMap);
            }
        }

        public void Clear() => DataMap.Clear();
        
        public GameObject GetUI(string uiName)
        {
            int prefabKey = DataMap.FirstOrDefault(x => x.Value.UIName == uiName).Value.PrefabKey;

            SM_TableManager tableManager = (SM_TableManager)SM_GameManager.Instance.GetManager(ESM_Manager.TableManager);
            if (!tableManager)
            {
                return null;
            }
            string prefabPath = tableManager.GetPath(prefabKey);

            return Instantiate(Resources.Load<GameObject>(prefabPath));
        }
    }
}

public class SM_UIEntry : SM_Data
{
    public int Key;
    public string UIName;
    public int PrefabKey;
}