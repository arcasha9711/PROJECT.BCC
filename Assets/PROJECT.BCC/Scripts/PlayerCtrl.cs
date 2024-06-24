using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace BCC
{
    public class PlayerCtrl : MonoBehaviour
    {
        //public NavMeshAgent navMeshAgent;
        //public Animator animator;

        private void Awake()
        {
            //animator = GetComponentInChildren<Animator>();
        }

        public float movespeed = 3.0f;

        private void Update()
        {
            /*if (navMeshAgent.velocity != Vector3.zero)
            {
                Vector3 localVelocity = transform.InverseTransformDirection(navMeshAgent.velocity);
                animator.SetFloat("Horizontal", localVelocity.x);
                animator.SetFloat("Vertical", localVelocity.z);
            }
            else
            {
                animator.SetFloat("Horizontal", 0);
                animator.SetFloat("Vertical", 0);
            }*/
            
        }

        public void GroupMove()
        {
            transform.Translate(Vector3.forward * movespeed * Time.deltaTime, Space.World);
        }
    }
}