using JetBrains.Annotations;
using UnityEngine;

namespace StateMachine
{
    public abstract class State : ScriptableObject
    {
        [SerializeField] [CanBeNull] protected State nextState;
        
        public Machine StateMachine { get; set; }
        [CanBeNull] public Animator Animator { get; set; }
        
        public abstract void Enter();

        public virtual void Update()
        {
        }

        public virtual void Exit()
        {
        }
    }
}