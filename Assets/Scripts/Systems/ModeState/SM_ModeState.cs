using Systems.StateMachine;
using UnityEngine;

namespace Systems.Scene
{
    public enum ESM_ModeType
    {
        Normal,
        Boss,
        Zombie,
    }
    public class SM_ModeState : SM_StateBase
    {
        private int _modeType;
        private float _duration;
        private float _elapsedTime;
        private bool _isRunning;
        private System.Action _onTrigger;

        public void PostInitialize(float duration)
        {
            _duration = duration;
        }
        
        protected void Begin()
        {
            switch ((ESM_ModeType)_modeType)
            {
                case ESM_ModeType.Normal:
                {
                    _elapsedTime = 0f;
                    break;
                }
                case ESM_ModeType.Boss:
                {
                    break;
                }
                case ESM_ModeType.Zombie:
                {
                    break;
                }
                default:
                    break;
            }
            
            _isRunning = true;
        }

        protected void Exit()
        {
            _isRunning = false;
        }

        public void SetCallback(System.Action callback)
        {
            _onTrigger = callback;
        }

        public void Update()
        {
            if (!_isRunning)
            {
                return;
            }

            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= _duration)
            {
                _isRunning = false;
                _onTrigger?.Invoke();
            }
        }
    }
}