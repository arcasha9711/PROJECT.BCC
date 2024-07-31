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
        public float viewDistance = 10f;  // 플레이어를 감지할 수 있는 거리
        public float viewAngle = 90f;     // 시야각
        public LayerMask playerMask;      // 플레이어 레이어 마스크
        public LayerMask obstacleMask;    // 장애물 체크를 위한 레이어 마스크

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

        private bool CanSeePlayer()
        {
            Collider[] playersInRange = Physics.OverlapSphere(transform.position, viewDistance, playerMask);
            foreach (Collider playerCollider in playersInRange)
            {
                Transform playerTransform = playerCollider.transform;
                Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;

                // 플레이어가 시야각 안에 있는지 확인
                if (Vector3.Angle(transform.forward, directionToPlayer) < viewAngle / 2)
                {
                    // 장애물 확인
                    if (!Physics.Linecast(transform.position, playerTransform.position, obstacleMask))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CanAttackPlayer()
        {
            Collider[] playersInRange = Physics.OverlapSphere(transform.position, viewDistance, playerMask);
            foreach (Collider playerCollider in playersInRange)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, playerCollider.transform.position);
                if (distanceToPlayer < viewDistance)
                {
                    return true;
                }
            }
            return false;
        }

        private void OnDrawGizmosSelected()
        {
            // 시야 범위를 표시
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, viewDistance);

            Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle / 2, 0) * transform.forward * viewDistance;
            Vector3 rightBoundary = Quaternion.Euler(0, viewAngle / 2, 0) * transform.forward * viewDistance;

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
            Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
        }
    }
}
