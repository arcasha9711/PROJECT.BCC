using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

namespace BCC
{
    public class SelectableCharacter : MonoBehaviour, ISelectable
    {
        public static List<SelectableCharacter> SpawnedCharacters = new List<SelectableCharacter>();
        public GameObject Selection => selectStateUI;

        public GameObject selectStateUI;

        private GameObject currentTarget;
        private bool isSelected = false;
        private PlayerCtrl playerCtrl;

        private void Awake()
        {
            SpawnedCharacters.Add(this);
            playerCtrl = GetComponent<PlayerCtrl>();
        }

        private void OnDestroy()
        {
            SpawnedCharacters.Remove(this);
        }

        private void Update()
        {
            if (currentTarget != null)
            {
                
                bool isPossibleAttack = true;

                var enemy = currentTarget.GetComponent<Enemy>();
                if (enemy == null || !enemy.isAlive)
                {
                    isPossibleAttack = false;
                }
                if (!CanFire())
                {
                    isPossibleAttack = false;
                }

                if (isPossibleAttack)
                {
                    playerCtrl.Attack();
                }
            }
        }

        public void Deselect()
        {
            isSelected = false;
            Selection.gameObject.SetActive(false);
            if (currentTarget != null)
            {
                var enemy = currentTarget.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.Deselect();
                }
            }
        }

        public void Select()
        {
            isSelected = true;
            Selection.gameObject.SetActive(true);
        }

        public void SetTarget(GameObject enemy)
        {
            currentTarget = enemy;
            var enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.OnCharacterDead += ResetTarget;
                enemyScript.Select();
            }
            playerCtrl.SetTarget(enemy);
        }

        public void ResetTarget()
        {
            currentTarget = null;
        }

        public void SetDestination(Vector3 targetDestination)
        {
            playerCtrl.SetDestination(targetDestination);
            playerCtrl.StopAttack();
            ResetTarget();
        }

        public bool IsSelected()
        {
            return isSelected;
        }

        public GameObject GetCurrentTarget()
        {
            return currentTarget;
        }

        private bool CanFire()
        {
            float nextFireTime = 0f;
            float fireRate = 1f;
            int ammo = 10;

            if (Time.time >= nextFireTime && ammo > 0)
            {
                nextFireTime = Time.time + fireRate;
                return true;
            }
            return false;
        }
    }
}
