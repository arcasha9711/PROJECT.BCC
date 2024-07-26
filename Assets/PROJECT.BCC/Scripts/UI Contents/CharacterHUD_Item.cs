using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BCC
{
    public class CharacterHUD_Item : MonoBehaviour
    {
        public CharacterBase LinkedCharacter => linkedCharacter;

        public Image hpBar;
        public Transform target;

        public Vector3 offsetOfViewport;
        private CharacterBase linkedCharacter;

        public void SetTarget(CharacterBase character)
        {
            linkedCharacter = character;
            SetValue(linkedCharacter.currentHP / linkedCharacter.maxHP);
            linkedCharacter.OnTakeDamaged += LinkedCharacterDamaged;
            this.target = character.transform;
        }

        private void LinkedCharacterDamaged(float currentHP, float maxHP)
        {
            SetValue(currentHP / maxHP);
        }

        // value 값은 0 ~ 1 사이로 들어와야함
        public void SetValue(float value)
        {
            hpBar.fillAmount = value;
        }

        private void Update()
        {
            if (target == null)
                return;

            Vector3 viewportPointOfPivot = Camera.main.WorldToViewportPoint(target.position);
            Vector3 viewportPosition = viewportPointOfPivot + offsetOfViewport;
            Vector3 finalPosition = Camera.main.ViewportToScreenPoint(viewportPosition);

            transform.position = finalPosition;
        }
    }
}
