using Camera;
using Managers;
using Systems.Controller;
using Systems.EventHub;
using UnityEngine;

namespace Units
{
    public class SM_PlayerUnit : SM_UnitBase
    {
        [SerializeField] private SM_Camera_MainFollow mainCamera;
        private Vector2 _rawInput = Vector2.zero;
        private bool bInitialize = false;

        public override void Initialize(ESM_UnitType unitType)
        {
            base.Initialize(unitType);
            SM_GlobalEventHub.StageHub.OnInputReceived += OnInputReceived;

            mainCamera = FindAnyObjectByType<SM_Camera_MainFollow>();
            mainCamera.SetFollowTarget(transform);
            mainCamera.SetLookAtTarget(transform);
            mainCamera.Activate();
        }

        private void OnDestroy()
        {
            SM_GlobalEventHub.StageHub.OnInputReceived -= OnInputReceived;
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