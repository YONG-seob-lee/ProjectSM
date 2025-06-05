using System;
using Systems.EventSignals;
using Systems.Interface;
using UnityEngine.SceneManagement;

namespace Systems.EventHub
{
    public class SM_ManagerEventHub : ISM_EventHub
    {
        public event Action<UnityEngine.SceneManagement.Scene, LoadSceneMode> OnSceneChanged;
        public event Action OnPauseRequested;
        public event Action OnGameOver;

        public void OnSceneLoadedSignalReceived(SceneLoadedSignal signal)
        {
            OnSceneChanged?.Invoke(signal.Scene, signal.LoadMode);
        }
    }
}