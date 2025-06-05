using System.Runtime.CompilerServices;
using Systems;
using UI.Base;

namespace UI
{
    public class SM_FadeDebugPanel : SM_PanelBase
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static string GetPath() { return "Prefabs/UI/FadeDebug_Panel"; }

        protected override void Awake()
        {
            base.Awake();
            SM_Log.INFO("FadeDebugPanel Awake");
        }
    }
}