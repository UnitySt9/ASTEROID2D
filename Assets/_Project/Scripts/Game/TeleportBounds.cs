using UnityEngine;

namespace _Project.Scripts
{
    public class TeleportBounds
    {
        private readonly Transform _targetTransform;
        private readonly Camera _camera;
        private Vector3 _cameraBounds;

        public TeleportBounds(Transform targetTransform, Camera camera)
        {
            _targetTransform = targetTransform;
            _camera = camera;

            if (_camera != null)
            {
                _cameraBounds = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _camera.transform.position.z));
            }
        }

        public void BoundsUpdate()
        {
            if (_camera == null)
                return;

            Vector3 viewPos = _targetTransform.position;

            if (viewPos.x > _cameraBounds.x)
                viewPos.x = -_cameraBounds.x;
            if (viewPos.x < -_cameraBounds.x)
                viewPos.x = _cameraBounds.x;
            if (viewPos.y > _cameraBounds.y)
                viewPos.y = -_cameraBounds.y;
            if (viewPos.y < -_cameraBounds.y)
                viewPos.y = _cameraBounds.y;

            _targetTransform.position = viewPos;
        }
    }
}
