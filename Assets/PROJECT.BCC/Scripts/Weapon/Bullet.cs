using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCC
{
    public class Bullet : MonoBehaviour
    {
        //public void SetBulletSpeed(float speed)
        //{
        //    this.speed = speed;
        //}

        public float speed;
        public int damage;
        public float lifeTime = 0.3f; // 사거리 처리
        public float maxDistance = 50f;

        private Vector3 startPosition;


        private void Start()
        {
            startPosition = transform.position;
            Destroy(gameObject, lifeTime);
        }

        private void Update()
        {
            transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);

            float traveledDistance = Vector3.Distance(startPosition, transform.position);
            if (traveledDistance >= maxDistance)
            {
                Destroy(gameObject);
            }
        }


        private void OnTriggerEnter(Collider collider)
        {
            if(collider.gameObject.CompareTag("Player"))
            {
                return;
            }

            if (collider.gameObject.CompareTag("Enemy"))
            {
                if (collider.gameObject.TryGetComponent(out CharacterBase character))
                {
                    character.TakeDamage(damage);
                }
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject); //다른 오브젝트에 닿아도 총알 파괴
            }
        }
    }
}
