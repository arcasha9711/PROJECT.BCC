using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace BCC
{
    public class PlayerCtrl : MonoBehaviour
    {
        public float moveSpeed = 3.0f;
        public float sprintSpeed = 5.0f;
        public float speedChangeRate = 10.0f;

        private Animator animator;
        private NavMeshAgent navMeshAgent;
        private float currentSpeed = 0.0f;

        private void Start()
        {
            animator = GetComponent<Animator>();
            navMeshAgent = GetComponent<NavMeshAgent>();

            if (navMeshAgent != null)
            {
                navMeshAgent.speed = moveSpeed;
            }
        }

        private void Update()
        {
            Move();
            UpdateAnimator();
        }

        void Move()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(horizontal, 0.0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetSpeed = moveSpeed;
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    targetSpeed = sprintSpeed;
                }

                currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * speedChangeRate);
                navMeshAgent.speed = currentSpeed;

                Vector3 moveDirection = direction * currentSpeed;
                navMeshAgent.Move(moveDirection * Time.deltaTime);

                // Rotate player to face the movement direction
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0.0f, targetAngle, 0.0f);
            }
            else
            {
                currentSpeed = Mathf.Lerp(currentSpeed, 0.0f, Time.deltaTime * speedChangeRate);
                navMeshAgent.speed = currentSpeed;
            }
        }

        void UpdateAnimator()
        {
            if (animator != null)
            {
                // Use magnitude of the input vector as speed
                float speed = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")).magnitude;
                animator.SetFloat("Speed", speed);

                // Optionally, update other parameters such as Direction if needed
                animator.SetFloat("Direction", Input.GetAxis("Horizontal"));
            }
        }
    }
}
