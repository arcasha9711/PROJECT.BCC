using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCC
{
    public class Bullet : MonoBehaviour
    {
        public int damage;
        public float lifeTime = 2f;
        public float maxDistance = 50f;

        private Vector3 startPosition;


        private void Start()
        {
            startPosition = transform.position;
            Destroy(gameObject, lifeTime);
        }

        private void Update()
        {
            float traveledDistance = Vector3.Distance(startPosition, transform.position);
            if (traveledDistance >= maxDistance)
            {
                Destroy(gameObject);
            }
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
            {
                if (collision.gameObject.TryGetComponent(out CharacterBase character))
                {
                    character.TakeDamage(damage);
                }
            }

            Destroy(gameObject);
        }
    }
}
