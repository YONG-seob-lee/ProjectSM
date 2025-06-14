using System.Collections.Generic;
using Characters;
using Systems;
using Systems.EventHub;
using Table;
using UnityEngine;
using Zenject;

namespace Managers
{
    public enum ESM_UnitType
    {
        Player,
        Enemy,
        Boss
    }
    
    public class SM_UnitManager : MonoBehaviour, ISM_ManagerBase
    {
        [Inject] private SM_ManagerEventHub _eventHub;
        private readonly Dictionary<ESM_UnitType, List<SM_UnitBase>> Units = new();
        
        [Inject]
        public void Construct(SignalBus signalBus)
        {
            signalBus.Subscribe<Signal_InitializeManagers>(x => InitManager(x.EventHub));
            SM_GameManager.Instance.RegisterManager(ESM_Manager.UnitManager, this);
        }
        public void InitManager(SM_ManagerEventHub eventHub)
        {
            _eventHub = eventHub;
        }
        public void DestroyManager()
        {
            DestroyAllUnits();
        }

        public SM_UnitBase CreateUnit(ESM_UnitType unitType)
        {
            SM_TableManager tableManager = (SM_TableManager)SM_GameManager.Instance.GetManager(ESM_Manager.TableManager);
            if (!tableManager)
            {
                SM_Log.ASSERT(false, "[TableManager] is not exist!!");
                return null;
            }

            SM_EnemyUnit_DataTable EnemyTable = (SM_EnemyUnit_DataTable)tableManager.GetTable(ESM_TableType.EnemyUnit);
            if (!EnemyTable)
            {
                SM_Log.ASSERT(false, "[EnemyUnit Table] is not exist!!");
                return null;
            }

            string prefabPath = EnemyTable.GetPath();
            if (string.IsNullOrEmpty(prefabPath))
            {
                SM_Log.ERROR($"유닛 타입({unitType})에 대한 프리팹 경로가 없습니다.");
                return null;
            }
            
            GameObject UnitObject = GameObject.Instantiate(Resources.Load<GameObject>(prefabPath));
            if (!UnitObject)
            {
                SM_Log.ERROR($"Resources.Load 실패: {prefabPath}");
                return null;
            }

            var unitFolder = GameObject.FindWithTag("Unit")?.transform;
            UnitObject.transform.SetParent(unitFolder, false);
            SM_UnitBase unit = UnitObject.GetComponent<SM_UnitBase>();
            unit.Initialize(unitType);

            if (!Units.ContainsKey(unitType))
            {
                Units[unitType] = new List<SM_UnitBase>();
            }
            else
            {
                Units[unitType].Add(unit);
            }
            
            return unit;
        }

        public void UnregisterUnit(SM_UnitBase unit)
        {
            if (!unit)
            {
                SM_Log.ERROR("Unit is no exist!");
                return;
            }
            
            List<SM_UnitBase> targetUnits = GetUnits(unit.GetUnitType());
            targetUnits.Remove(unit);
        }

        public void DestroyAllUnits()
        {
            foreach (var kvp in Units)
            {
                var unitList = kvp.Value;
                foreach (var unit in unitList)
                {
                    if (unit)
                    {
                        Destroy(unit.gameObject);
                    }
                }
            }

            Units.Clear();
            SM_Log.INFO("[UnitManager] 모든 유닛 제거 완료");
        }

        public List<SM_UnitBase> GetUnits(ESM_UnitType unitType) => Units.GetValueOrDefault(unitType);
    }
}