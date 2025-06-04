using UnityEngine.SceneManagement;

namespace Systems.EventSignals
{
    public class SceneLoadedSignal
    {
        public Scene Scene { get; }
        public LoadSceneMode LoadMode { get; }

        public SceneLoadedSignal(Scene scene, LoadSceneMode mode)
        {
            Scene = scene;
            LoadMode = mode;
        }
    }
}