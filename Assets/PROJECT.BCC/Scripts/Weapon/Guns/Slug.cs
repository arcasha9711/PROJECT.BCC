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
        public float spreadRange = 1;

        private bool isShooting = false;

        public override void Attack()
        {
            if (type == Type.Range)
            {
                StartCoroutine(ShootBullets());
            }
        }

        private IEnumerator ShootBullets()
        {
            isShooting = true;
            yield return new WaitForSeconds(1f);

            for (int i = 0; i < 8; i++)
            {
                Vector2 randomCircle = Random.insideUnitCircle * spreadRange;
                float randomValue = Random.Range(0, spreadRange);
                Vector3 randomAngle = new Vector3(0, randomValue, 0);
                ShootBullet(randomAngle);
            }

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
