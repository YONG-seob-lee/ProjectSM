﻿using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Systems.Controller
{
    public class SM_PlayerController : MonoBehaviour
    {
        private SM_InputManager _inputManager;

        // 
        private Dictionary<string, Key> _actionToKeyMap = new()
        {
            { "MoveUp", Key.W },
            { "MoveDown", Key.S},
            { "MoveLeft", Key.A },
            { "MoveRight", Key.D },
            { "UseSkill", Key.Q },
            { "Interact", Key.E }
        };

        public void InitializeInputMap()
        {
            SM_UserSettingManager usManager = (SM_UserSettingManager)SM_GameManager.Instance.GetManager(ESM_Manager.UserSettingManager);
            if (!usManager)
            {
                SM_Log.ASSERT(false, "[UserSettingManager] is not Exist!!");
                return;
            }

            Dictionary<string, Key> parsingMappingKey = usManager.GetTotalKeyBinding();
            if (parsingMappingKey is { Count: > 0 })
            {
                _actionToKeyMap = usManager.GetTotalKeyBinding();
            }
        }

        private void Start()
        {
            _inputManager = (SM_InputManager)SM_GameManager.Instance.GetManager(ESM_Manager.InputManager);
        }

        void Update()
        {
            foreach (var (action, key) in _actionToKeyMap)
            {
                var control = Keyboard.current[key];
                if (control == null) continue;

                if (control.wasPressedThisFrame)
                {
                    Dispatch(action, EInputState.Pressed);
                }
                else if (control.wasReleasedThisFrame)
                {
                    Dispatch(action, EInputState.Released);
                }
                else if (control.isPressed)
                {
                    Dispatch(action, EInputState.Held);
                }
            }
        }

        private void Dispatch(string action, EInputState state)
        {
            Debug.Log($"Action: {action}, State: {state}");

            _inputManager.NotifyInput(action, state);
        }
    }
}