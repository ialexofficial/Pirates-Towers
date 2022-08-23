using System;
using ScriptableObjects;

namespace Models
{
    public class EnemyModel
    {
        private int _health;
        private bool _dead;

        public event Action OnDamage;
        public event Action OnDie;

        public int Money { get; }

        public int Health
        {
            get => _health;
            private set
            {
                _health = value;
                OnDamage?.Invoke();
            }
        }

        public EnemyModel(int health, int money) =>
            (Health, Money) = (health, money);

        public void Damage(int damage)
        {
            Health -= damage;

            if (Health <= 0 && !_dead)
            {
                _dead = true;
                OnDie?.Invoke();
            }
        }
    }
}