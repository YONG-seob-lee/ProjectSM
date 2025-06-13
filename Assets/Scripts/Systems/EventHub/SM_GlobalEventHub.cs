namespace Systems.EventHub
{
    public class SM_GlobalEventHub
    {
        public static SM_ManagerEventHub Instance { get; private set; }

        public static void Initialize(SM_ManagerEventHub hub)
        {
            Instance = hub;
        }

        public static void Reset()
        {
            Instance = null;
        }
    }
}