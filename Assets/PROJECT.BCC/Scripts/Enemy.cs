using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCC
{
    public class Enemy : MonoBehaviour, ISelectable
    {
        public GameObject SelectionUI;

        public GameObject Selection => throw new System.NotImplementedException();

        public System.Action OnCharacterDead;

        private void Awake()
        {
            SelectionUI.SetActive(false);
        }

        public void Select()
        {
            SelectionUI.SetActive(true);
        }

        public void Deselect()
        {
            SelectionUI.SetActive(false);
        }

        private void OnDestroy()
        {
            OnCharacterDead?.Invoke();
        }
    }
}
