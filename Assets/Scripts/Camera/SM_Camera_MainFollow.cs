using UnityEngine;

namespace Camera
{
    public class SM_Camera_MainFollow : SM_CinemaCameraBase
    {
        public override void Activate()
        {
            base.Activate();
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }

        public override void SetFollowTarget(Transform target)
        {
            base.SetFollowTarget(target);
        }

        public override void SetLookAtTarget(Transform target)
        {
            base.SetLookAtTarget(target);
        }
    }
}