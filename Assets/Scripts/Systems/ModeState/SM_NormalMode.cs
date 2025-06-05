using Systems.Controller;
using UnityEngine;

namespace Systems.Scene
{
    public class SM_NormalMode : SM_ModeState
    {
        private float _duration;
        private float _elapsedTime;

        public override void PostInitialize(int modeType, float duration)
        {
            _modeType = modeType;
            _duration = duration;
        }
        
        protected void Begin()
        {
            _elapsedTime = 0f;
            _isRunning = true;
        }

        protected override void Exit()
        {
            _isRunning = false;
        }

        protected override void OnOnInputReceived(string action, EInputState state)
        {
            // 여기에 인풋을 박아 넣는다.
        }

        public override void Update()
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