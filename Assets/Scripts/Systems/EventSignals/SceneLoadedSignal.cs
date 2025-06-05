using UnityEngine.SceneManagement;

namespace Systems.EventSignals
{
    public class SceneLoadedSignal
    {
        public UnityEngine.SceneManagement.Scene Scene { get; }
        public LoadSceneMode LoadMode { get; }

        public SceneLoadedSignal(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
        {
            Scene = scene;
            LoadMode = mode;
        }
    }
}