using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCC
{
    public class Sniper : PlayerCtrl
    {
        public GameObject bulletPrefab;
        public Transform bulletSpawnPoint;
        public float bulletSpeed = 50f;
        public float spreadRange = 0.1f;
        public float shotDelay = 2f;

        private bool isShooting = false;
        private int bulletsPerBurst = 1;

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

            for (int i = 0; i < bulletsPerBurst; i++)
            {
                Vector2 randomCircle = Random.insideUnitCircle * spreadRange;
                Vector3 randomAngle = new Vector3(randomCircle.x, randomCircle.y, 0);
                ShootBullet(randomAngle);
                yield return new WaitForSeconds(0.1f);
            }

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
                bulletRb.velocity = bulletSpawnPoint.forward * bulletSpeed;
            }
        }
    }
}
