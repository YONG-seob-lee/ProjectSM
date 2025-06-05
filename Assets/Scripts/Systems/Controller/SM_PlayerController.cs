using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Systems.Controller
{
    public class SM_PlayerController : MonoBehaviour
    {
        [Inject] private SM_InputManager _inputManager;

        // 
        private Dictionary<string, Key> _actionToKeyMap = new()
        {
            { "MoveUp", Key.W },
            { "MoveLeft", Key.A },
            { "MoveRight", Key.D },
            { "UseSkill", Key.Q },
            { "Interact", Key.E }
        };

        void InitializeInputMap()
        {
            //_actionToKeyMap = SM_UserGameSetting.GetActionKeyMap();
        }
        
        void Update()
        {
            foreach (var (action, key) in _actionToKeyMap)
            {
                var control = Keyboard.current[key];
                if (control == null) continue;

                if (control.wasPressedThisFrame)
                    Dispatch(action, EInputState.Pressed);
                else if (control.wasReleasedThisFrame)
                    Dispatch(action, EInputState.Released);
                else if (control.isPressed)
                    Dispatch(action, EInputState.Held);
            }
        }

        private void Dispatch(string action, EInputState state)
        {
            Debug.Log($"Action: {action}, State: {state}");

            _inputManager.NotifyInput(action, state);
        }
    }
}