using Systems.EventHub;
using Zenject;

public enum ESM_Manager
{
    Nono = 0,
    SceneManager = 1,
    UIManager = 2,
    TableManager = 3,
    InputManager = 4,
    StageManager = 5,
    UserSettingManager = 6,
    DBManager = 7,
    FirebaseManager = 8,
    UnitManager = 9,
    GridManager = 10,
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