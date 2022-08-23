using Models;
using UnityEngine;
using UnityEngine.Events;

namespace Views
{
    public class WallView : MonoBehaviour, IDamagable
    {
        public UnityEvent OnLose = new UnityEvent();
        public UnityEvent<int> OnDamage = new UnityEvent<int>();
        
        [SerializeField] private int health = 3;
        
        private WallModel _model;

        public void Damage(int damage)
        {
            _model.Damage(damage);
        }
        
        private void Awake()
        {
            _model = new WallModel(health);
            
            _model.OnLose += Lost;
            _model.OnDamage += Damaged;
        }

        private void Lost()
        {
            OnLose?.Invoke();
        }

        private void Damaged()
        {
            OnDamage?.Invoke(_model.Health);
        }

        private void OnDestroy()
        {
            _model.OnLose -= Lost;
            _model.OnDamage -= Damaged;
        }
    }
}