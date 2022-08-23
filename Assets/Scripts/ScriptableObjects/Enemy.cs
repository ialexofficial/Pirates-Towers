using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObject/Enemy", order = 0)]
    public class Enemy : ScriptableObject
    {
        public int Health;
        public int Money;
        public float Speed;
    }
}