using Managers;
using Systems.Controller;
using Systems.EventHub;
using UnityEngine;

namespace Characters
{
    public class SM_PlayerUnit : SM_UnitBase
    {
        private SM_ManagerEventHub _eventHub;
        private Vector2 _rawInput = Vector2.zero;
        private bool bInitialize = false;

        public override void Initialize(ESM_UnitType unitType)
        {
            base.Initialize(unitType);
            _eventHub = SM_GlobalEventHub.Instance;
            if (_eventHub != null)
            {
                _eventHub.OnInputReceived += OnInputReceived;
            }
        }

        private void OnDestroy()
        {
            if (_eventHub != null)
            {
                _eventHub.OnInputReceived -= OnInputReceived;
            }
        }

        protected override void Update()
        {
            base.Update();

            if (bInitialize == false)
            {
                Initialize(ESM_UnitType.Player);
                bInitialize = true;
            }
            SetMovement(_rawInput);
        }

        private void OnInputReceived(string action, EInputState state)
        {
            if (state == EInputState.Pressed || state == EInputState.Held)
            {
                switch (action)
                {
                    case "MoveUp":
                        _rawInput.y = 1;
                        break;
                    case "MoveDown":
                        _rawInput.y = -1;
                        break;
                    case "MoveLeft":
                        _rawInput.x = -1;
                        break;
                    case "MoveRight":
                        _rawInput.x = 1;
                        break;
                    default:
                        break;
                }
            }
            else if (state == EInputState.Released)
            {
                switch (action)
                {
                    case "MoveUp":
                    case "MoveDown":
                        _rawInput.y = 0;
                        break;
                    case "MoveLeft":
                    case "MoveRight":
                        _rawInput.x = 0;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}