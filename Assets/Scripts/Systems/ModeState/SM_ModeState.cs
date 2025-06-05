using Systems.StateMachine;

namespace Systems.Scene
{
    public enum ESM_ModeType
    {
        None = 0,
        Normal,
        Boss,
        Zombie,
        CutScene,
    }
    
    public class SM_ModeState : SM_StateBase
    {
        protected void Begin()
        {
            
        }

        protected void Exit()
        {
            
        }

        public bool IsTriggerSatisfied()
        {
            throw new System.NotImplementedException();
        }
    }
}