using System.Collections.Generic;
using Installer;
using Systems;
using Systems.EventHub;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class SM_UserSettingManager : MonoBehaviour, ISM_ManagerBase
    {
        private readonly Dictionary<string, string> _defaultKeyBindings = new Dictionary<string, string>()
        {
            {"MoveUp", "W"},
            {"MoveDown", "S"},
            {"MoveLeft", "A"},
            {"MoveRight", "D"},
        };
            
        public void Construct(SignalBus signalBus)
        {
            signalBus.Subscribe<Signal_InitializeManagers>(x => InitManager(x.EventHub));
            // ReSharper disable once Unity.NoNullPropagation
            SM_GameManager.Instance?.RegisterManager(ESM_Manager.UIManager, this);
        }

        public void InitManager(SM_ManagerEventHub eventHub)
        {
            foreach (var pair in _defaultKeyBindings)
            {
                if (!PlayerPrefs.HasKey($"Key_{pair.Key}"))
                {
                    PlayerPrefs.SetString($"Key_{pair.Key}", pair.Value);
                }
            }

            PlayerPrefs.Save();
        }

        public void DestroyManager()
        {
        }

        public string GetKeyBinding(string action)
        {
            return PlayerPrefs.GetString($"Key_{action}", _defaultKeyBindings.GetValueOrDefault(action, "None"));
        }

        public void SetKeyBinding(string action, string key)
        {
            PlayerPrefs.SetString($"Key_{action}", key);
            PlayerPrefs.Save();
        }

        private void ResetToDefault()
        {
            foreach (var pair in _defaultKeyBindings)
            {
                PlayerPrefs.SetString($"Key_{pair.Key}", pair.Value);
            }
            
            PlayerPrefs.Save();
        }
    }
}