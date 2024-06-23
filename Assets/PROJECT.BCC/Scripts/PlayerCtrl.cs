using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BCC
{
    public class PlayerCtrl : MonoBehaviour
    {
        public float movespeed = 3.0f;

        private void Update()
        {
            GroupMove();
        }

        void GroupMove()
        {
            transform.Translate(Vector3.forward * movespeed * Time.deltaTime, Space.World);
        }
    }
}