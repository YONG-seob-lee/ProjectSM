using System;
using System.Collections.Generic;

namespace Systems.StateMachine
{
    public class SM_StateMachine
    {
        private Dictionary<int, SM_StateBase> StateEntry = new Dictionary<int, SM_StateBase>();
        private int CurrentStateKey = -1;
        
        public void Create() { }

        public void Destroy()
        {
            UnRegisterAllState();
        }

        public void Update()
        {
            SM_StateBase currentState = GetCurrentState();
            currentState?.Update();
        }

        public SM_StateBase RegisterState<T>(int index, string name)
            where T : SM_StateBase
        {
            if (StateEntry.ContainsKey(index))
            {
                SM_Log.WARNING($"Is Already Exist. [{name}]");
                return null;
            }

            SM_StateBase newState = (T)Activator.CreateInstance(typeof(T));
            if (newState == null)
            {
                return null;
            }
            
            newState.Initialize(index, name);
            StateEntry[index] = newState;
            
            return newState;
        }

        public void UnRegisterAllState()
        {
            foreach (var State in StateEntry)
            {
                if (State.Value != null)
                {
                    State.Value.Finalize();
                }
            }
        }

        public SM_StateBase GetCurrentState()
        {
            return StateEntry.GetValueOrDefault(CurrentStateKey);
        }

        public SM_StateBase ChangeState(int index)
        {
            return SetState_Internal(index);
        }

        private SM_StateBase GetState(int stateKey)
        {
            return StateEntry.GetValueOrDefault(stateKey);
        }

        private SM_StateBase SetState_Internal(int index)
        {
            SM_StateBase currentState = GetState(CurrentStateKey);
            if(currentState != null)
            {
                currentState.OnExitState();
            }

            CurrentStateKey = index;
            SM_StateBase nextState = GetState(index);
            if(nextState != null)
            {
                nextState.OnBeginState();
            }

            return nextState;
        }
    }
}