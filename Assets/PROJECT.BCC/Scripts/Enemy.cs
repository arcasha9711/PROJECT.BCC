using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCC
{
    public class Enemy : MonoBehaviour
    {
        public GameObject SelectionUI;

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
    }
}
