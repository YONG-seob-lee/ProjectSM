using System;
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