using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCC
{
    public class Bullet : MonoBehaviour
    {
        public int damage;

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag == "Wall")
            {
                Destroy(gameObject, 3);
            }

            //다른 것
        }
    }
}
