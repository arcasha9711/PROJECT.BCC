using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BCC
{
    public class PlayerCtrl : MonoBehaviour
    {
        public float movespeed = 3.0f;
        public float sprintSpeed = 5.0f;
        public float speedChangeRate = 10.0f;


        private void Update()
        {
           GroupMove();

            
        }

        void GroupMove()
        {
            transform.Translate(Vector3.forward * Time.deltaTime, Space.World);
        }
    }
}
