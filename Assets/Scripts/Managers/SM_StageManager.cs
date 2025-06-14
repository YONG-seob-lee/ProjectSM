using Systems;
using Systems.EventHub;
using Systems.Stage;
using Table;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class SM_StageManager : MonoBehaviour, ISM_ManagerBase
    {
        private SM_ModeHelper _modeHelper;
        private SM_StageData _stageData;

        public bool bRegisterSuccess = false; 
        
        [Inject] public void Construct(SignalBus signalBus)
        {
            signalBus.Subscribe<Signal_InitializeManagers>(x => InitManager(x.EventHub));
            // ReSharper disable once Unity.NoNullPropagation
            SM_GameManager.Instance?.RegisterManager(ESM_Manager.StageManager, this);
        }

        public void InitManager(SM_ManagerEventHub eventHub)
        {
            //_playerInstance = Instantiate()
            _modeHelper = new SM_ModeHelper();
            if (_modeHelper != null)
            {
                _modeHelper.InitHelper();
            }
        }

        public void DestroyManager()
        {
        }

        public void RegisterStage(string nextSceneName)
        {
            CreateStageData(nextSceneName);
            
            _modeHelper.RegisterMode(_stageData.ModeSequence);
        }

        private void CreateStageData(string nextSceneName)
        {
            SM_TableManager tableManager = (SM_TableManager)SM_GameManager.Instance.GetManager(ESM_Manager.TableManager);
            if (!tableManager)
            {
                SM_Log.WARNING("TableManager is not exist!!");
                return;
            }

            SM_Stage_DataTable stageTable = (SM_Stage_DataTable)tableManager.GetTable(ESM_TableType.Stage);
            SM_Mode_DataTable modeTable = (SM_Mode_DataTable)tableManager.GetTable(ESM_TableType.Mode);
            if (!stageTable || !modeTable)
            {
                SM_Log.ERROR("Stage Table or Mode Table is not exist.");
                return;
            }

            _stageData = new SM_StageData
            {
                StageId = 1,
                ModeSequence = stageTable.GetModeSequence(nextSceneName),
                CurrentModeIndex = 0,
                IsCompleted = false
            };
        }

        private void CreatePlayer()
        {
            SM_UnitManager unitManager = (SM_UnitManager)SM_GameManager.Instance.GetManager(ESM_Manager.UnitManager);
            if (!unitManager)
            {
                SM_Log.ASSERT(false, "[Unit Manager] is not exist!");
                return;
            }

            unitManager.CreateUnit(ESM_UnitType.Player);
        }

        public void StartStage()
        {
            CreatePlayer();
            StartCurrentMode();
        }

        private void StartCurrentMode()
        {
            int currentId = _stageData.ModeSequence[_stageData.CurrentModeIndex];
            _modeHelper.StartMode(currentId);
        }

        private void OnModeComplete(int completedModeId)
        {
            _stageData.CurrentModeIndex++;
            if (_stageData.CurrentModeIndex >= _stageData.ModeSequence.Count)
            {
                _stageData.IsCompleted = true;
                _modeHelper.ClearMode();
                SM_Log.INFO($"Stage {_stageData.StageId} 완료");
                return;
            }
            
            StartCurrentMode();
            }
    }
}