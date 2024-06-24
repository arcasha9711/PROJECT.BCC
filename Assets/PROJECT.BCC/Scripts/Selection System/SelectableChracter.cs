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
            
        }
    }
}