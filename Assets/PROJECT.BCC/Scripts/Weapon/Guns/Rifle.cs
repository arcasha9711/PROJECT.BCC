using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCC
{
    public class Rifle : PlayerCtrl
    {
        public GameObject bulletPrefab;
        public Transform bulletSpawnPoint;
        public float bulletSpeed = 20f;
        public float spreadRange = 0.2f;
        public float shotDelay = 1f;
        public float range = 15f; // 사거리

        private int bulletsPerBurst = 10;
        private float fireRateTime = 0;

        private IEnumerator fireCoroutine = null;

        public override void Attack()
        {
            if (type == Type.Range)
            {
                if (IsTargetInRange())
                {
                    ShootBullets();
                }
                else
                {
                    if (selectableCharacter.GetCurrentTarget() != null)
                    {
                        MoveToTarget(); // 타겟을 다시 추적
                    }
                    else
                    {
                        navMeshAgent.isStopped = false; // 이동 재개
                    }
                }
            }
        }

        protected override void Update()
        {
            base.Update();

            fireRateTime -= Time.deltaTime;
        }

        private bool IsTargetInRange()
        {
            if (selectableCharacter.GetCurrentTarget() == null)
                return false;

            return Vector3.Distance(transform.position, selectableCharacter.GetCurrentTarget().transform.position) <= range;
        }

        private void MoveToTarget()
        {
            if (selectableCharacter.GetCurrentTarget() != null)
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(selectableCharacter.GetCurrentTarget().transform.position);
            }
        }

        public override void StopAttack()
        {
            if (fireCoroutine != null)
            {
                StopCoroutine(fireCoroutine);
            }
        }

        private void ShootBullets()
        {
            if (fireRateTime > 0)
                return;

            if (fireCoroutine != null)
            {
                StopCoroutine(fireCoroutine);
            }

            fireCoroutine = FireCoroutine();
            StartCoroutine(fireCoroutine);
            fireRateTime = shotDelay;
        }

        IEnumerator FireCoroutine()
        {
            for (int i = 0; i < bulletsPerBurst; i++)
            {
                Vector2 randomCircle = Random.insideUnitCircle * spreadRange;
                Vector3 randomAngle = new Vector3(randomCircle.x, randomCircle.y, 0);
                ShootBullet(randomAngle);

                yield return new WaitForSeconds(0.1f);
            }
        }

        private void ShootBullet(Vector3 rotation)
        {
            Quaternion rot = bulletSpawnPoint.rotation * Quaternion.Euler(rotation);
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, rot);

            //Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            //if (bulletRb != null)
            //{
            //    bulletRb.velocity = bulletSpawnPoint.forward * bulletSpeed;
            //}
        }
    }
}
