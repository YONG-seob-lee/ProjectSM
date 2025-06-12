using Interface;
using Unity.Cinemachine;
using UnityEngine;

namespace Camera
{
    public abstract class SM_CameraBase : MonoBehaviour, ISM_CameraBase
    {
        public virtual void Activate()
        {
            
        }

        public virtual void Deactivate()
        {
            
        }

        public virtual void SetFollowTarget(Transform target) { }

        public virtual void SetLookAtTarget(Transform target) { }
    }

    public class SM_CinemaCameraBase : SM_CameraBase
    {
        [SerializeField] protected CinemachineVirtualCamera _vcam;

        public virtual void Activate()
        {
            _vcam.Priority = 20; // 활성 우선순위
        }

        public virtual void Deactivate()
        {
            _vcam.Priority = 10; // 비활성 우선순위
        }

        public virtual void SetFollowTarget(Transform target)
        {
            _vcam.Follow = target;
        }

        public virtual void SetLookAtTarget(Transform target)
        {
            _vcam.LookAt = target;
        }

        public CinemachineVirtualCamera GetVCam()
        {
            return _vcam;
        }
    }
}