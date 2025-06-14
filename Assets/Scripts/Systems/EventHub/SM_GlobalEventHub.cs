namespace Systems.EventHub
{
    public class SM_GlobalEventHub
    {
        public static SM_ManagerEventHub EventHub { get; private set; }
        public static SM_StageEventHub StageHub { get; private set; }

        public static void InitializeEventHub(SM_ManagerEventHub hub)
        {
            EventHub = hub;
        }

        public static void InitializeStageHub(SM_StageEventHub hub)
        {
            StageHub = hub;
        }

        public static void Reset()
        {
            EventHub = null;
            StageHub = null;
        }
    }
}