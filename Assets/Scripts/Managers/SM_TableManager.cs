using System.Collections.Generic;
using Installer;
using Interface;
using Systems;
using Systems.EventHub;
using Table;
using UnityEngine;
using Zenject;

public enum ESM_TableType
{
    None = 0,
    Common,
    Directory,
    PrefabFile,
    UI,
    Item,
    Stage,
    Mode,
    PlayerUnit,
    EnemyUnit,
    BossUnit,
    TileMap,
    Background,
}

namespace Managers
{
    public class SM_TableManager : MonoBehaviour, ISM_ManagerBase
    {
        private Dictionary<ESM_TableType, ISM_DataTable> _tableMap = new();

        [Inject] public void Construct(SignalBus signalBus)
        {
            signalBus.Subscribe<Signal_InitializeManagers>(x => InitManager(x.EventHub));
            SM_GameManager.Instance?.RegisterManager(ESM_Manager.TableManager, this);
        }

        public void RegisterTable(ESM_TableType tableType, ISM_DataTable table)
        {
            _tableMap[tableType] = table;
        }

        public void InitManager(SM_ManagerEventHub eventHub)
        {
            RegisterTableData();
            TestDebug();
        }

        private void RegisterTableData()
        {
            foreach (var table in _tableMap)
            {
                table.Value.RegisterData();
            }
        }
        
        public string GetPath(int prefabKey)
        {
            SM_PrefabFile_DataTable prefabTable = (SM_PrefabFile_DataTable)GetTable(ESM_TableType.PrefabFile);
            if (prefabTable)
            {
                return prefabTable.GetPath(prefabKey);
            }

            return null;
        }

        public ISM_DataTable GetTable(ESM_TableType tableType)
        {
            return _tableMap.GetValueOrDefault(tableType);
        }
        public void DestroyManager() { }

        public T GetParameter<T>(ESM_CommonType commonType, ESM_ParamType paramType = ESM_ParamType.Param01)
        {
            SM_Common_DataTable commonDataTable = (SM_Common_DataTable)GetTable(ESM_TableType.Common);
            if (!commonDataTable)
            {
                return default;
            }
            return commonDataTable.GetParameter<T>(commonType, paramType);
        }

        private void TestDebug()
        {
            SM_Log.INFO($"Total Table Count : { _tableMap.Count }");
            foreach(var table in _tableMap)
            {
                if (table.Key == ESM_TableType.UI && table.Value is SM_UI_DataTable uiDataTable)
                {
                    foreach (var entry in uiDataTable.DataMap)
                    {
                        SM_Log.INFO($"UI Table Entry - key: {entry.Key}, Prefab : {entry.Value.PrefabKey}");
                    }
                }
                else if (table.Key == ESM_TableType.UI && table.Value is SM_Item_DataTable itemDataTable)
                {
                    foreach (var entry in itemDataTable.DataMap)
                    {
                        SM_Log.INFO($"UI Table Entry - key: {entry.Key}, ItemType : {entry.Value.ItemType}");
                    }
                }
            }
        }
    }
}