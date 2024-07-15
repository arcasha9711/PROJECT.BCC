using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCC
{
    public class Bullet : MonoBehaviour
    {
        public int damage;
        public float lifeTime = 3f;
        

        private void Start()
        {
            Destroy(gameObject, lifeTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Destroy(gameObject);
        }

        private void Exception()
        {
            
        }
    }
}
