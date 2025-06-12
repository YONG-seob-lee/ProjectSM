using UnityEngine;

namespace Interface
{
    public interface ISM_CameraBase
    {
        void Activate() { }

        void Deactivate() { }

        void SetFollowTarget(Transform target) { }

        void SetLookAtTarget(Transform target) { }
    }
}