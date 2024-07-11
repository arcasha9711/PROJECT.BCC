using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCC
{
    public class Slug : MonoBehaviour
    {
        public Transform bulletPos;
        public GameObject bullet;
        public float bulletSpeed = 50f;
        public float shootDelay = 1f;
        private bool isShooting = false;

        private SelectableCharacter selectableCharacter;

        private void Awake()
        {
            selectableCharacter = GetComponent<SelectableCharacter>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (selectableCharacter.IsSelected() && selectableCharacter.GetCurrentTarget() != null)
                {
                    Attack();
                }
            }
        }

        private void Attack()
        {
            if (!isShooting)
            {
                StartCoroutine(ShootBullets());
            }
        }

        private IEnumerator ShootBullets()
        {
            isShooting = true;
            yield return new WaitForSeconds(shootDelay);  // 1초 대기

            for (int i = 0; i < 8; i++)
            {
                ShootBullet();
            }

            isShooting = false;
        }

        private void ShootBullet()
        {
            GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation); // 총알 발사

            Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
            bulletRigid.velocity = bulletPos.forward * bulletSpeed; // 탄속도
        }
    }
}
