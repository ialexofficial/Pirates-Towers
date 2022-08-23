using Models;
using StateMachine;
using UnityEngine;
using UnityEngine.Events;

namespace Views
{
    [RequireComponent(typeof(Animator))]
    public class EnemyView : Machine, IDamagable
    {
        public UnityEvent OnDamage = new UnityEvent();
        public UnityEvent<int> OnDie = new UnityEvent<int>();

        [SerializeField] private State dieState;
        [SerializeField] private int health;
        [SerializeField] private int money;

        private EnemyModel _model;
        private Vector3 _aimPosition;

        public void Damage(int damage)
        {
            _model.Damage(damage);
        }

        private void Awake()
        {
            _model = new EnemyModel(health, money);

            _model.OnDamage += Damaged;
            _model.OnDie += Dead;
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void Damaged()
        {
            OnDamage?.Invoke();
        }

        private void Dead()
        {
            AddState(dieState);
            SetNextState(dieState);
            OnDie?.Invoke(_model.Money);
        }

        private void OnDestroy()
        {
            _model.OnDamage -= Damaged;
            _model.OnDie -= Dead;
        }
    }
}