using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BCC
{
    public class PlayerCtrl : MonoBehaviour
    {
        public enum Type { Range }
        public Type type;
        public Transform bulletPos;
        public GameObject bullet;
        protected NavMeshAgent navMeshAgent;
        protected Animator animator;

        protected SelectableCharacter selectableCharacter;
        private bool isAttacking = false;

        protected virtual void Awake()
        {
            selectableCharacter = GetComponent<SelectableCharacter>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponentInChildren<Animator>();
        }

        protected virtual void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (selectableCharacter.IsSelected() && selectableCharacter.GetCurrentTarget() != null)
                {
                    Attack();
                }
            }

            HandleMovement();
        }

        protected void HandleMovement()
        {
            if (navMeshAgent == null) return;

            if (navMeshAgent.isStopped && !isAttacking)
            {
                navMeshAgent.isStopped = false;
            }

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

            if (selectableCharacter.GetCurrentTarget() != null)
            {
                float distance = Vector3.Distance(transform.position, selectableCharacter.GetCurrentTarget().transform.position);
                if (distance <= navMeshAgent.stoppingDistance)
                {
                    animator.SetTrigger("doShot");
                    StopMoving();
                    isAttacking = true;
                }
            }
        }

        public virtual void Attack()
        {
            if (type == Type.Range)
            {
                StartCoroutine(Shot());
            }
        }

        protected virtual IEnumerator Shot()
        {
            GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);

            Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
            bulletRigid.velocity = bulletPos.forward * 50;

            yield return null;

        }

        public void SetDestination(Vector3 targetDestination)
        {
            if (selectableCharacter.GetCurrentTarget())
            {
                navMeshAgent.isStopped = false;
            }

            if (navMeshAgent != null)
            {
                navMeshAgent.SetDestination(targetDestination);
            }

            StartCoroutine(InternalLookatFirstCorner());
            IEnumerator InternalLookatFirstCorner()
            {
                yield return new WaitUntil(() => { return navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete; });

                if (navMeshAgent.path.corners.Length >= 1)
                {
                    Vector3 firstCornerPosition = navMeshAgent.path.corners[1];
                    Vector3 dir = firstCornerPosition - transform.position;
                    transform.forward = dir;
                }
            }
        }

        public void SetTarget(GameObject enemy)
        {
            TargetEnemy(enemy);
        }

        protected virtual void TargetEnemy(GameObject enemy)
        {
            Vector3 direction = (enemy.transform.position - transform.position).normalized;
            transform.LookAt(new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z));
            animator.SetTrigger("doShot");
            StopMoving();
            isAttacking = true;
        }

        protected void StopMoving()
        {
            if (navMeshAgent != null)
            {
                navMeshAgent.SetDestination(transform.position);
            }
        }

        protected void ResumeMoving()
        {
            if (navMeshAgent != null)
            {
                navMeshAgent.SetDestination(transform.position);
            }
        }

        public void OnAttackAnimationEnd()
        {
            ResumeMoving();
            isAttacking = false;
        }
    }
}
