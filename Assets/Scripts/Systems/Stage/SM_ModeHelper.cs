using System;
using System.Collections.Generic;
using Managers;
using Systems.EventHub;
using Systems.Scene;
using Systems.StateMachine;
using Table;

namespace Systems.Stage
{
    public class SM_ModeHelper
    {
        private SM_StateMachine _modeStateMachine;
        private Action<int> _onModeComplete; 

        public void InitHelper()
        {
            _modeStateMachine = new();
            
        }
        public void DestroyHelper()
        {
        }

        public void Update()
        {
            //_modeStateMachine.Update();
        }
        
        public void RegisterMode(List<int> modeIds)
        {
            SM_TableManager tableManager = (SM_TableManager)SM_GameManager.Instance.GetManager(ESM_Manager.TableManager);
            if (!tableManager)
            {
                SM_Log.ASSERT(false, "[TableManager] is not exist!!");
                return;
            }

            SM_Mode_DataTable modeTable = (SM_Mode_DataTable)tableManager.GetTable(ESM_TableType.Mode);
            if (!modeTable)
            {
                SM_Log.ASSERT(false, "[Mode Data Table] is not exist!!");
                return;
            }
            
            foreach (int modeID in modeIds)
            {
                SM_ModeEntry entry = modeTable.GetModeData(modeID);
                SM_ModeState modeState = (SM_ModeState)_modeStateMachine.RegisterState<SM_ModeState>(modeID, entry.ModeName);
                modeState?.PostInitialize(entry.ModeType, entry.DurationTime);
                modeState?.RegisterInput();
            }
        }
        
        public void StartMode(int modeId)
        {
            var state = (SM_ModeState)_modeStateMachine.ChangeState(modeId);
            state.SetCallback(() => _onModeComplete?.Invoke(modeId));
        }
        public void ClearMode()
        {
            _modeStateMachine.UnRegisterAllState();
        }
    }
}