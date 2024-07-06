using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BCC
{
    public class CharacterUI : MonoBehaviour
    {
        public Image hpBar;

        public CharacterBaseCallback linkedCharacter;

        private void Start()
        {
            linkedCharacter.onDamageCallback += RefreshHpBar; // Delegate에 Chain(체인)을 건다.
            linkedCharacter.onDamageAction += RefreshHpBar; // Delegate에 Chain(체인)을 건다.
        }


        public void RefreshHpBar(float currenHP, float maxHp)
        {
            hpBar.fillAmount = currenHP / maxHp;
        }
    }
}
