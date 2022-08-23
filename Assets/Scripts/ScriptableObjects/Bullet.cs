using UnityEngine;
using Views;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Bullet", menuName = "ScriptableObject/Bullet", order = 0)]
    public class Bullet : ScriptableObject
    {
        public float Weight;
        [SerializeField] private int damage;
        [SerializeField] private bool isExplosive;
        [SerializeField] private bool isDouble;

        public void Damage(IDamagable damagable)
        {
            damagable.Damage(damage);
        }
    }
}