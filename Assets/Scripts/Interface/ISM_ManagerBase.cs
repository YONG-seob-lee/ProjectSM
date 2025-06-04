using Systems.EventHub;
using Zenject;

public enum ESM_Manager
{
    Nono = 0,
    SceneManager = 1,
    UIManager = 2,
    TableManager = 3,
}

namespace Systems
{
    public interface ISM_ManagerBase
    {
        void InitManager(SM_ManagerEventHub eventHub);

        void Construct(SignalBus signalBus);
        void DestroyManager();
    }
}