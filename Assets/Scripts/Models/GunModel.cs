using System;
using UnityEngine;

namespace Models
{
    public class GunModel
    {
        public event Action<float> OnFire;
        public event Action<Vector3> OnRotate;
        public event Action OnPowderLoad;
        public event Action OnBulletLoad;

        private bool _isPowderLoaded = true;
        private bool _isBulletLoaded = true;

        public bool IsPowderLoaded => _isPowderLoaded;
        public bool IsBulletLoaded => _isBulletLoaded;

        public void Fire(float bulletStartSpeed)
        {
            if (_isPowderLoaded && _isBulletLoaded)
            {
                _isPowderLoaded = _isBulletLoaded = false;
                OnFire?.Invoke(bulletStartSpeed);
            }
        }

        public void Rotate(float horizontal, float vertical, float rotationSpeed, Vector3 currentRotation, int[] horizontalAngleRange, int[] verticalAngleRange)
        {
            Vector3 newRotation = currentRotation +
                                  new Vector3(-vertical * rotationSpeed, horizontal * rotationSpeed, 0);

            if (newRotation.x > 180)
            {
                newRotation.x -= 360f;
            }

            if (newRotation.x < verticalAngleRange[0])
                newRotation.x = verticalAngleRange[0];

            if (newRotation.x > verticalAngleRange[1])
                newRotation.x = verticalAngleRange[1];

            if (newRotation.y < horizontalAngleRange[0])
                newRotation.y = horizontalAngleRange[0];

            if (newRotation.y > horizontalAngleRange[1])
                newRotation.y = horizontalAngleRange[1];

            if (newRotation.x < 0)
                newRotation.x += 360f;

            OnRotate?.Invoke(newRotation);
        }

        public void LoadGunPowder()
        {
            if (_isPowderLoaded)
                return;

            _isPowderLoaded = true;
            OnPowderLoad?.Invoke();
        }

        public void LoadBullet()
        {
            if (_isBulletLoaded || !_isPowderLoaded)
                return;

            _isBulletLoaded = true;
            OnBulletLoad?.Invoke();
        }
    }
}
