using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCC
{
    public abstract class EnemyAI : MonoBehaviour
    {
        public float viewDistance = 10f; // 플레이어를 감지할 수 있는 거리
        public float moveSpeed = 2f; // 이동 속도
        public float viewAngle = 90f; // 시야각
        public LayerMask playerMask; // 플레이어 레이어 마스크
        public LayerMask obstacleMask; // 장애물 체크를 위한 레이어 마스크

        protected Transform targetPlayer;

        protected virtual void Update()
        {
            if (CanSeePlayer())
            {
                MoveTowardsPlayer();

                if (IsPlayerInRange())
                {
                    Attack();
                }
            }
        }

        protected bool CanSeePlayer()
        {
            Collider[] playersInRange = Physics.OverlapSphere(transform.position, viewDistance, playerMask);
            foreach (Collider playerCollider in playersInRange)
            {
                Transform playerTransform = playerCollider.transform;
                Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToPlayer) < viewAngle / 2)
                {
                    if (!Physics.Linecast(transform.position, playerTransform.position, obstacleMask))
                    {
                        targetPlayer = playerTransform;
                        return true;
                    }
                }
            }
            return false;
        }

        protected bool IsPlayerInRange()
        {
            if (targetPlayer == null) return false;

            float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.position);
            return distanceToPlayer <= GetAttackRange();
        }

        protected void MoveTowardsPlayer()
        {
            if (targetPlayer == null) return;

            Vector3 direction = (targetPlayer.position - transform.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, targetPlayer.position, moveSpeed * Time.deltaTime);

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * moveSpeed);
        }

        protected abstract void Attack();
        protected abstract float GetAttackRange();
    }
}
