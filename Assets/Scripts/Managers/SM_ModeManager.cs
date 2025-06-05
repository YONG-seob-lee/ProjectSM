using System.Collections.Generic;
using Installer;
using Systems;
using Systems.EventHub;
using Systems.Scene;
using Systems.StateMachine;
using Table;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class SM_ModeManager : MonoBehaviour, ISM_ManagerBase
    {
        private SM_StateMachine _modeStateMachine;
        private Queue<int> _modeSequence; // Mode_ID

        [Inject] public void Construct(SignalBus signalBus)
        {
            signalBus.Subscribe<Signal_InitializeManagers>(x => InitManager(x.EventHub));
            // ReSharper disable once Unity.NoNullPropagation
            SM_GameManager.Instance?.RegisterManager(ESM_Manager.ModeManager, this);
        }
        
        public void InitManager(SM_ManagerEventHub eventHub)
        {
            _modeStateMachine = new();
            _modeSequence = new();
        }


        public void DestroyManager()
        {
        }

        public void Update()
        {
            if (_modeStateMachine.GetCurrentState() is SM_ModeState modeState)
            {
                if (modeState.IsTriggerSatisfied())
                {
                    if (_modeSequence.Count > 0)
                    {
                        int nextID = _modeSequence.Dequeue();
                        _modeStateMachine.ChangeState(nextID);
                    }
                }
            }
        }
        
        public void RegisterMode(string sceneName)
        {
            RegisterType(sceneName);
            RegisterModeState();
        }
        private void RegisterType(string sceneName)
        {
            SM_TableManager tableManager = (SM_TableManager)SM_GameManager.Instance.GetManager(ESM_Manager.TableManager);
            if (!tableManager)
            {
                SM_Log.WARNING("TableManager is not exist!!");
                return;
            }

            SM_ModeSequence_DataTable modeSequenceTable = (SM_ModeSequence_DataTable)tableManager.GetTable(ESM_TableType.ModeSequence);
            if (modeSequenceTable)
            {
                _modeSequence = modeSequenceTable.GetModeSequence(sceneName);
            }
        }
        private void RegisterModeState()
        {
            SM_TableManager tableManager = (SM_TableManager)SM_GameManager.Instance.GetManager(ESM_Manager.TableManager);
            if (!tableManager)
            {
                SM_Log.WARNING("TableManager is not exist!!");
                return;
            }
            SM_Mode_DataTable modeTable = (SM_Mode_DataTable)tableManager.GetTable(ESM_TableType.Mode);
            if (!modeTable)
            {
                SM_Log.WARNING("ModeTable is not exist!!");
                return;
            }
            
            while (_modeSequence.Count > 0)
            {
                int modeID = _modeSequence.Dequeue();
                _modeStateMachine.RegisterState<SM_ModeState>(modeID, modeTable.GetModeName(modeID));
            }
        }
        public void ClearMode()
        {
            _modeSequence.Clear();
            _modeStateMachine.UnRegisterAllState();
        }
    }
}