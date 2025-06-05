using System;
using Installer;
using Systems;
using Systems.Controller;
using Systems.EventHub;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class SM_InputManager : MonoBehaviour, ISM_ManagerBase
    {
        [Inject] private SM_ManagerEventHub _eventHub;
        [Inject] public void Construct(SignalBus signalBus)
        {
            signalBus.Subscribe<Signal_InitializeManagers>(x => InitManager(x.EventHub));
            SM_GameManager.Instance.RegisterManager(ESM_Manager.InputManager, this);
        }

        public void InitManager(SM_ManagerEventHub eventHub)
        {
            _eventHub = eventHub;
        }
        public void DestroyManager() { }
        
        public void NotifyInput(string action, EInputState state)
        {
            _eventHub.BroadcastInput(action, state);
        }
    }
}