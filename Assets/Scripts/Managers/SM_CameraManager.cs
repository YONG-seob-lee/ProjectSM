using System.Collections.Generic;
using Camera;
using UnityEngine;

namespace Managers
{
    public class SM_CameraManager : MonoBehaviour
    {
        private Dictionary<string, SM_CameraBase> _cameraMap = new();
        private SM_CameraBase _currentCamera;

        public void RegisterCamera(string id, SM_CameraBase cam)
        {
            _cameraMap.TryAdd(id, cam);
        }

        public void SwitchTo(string id)
        {
            if (_cameraMap.TryGetValue(id, out var cam))
            {
                // ReSharper disable once Unity.NoNullPropagation
                _currentCamera?.Deactivate();
                _currentCamera = cam;
                _currentCamera.Activate();
            }
        }

        public SM_CameraBase GetCurrentCamera() => _currentCamera;
    }
}