using Systems.Controller;
using Systems.EventHub;
using Systems.StateMachine;

namespace Systems.Scene
{
    public enum ESM_ModeType
    {
        Normal,
        Boss,
        Zombie,
    }
    public abstract class SM_ModeState : SM_StateBase
    {
        protected int _modeType;
        protected bool _isRunning;
        protected System.Action _onTrigger;

        public virtual void PostInitialize(int modeType, float duration) {}
        
        protected override void Begin() {}

        protected override void Exit() {}

        public void SetCallback(System.Action callback)
        {
            _onTrigger = callback;
        }
        public void RegisterInput(SM_ManagerEventHub eventHub)
        {
            eventHub.OnInputReceived += OnOnInputReceived;
        }

        protected virtual void OnOnInputReceived(string action, EInputState state) { }
        
        public override void Update() {}
    }
}