using UnityEngine;

namespace StateMachine
{
    [CreateAssetMenu(fileName = "DieState", menuName = "EnemyState/Die", order = 1)]
    public class DieState : State
    {
        public override void Enter()
        {
            Animator.SetTrigger("Dead");
        }
    }
}