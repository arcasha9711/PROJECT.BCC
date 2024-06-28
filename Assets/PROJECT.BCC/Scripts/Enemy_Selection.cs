using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCC
{
    public class Enemy_Selection : MonoBehaviour
    {
        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
