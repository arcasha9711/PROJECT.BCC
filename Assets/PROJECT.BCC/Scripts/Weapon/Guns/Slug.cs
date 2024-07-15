using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCC
{
    public class Slug : PlayerCtrl
    {
        public GameObject bulletPrefab;
        public Transform bulletSpawnPoint;
        public float bulletSpeed = 10f;
        public float spreadRange = 1f;
        public float shotDelay = 1f;

        private bool isShooting = false;

        public override void Attack()
        {
            if (type == Type.Range && !isShooting)
            {
                StartCoroutine(ShootBullets());
            }
        }

        private IEnumerator ShootBullets()
        {
            isShooting = true;

            for (int i = 0; i < 8; i++)
            {
                Vector2 randomCircle = Random.insideUnitCircle * spreadRange;
                Vector3 randomAngle = new Vector3(randomCircle.x, randomCircle.y, 0);
                ShootBullet(randomAngle);
            }

            //딜레이
            yield return new WaitForSeconds(shotDelay);
            isShooting = false;
        }

        private void ShootBullet(Vector3 rotation)
        {
            Quaternion rot = bulletSpawnPoint.rotation * Quaternion.Euler(rotation);
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, rot);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                bulletRb.AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Impulse);
            }
        }
    }
}