using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCC
{
    public class PlayerCytl : MonoBehaviour
    {
        public enum Type { Range }
        public Type type;
        public Transform bulletPos;
        public GameObject bullet;
        public GameObject target;
        public Transform enemy;

       

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
            }

            transform.LookAt(enemy);
        }

        public void Attack()
        {
            if (type == Type.Range)
            {
                StartCoroutine(Shot());
            }
        }

        public void LookAt()
        {
            Vector3 vector = target.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(vector).normalized;
        }


        IEnumerator Shot()
        {
            GameObject intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation); // 총알 발사

            Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
            bulletRigid.velocity = bulletPos.forward * 50; // 탄속도

            yield return null;
        }
    }
}
