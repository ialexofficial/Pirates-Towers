using UnityEngine;

namespace StateMachine
{
    [CreateAssetMenu(fileName = "MoveState", menuName = "EnemyState/Move", order = 1)]
    public class MoveState : State
    {
        public Vector3 aimPosition;
        public float speed;
        public float moveDeadZone;

        private Transform _machineTransform;

        public override void Enter()
        {
            _machineTransform = StateMachine.transform;
        }

        public override void Update()
        {
            if (Vector3.Distance(_machineTransform.position, aimPosition) > moveDeadZone)
            {
                _machineTransform.position = Vector3.Slerp(_machineTransform.position, aimPosition, Time.deltaTime * speed);
            }
            else
            {
                StateMachine.SetNextState(nextState);
            }
        }
    }
}