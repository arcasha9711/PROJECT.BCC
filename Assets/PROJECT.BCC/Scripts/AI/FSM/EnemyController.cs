using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCC
{
    public class EnemyController : MonoBehaviour
    {
       private enum State
        {
            Idle,
            Move,
            Shot
        }

        private State _state;

        private void Start()
        {
            _state = State.Idle;
        }

        private void Update()
        {
            switch(_state)
            {
                case State.Idle:
                    break;
                case State.Move:
                    break;
                case State.Shot:
                    break;

            }
        }
    }
}
