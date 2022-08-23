using System;

namespace Models
{
    public class WallModel
    {
        private int _health;

        public event Action OnDamage;
        public event Action OnLose;

        public int Health
        {
            get => _health;
            private set
            {
                _health = value;
                OnDamage?.Invoke();
            }
        }

        public WallModel(int health) =>
            _health = health;

        public void Damage(int damage)
        {
            Health -= damage;
            
            if(Health == 0)
                OnLose?.Invoke();
        }
    }
}