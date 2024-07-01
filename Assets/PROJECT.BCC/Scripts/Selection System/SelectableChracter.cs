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
            Selection.gameObject.SetActive(true);
        }

        public void SetDestination(Vector3 targetDestination)
        {
            navMeshAgent.SetDestination(targetDestination);
        }

        public void SetTarget(GameObject enemy)
        {
            currentTarget = enemy;
            var enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.Select();
            }
            TargetEnemy(enemy);
        }

        private void Update()
        {
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
                }
            }
        }

        public void TargetEnemy(GameObject enemy)
        {
            Vector3 direction = (enemy.transform.position - transform.position).normalized;
            transform.LookAt(new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z));
            animator.SetTrigger("doShot");
        }
    }
}
