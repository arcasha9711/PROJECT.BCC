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
        }

        public void Select()
        {
            Selection.gameObject.SetActive(true);
        }

        public void SetDestination(Vector3 targetDestination)
        {
           navMeshAgent.SetDestination(targetDestination);
        }
        private void Update()
        {
            //클릭시 움직이는 애니메이션
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

            //총쏘는 애니메이션
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                animator.SetTrigger("doShot");
            }                        
        }

        public void TargetEnemy(GameObject enemy)
        {
            // 기존 선택 상태를 비활성화
            Deselect();

            // Enemy_Selection 활성화
            Enemy_Selection enemySelection = enemy.GetComponent<Enemy_Selection>();
            if (enemySelection != null)
            {
                enemySelection.Activate();
            }

            // 적을 향해 회전하고 공격 애니메이션을 재생합니다.
            Vector3 direction = (enemy.transform.position - transform.position).normalized;
            transform.LookAt(new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z));
            animator.SetTrigger("doShot");
        }
    }
}