using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCC
{
    public class CharacterBaseCallback : MonoBehaviour
    {
        public float currentHP;
        public float maxHP;

        public delegate void OnDamage(float currnetHP, float maxHP);
        public delegate void OnCharacterDead();

        public event OnDamage onDamageEvent; //event를 쓰면 chain을 외부클래스에서 못하게 막을 수 있다.
        public OnDamage onDamageCallback;
        public OnCharacterDead onCharacterDead;

        public System.Action<float, float> onDamageAction;


        public void Damage(float damage)
        {
            currentHP -= damage;

            onDamageCallback(currentHP, maxHP);

            if(currentHP <= 0)
            {
                onCharacterDead(); // Chain 걸려있는 애들이 다 호출
                Destroy(gameObject);
            }
        }
        [ContextMenu("Damage Debug")]
        public void DemageDebugButton()
        {
            Damage(20);
        }
    }
}
