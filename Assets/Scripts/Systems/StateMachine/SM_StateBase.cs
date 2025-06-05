namespace Systems.StateMachine
{
    public interface SM_StateInterface
    {
        public virtual void Initialize(int index, string name) {}

        void OnBeginState() {}
        void OnExitState() {}
        
        protected void Begin() {}
        protected void Exit() {}
    }
    
    public abstract class SM_StateBase : SM_StateInterface
    {
        private int _stateIndex = -1;
        private string _stateName;

        public virtual void Initialize(int index, string name) 
        {
            _stateIndex = index;
            _stateName = name;
            
        }
        public virtual void Finalize() { }
        public void OnBeginState() => Begin();
        public void OnExitState() => Exit();
        
        protected void Begin() {}
        protected void Exit() {}
    }
}