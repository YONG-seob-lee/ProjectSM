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

    [RequireComponent(typeof(CinemachineBrain))]
    public class SM_CinemaCameraBase : SM_CameraBase
    {
        [SerializeField] protected CinemachineCamera _cinaCam;

        public virtual void Activate()
        {
            _cinaCam.Priority = 20; // 활성 우선순위
        }

        public virtual void Deactivate()
        {
            _cinaCam.Priority = 10; // 비활성 우선순위
        }

        public virtual void SetFollowTarget(Transform target)
        {
            _cinaCam.Follow = target;
        }

        public virtual void SetLookAtTarget(Transform target)
        {
            _cinaCam.LookAt = target;
        }

        public CinemachineCamera GetVCam()
        {
            return _cinaCam;
        }
    }
}