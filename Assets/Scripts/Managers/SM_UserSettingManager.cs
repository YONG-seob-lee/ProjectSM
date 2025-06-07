using System.Collections.Generic;
using Installer;
using Systems;
using Systems.EventHub;
using Table;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class SM_UserSettingManager : MonoBehaviour, ISM_ManagerBase
    {
        private readonly Dictionary<string, string> _defaultKeyBindings = new();
            
        public void Construct(SignalBus signalBus)
        {
            signalBus.Subscribe<Signal_InitializeManagers>(x => InitManager(x.EventHub));
            // ReSharper disable once Unity.NoNullPropagation
            SM_GameManager.Instance?.RegisterManager(ESM_Manager.UIManager, this);
        }

        public void InitManager(SM_ManagerEventHub eventHub)
        {
            Init_MappingKeyboard();
            Init_MappingMouse();

            RefreshMapping();

            PlayerPrefs.Save();
        }

        private void Init_MappingKeyboard()
        {
            SM_TableManager tableManager = (SM_TableManager)SM_GameManager.Instance.GetManager(ESM_Manager.TableManager);

            for (int i = (int)ESM_CommonType.KBOARD_UP; i <= (int)ESM_CommonType.KBOARD_RIGHT; ++i)
            {
                _defaultKeyBindings[((ESM_CommonType)i).ToString()] = tableManager.GetParameter<string>((ESM_CommonType)i);
            }
        }
        private void Init_MappingMouse()
        {
            SM_TableManager tableManager = (SM_TableManager)SM_GameManager.Instance.GetManager(ESM_Manager.TableManager);

            for (int i = (int)ESM_CommonType.MOUSE_LEFT; i <= (int)ESM_CommonType.MOUSE_RIGHT; ++i)
            {
                _defaultKeyBindings[((ESM_CommonType)i).ToString()] = tableManager.GetParameter<string>((ESM_CommonType)i);
            }
        }

        private void RefreshMapping()
        {
            // PlayerPrefs 의 값으로 Input 키 관리.
            foreach (var pair in _defaultKeyBindings)
            {
                if (!PlayerPrefs.HasKey($"Key_{pair.Key}"))
                {
                    PlayerPrefs.SetString($"Key_{pair.Key}", pair.Value);
                }
            }
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