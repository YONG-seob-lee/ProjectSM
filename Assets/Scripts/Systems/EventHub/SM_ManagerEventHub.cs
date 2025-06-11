using System;
using Systems.Controller;
using Systems.EventSignals;
using Systems.Interface;
using UnityEngine.SceneManagement;

namespace Systems.EventHub
{
    public class SM_ManagerEventHub : ISM_EventHub
    {
        public event Action<UnityEngine.SceneManagement.Scene, LoadSceneMode> OnSceneChanged;
        public void OnSceneLoadedSignalReceived(SceneLoadedSignal signal)
        {
            OnSceneChanged?.Invoke(signal.Scene, signal.LoadMode);
        }
        public event Action<string, EInputState> OnInputReceived;

        public void BroadcastInput(string action, EInputState state)
        {
            OnInputReceived?.Invoke(action, state);
        }
        
        public event Action OnPauseRequested;
        public event Action OnGameOver;
    }
    
    public class Signal_InitializeManagers
    {
        public SM_ManagerEventHub EventHub;

        public Signal_InitializeManagers(SM_ManagerEventHub eventHub)
        {
            EventHub = eventHub;
        }
    }
    
    public class Signal_FirebaseReady { }
}