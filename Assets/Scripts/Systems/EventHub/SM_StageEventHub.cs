using System;
using Systems.Controller;
using Systems.Interface;
using Unity.VisualScripting;

namespace Systems.EventHub
{
    public class SM_StageEventHub : ISM_EventHub
    {
        public event Action<string, EInputState> OnInputReceived;

        public void BroadcastInput(string action, EInputState state)
        {
            OnInputReceived?.Invoke(action, state);
        }
        
        public event Action<bool> OnStageClear;

        public void OnStageClearFunc(bool bClear)
        {
            OnStageClear?.Invoke(bClear);
        }

        public event Action<int, float> OnHitDamaged;

        public void OnHitDamagedFunc(int unitId, float damageAmount)
        {
            OnHitDamaged?.Invoke(unitId, damageAmount);
        }
    }

    public class Signal_StageClear
    {
        public bool BClear;

        public Signal_StageClear(bool bClear)
        {
            BClear = bClear;
        }
    }

    public class Signal_HitDamaged
    {
        public int UnitId;
        public int DamageAmount;

        public Signal_HitDamaged(int unitId, int damageAmount)
        {
            UnitId = unitId;
            DamageAmount = damageAmount;
        }
    }
}