using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace BCC
{
    public class PlayerMove : MonoBehaviour
    {
        NavMeshAgent agent;

        private Animator animator;
        private float animationBlend;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            move();
            animator.SetFloat("speed", animationBlend);

        }

        void move()
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    agent.SetDestination(hit.point);

                }

                //Todo: Click Animation
            }
        }
    }
}
