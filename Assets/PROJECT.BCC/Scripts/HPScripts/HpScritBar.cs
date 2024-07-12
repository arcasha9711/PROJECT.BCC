using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCC
{
    public class HpScritBar : MonoBehaviour
    {
        public float currentHp;
        public float maxHp;

        public delegate void OnDamage(float currnetHP, float maxHP);
        public delegate void OnCharacterDead();


        public event OnDamage onDamageEvent; //event를 쓰면 chain을 외부클래스에서 못하게 막을 수 있다.
        public OnDamage onDamageCallback;
        public OnCharacterDead onCharacterDead;

        public System.Action<float, float> onDamageAction;


    }
}
