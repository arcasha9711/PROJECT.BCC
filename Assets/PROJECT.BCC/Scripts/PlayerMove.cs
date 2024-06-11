using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace BCC
{
    public class PlayerMove : MonoBehaviour
    {
        NavMeshAgent agent;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if(Input.GetKey(KeyCode.Mouse1))
            {
               Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if(Physics.Raycast(ray, out RaycastHit hit))
                {
                    agent.SetDestination(hit.point);
                }
            }
        }

    }
}
