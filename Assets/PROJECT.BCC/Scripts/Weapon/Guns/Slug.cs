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
        public float range = 7f; // 사거리

        private int bulletsPerBurst = 8;
        private float fireRateTime = 0;

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
                StartCoroutine(WaitUntilInRange());
            }
        }

        private IEnumerator WaitUntilInRange()
        {
            while (!IsTargetInRange())
            {
                yield return null;
            }

            navMeshAgent.isStopped = true; // 사거리 내에 도달하면 이동 멈춤
            ShootBullets();
        }

        private void ShootBullets()
        {
            if (fireRateTime > 0)
                return;

            for (int i = 0; i < bulletsPerBurst; i++)
            {
                
                Vector2 randomCircle = Random.insideUnitCircle * spreadRange;
                Vector3 randomAngle = new Vector3(randomCircle.x, randomCircle.y, 0);
                ShootBullet(randomAngle);
            }

            fireRateTime = shotDelay;
        }

        private void ShootBullet(Vector3 rotation)
        {
            Quaternion rot = bulletSpawnPoint.rotation * Quaternion.Euler(rotation);
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, rot);
            
            //Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            //if (bulletRb != null)
            //{
            //    bulletRb.velocity = bullet.transform.forward * bulletSpeed;
            //}
        }
    }
}
