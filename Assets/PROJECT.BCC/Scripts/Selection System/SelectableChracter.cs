using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BCC
{
    public class SelectableCharacter : MonoBehaviour, ISelectable
    {
        public static List<SelectableCharacter> SpawnedCharacters = new List<SelectableCharacter>();

        public GameObject Selection => selectStateUI;
        public NavMeshAgent navMeshAgent;
        public Animator animator;

        public GameObject selectStateUI;

        private GameObject currentTarget;
        private bool isSelected = false;
        private bool isAttacking = false;

        private void Awake()
        {
            SpawnedCharacters.Add(this);
            animator = GetComponentInChildren<Animator>();
        }

        private void OnDestroy()
        {
            SpawnedCharacters.Remove(this);
        }

        public void Deselect()
        {
            isSelected = false;
            Selection.gameObject.SetActive(false);
            if (currentTarget != null)
            {
                var enemy = currentTarget.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.Deselect();
                }
            }
        }

        public void Select()
        {
            isSelected = true;
            Selection.gameObject.SetActive(true);
        }

        public void SetDestination(Vector3 targetDestination)
        {
            if (currentTarget)
            {
                var enemy = currentTarget.GetComponent<Enemy>();
                enemy.OnCharacterDead -= ResumeMoving;
                navMeshAgent.isStopped = false;
                currentTarget = null;
            }

            navMeshAgent.SetDestination(targetDestination);
        }

        public void SetTarget(GameObject enemy)
        {
            if (currentTarget != null)
            {
                var beforeEnemy = currentTarget.GetComponent<Enemy>();
                beforeEnemy.OnCharacterDead -= ResumeMoving;
            }

            currentTarget = enemy;
            var enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.Select();
                enemyScript.OnCharacterDead += ResumeMoving;
            }
            TargetEnemy(enemy);
        }

        private void Update()
        {
            if (navMeshAgent.isStopped && !isAttacking)
            {
                navMeshAgent.isStopped = false;
            }

            // 움직이는 애니메이션
            if (navMeshAgent.velocity != Vector3.zero)
            {
                Vector3 localVelocity = transform.InverseTransformDirection(navMeshAgent.velocity);
                animator.SetFloat("Horizontal", localVelocity.x);
                animator.SetFloat("Vertical", localVelocity.z);
            }
            else
            {
                animator.SetFloat("Horizontal", 0);
                animator.SetFloat("Vertical", 0);
            }

            // 공격 애니메이션
            if (currentTarget != null)
            {
                float distance = Vector3.Distance(transform.position, currentTarget.transform.position);
                if (distance <= navMeshAgent.stoppingDistance)
                {
                    // 적을 공격
                    animator.SetTrigger("doShot");
                    StopMoving();
                    isAttacking = true;
                }
            }
        }

        public void TargetEnemy(GameObject enemy)
        {
            Vector3 direction = (enemy.transform.position - transform.position).normalized;
            transform.LookAt(new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z));
            animator.SetTrigger("doShot");
            StopMoving();
            isAttacking = true;
        }

        public bool IsSelected()
        {
            return isSelected;
        }

        public GameObject GetCurrentTarget()
        {
            return currentTarget;
        }

        private void StopMoving()
        {
            if (navMeshAgent)
            {
                navMeshAgent.isStopped = true;
            }
        }

        private void ResumeMoving()
        {
            if (navMeshAgent)
            {
                navMeshAgent.isStopped = false;
            }
        }

        // 애니메이션 이벤트로 공격 애니메이션 끝날 때 호출
        public void OnAttackAnimationEnd()
        {
            ResumeMoving();
            isAttacking = false;
        }
    }
}
