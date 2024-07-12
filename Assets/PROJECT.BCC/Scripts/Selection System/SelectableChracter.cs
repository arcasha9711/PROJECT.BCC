using System.Collections;
using System.Collections.Generic;
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
                enemyScript.Select();
            }
            playerCtrl.SetTarget(enemy);
        }

        public void SetDestination(Vector3 targetDestination)
        {
            playerCtrl.SetDestination(targetDestination);
        }

        public bool IsSelected()
        {
            return isSelected;
        }

        public GameObject GetCurrentTarget()
        {
            return currentTarget;
        }
    }
}
