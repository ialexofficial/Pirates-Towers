using Models;
using ScriptableObjects;
using UnityEngine;
using BulletComponent = Components.Bullet;

namespace Views
{
    public class GunView : MonoBehaviour
    {
        [Header("Cannon properties")]
        [SerializeField] private float rotationSpeed = 1;
        [SerializeField] private float reloadingTime = 1.5f;
        [SerializeField] private int[] verticalAngleRange = {40, 140};
        [SerializeField] private int[] horizontalAngleRange = {30, 150};
        [SerializeField] private Transform verticalRotatingObject;

        [Header("Bullet properties")]
        [SerializeField] private float bulletStartSpeed = 20f;
        [SerializeField] private Bullet bulletScriptableObject;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Vector3 bulletSpawnOffset = new Vector3(0, 1, 0);

        private GunModel _model;

        public bool IsPowderLoaded => _model.IsPowderLoaded;
        public bool IsBulletLoaded => _model.IsBulletLoaded;

        public void Rotate(Vector2 movement)
        {
            _model.Rotate(movement.x, movement.y, rotationSpeed, verticalRotatingObject.eulerAngles, horizontalAngleRange, verticalAngleRange);
        }

        public void Fire()
        {
            _model.Fire(bulletStartSpeed);
        }

        public void LoadGunPowder()
        {
            _model.LoadGunPowder();
        }

        public void LoadBullet()
        {
            _model.LoadBullet();
        }

        private void Awake()
        {
            _model = new GunModel();

            _model.OnFire += Fired;
            _model.OnRotate += Rotated;
            _model.OnPowderLoad += () => { };
            _model.OnBulletLoad += () => { };
        }

        private void Fired(float bulletStartSpeed)
        {
            BulletComponent bullet = Instantiate(bulletPrefab, transform.position + bulletSpawnOffset, verticalRotatingObject.rotation).GetComponent<BulletComponent>();
            bullet.scriptableObject = bulletScriptableObject;
            
            bullet.GetComponent<Rigidbody>().AddForce(verticalRotatingObject.forward * bulletStartSpeed, ForceMode.VelocityChange);
        }

        private void Rotated(Vector3 rotation)
        {
            Vector3 verticalRotation = verticalRotatingObject.localEulerAngles;
            verticalRotation.x = rotation.x;
            verticalRotatingObject.localEulerAngles = verticalRotation;

            Vector3 horizontalRotation = transform.localEulerAngles;
            horizontalRotation.y = rotation.y;
            transform.localEulerAngles = horizontalRotation;
        }

        private void OnDestroy()
        {
            _model.OnFire -= Fired;
            _model.OnRotate -= Rotated;
        }
    }
}
