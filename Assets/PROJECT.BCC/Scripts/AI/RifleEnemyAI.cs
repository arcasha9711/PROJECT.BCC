using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCC
{
    public class RifleEnemyAI : EnemyController
    {
        private enum State
        {
            Idle,
            Move,
            Shot
        }

        private State _curState;
        private FSM _fsm;

        private void Start()
        {
            _curState = State.Idle;
            _fsm = new FSM(new IdleState(this));
        }

        private void Update()
        {
            switch (_curState)
            {
                case State.Idle:
                    if (CanSeePlayer())
                    {
                        if (CanAttackPlayer())
                            ChangeState(State.Shot);
                        else
                            ChangeState(State.Move);
                    }
                    break;
                case State.Move:
                    if (CanSeePlayer())
                    {
                        if (CanAttackPlayer())
                        {
                            ChangeState(State.Shot);
                        }
                    }
                    else
                    {
                        ChangeState(State.Idle);
                    }
                    break;
                case State.Shot:
                    if (CanSeePlayer())
                    {
                        if (!CanAttackPlayer())
                        {
                            ChangeState(State.Move);
                        }
                    }
                    else
                    {
                        ChangeState(State.Idle);
                    }
                    break;
            }

            _fsm.UpdateState();
        }

        private void ChangeState(State nextState)
        {
            _curState = nextState;
            switch (_curState)
            {
                case State.Idle:
                    _fsm.ChangeState(new IdleState(this));
                    break;
                case State.Move:
                    _fsm.ChangeState(new MoveState(this));
                    break;
                case State.Shot:
                    _fsm.ChangeState(new ShotState(this));
                    break;
            }
        }

       /* private bool CanSeePlayer()
        {
            // TODO:: 플레이어 탐지 구현
        }

        private bool CanAttackPlayer()
        {
            // TODO:: 사정거리 체크 구현
        }
       */
    }
}
