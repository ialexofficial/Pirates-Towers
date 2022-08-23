using System.Collections;
using ScriptableObjects;
using UnityEngine;
using BulletComponent = Components.Bullet;

namespace StateMachine
{
    [CreateAssetMenu(fileName = "FireState", menuName = "EnemyState/Fire", order = 1)]
    public class FireState : State
    {
        [SerializeField] private Bullet bulletData;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float reloadTime;
        [SerializeField] private Vector3 fireAim;

        private Coroutine _fireCoroutine;
        private Vector3 _machinePosition;

        public override void Enter()
        {
            _machinePosition = StateMachine.transform.position;

            float bulletStartSpeed = Mathf.Sqrt(
                Vector3.Distance(_machinePosition, fireAim) *
                Mathf.Abs(Physics.gravity.y) /
                Mathf.Pow(
                    Mathf.Sin(
                        Vector3.Distance(_machinePosition, fireAim) * Mathf.PI / 180
                    ),
                    2
                )
            );
            
            _fireCoroutine = StateMachine.StartCoroutine(Fire(bulletStartSpeed));
        }

        public override void Exit()
        {
            StateMachine.StopCoroutine(_fireCoroutine);
        }

        private IEnumerator Fire(float bulletStartSpeed)
        {
            while (true)
            {
                BulletComponent bullet = Instantiate(bulletPrefab, _machinePosition, Quaternion.identity).GetComponent<BulletComponent>();
                bullet.transform.LookAt(fireAim);
                bullet.scriptableObject = bulletData;
            
                bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletStartSpeed, ForceMode.VelocityChange);

                yield return new WaitForSeconds(reloadTime);
            }
        }
    }
}