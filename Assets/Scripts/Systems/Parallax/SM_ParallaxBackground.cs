using UnityEngine;

namespace Systems.Parallax
{
    public class SM_ParallaxBackground : MonoBehaviour
    {
        public Transform cameraTransform;
        public float parallaxFactor = 0.5f;

        public Vector3 previousCameraPosition;

        private void Start()
        {
            if (cameraTransform == null)
            {
                cameraTransform = UnityEngine.Camera.main.transform;
            }

            previousCameraPosition = cameraTransform.position;
        }

        private void LateUpdate()
        {
            Vector3 delta = cameraTransform.position - previousCameraPosition;
            transform.position += new Vector3(delta.x * parallaxFactor, delta.y * parallaxFactor, 0f);
            previousCameraPosition = cameraTransform.position;
        }
    }
}