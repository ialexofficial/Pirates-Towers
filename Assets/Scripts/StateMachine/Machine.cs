using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class Machine : MonoBehaviour
    {
        protected Animator _animator;
        private State _currentState;
        private Coroutine _coroutine;
        private readonly Dictionary<State, State> _statesDict = new Dictionary<State, State>();

        public void SetStateArray(State[] states)
        {
            foreach (var state in states)
            {
                AddState(state);
            }

            SetNextState(states[0]);
            _coroutine = StartCoroutine(UpdateCoroutine());
        }

        public void AddState(State state)
        {
            if (_statesDict.ContainsKey(state))
                return;

            State newState = Instantiate(state);
            newState.StateMachine = this;
            newState.Animator = _animator ??= GetComponent<Animator>();

            _statesDict[state] = newState;
        }

        public void SetNextState(State nextState)
        {
            _currentState?.Exit();
            
            _currentState = _statesDict[nextState];
            _currentState.Enter();
        }

        private IEnumerator UpdateCoroutine()
        {
            while (true)
            {
                _currentState.Update();
                yield return null;
            }
        }
    }
}