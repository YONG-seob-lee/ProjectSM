using Systems.EventHub;
using Zenject;

public enum ESM_Manager
{
    Nono = 0,
    SceneManager = 1,
    UIManager = 2,
    TableManager = 3,
    InputManager = 4,
    ModeManager = 5,
    DBManager = 6,
    FirebaseManager = 7,
    UnitManager = 8,
}

namespace Systems
{
    public interface ISM_ManagerBase
    {
        void Construct(SignalBus signalBus);
        void InitManager(SM_ManagerEventHub eventHub);
        void DestroyManager();
    }
}