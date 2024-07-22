using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCC
{
    public class CharacterHUDGroupUI : MonoBehaviour
    {
        public static CharacterHUDGroupUI Instance { get; private set; } = null;


        public CharacterHUD_Item itemPrefab;
        public List<CharacterHUD_Item> createdItems = new List<CharacterHUD_Item>();


        private void Awake()
        {
            Instance = this;
            itemPrefab.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        public void AddNewHUD(CharacterBase target)
        {
            var newHudItem = Instantiate(itemPrefab, transform);
            newHudItem.gameObject.SetActive(true);
            newHudItem.SetTarget(target);
        }
    }
}
