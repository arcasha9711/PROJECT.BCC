using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCC
{
    public class CharacterBase : MonoBehaviour
    {
        public static List<CharacterBase> AllCharacters = new List<CharacterBase>();

        [field: SerializeField] public string CharacterID { get; private set; }

        public float currentHP;
        public float maxHP;


        public System.Action<float, float> OnTakeDamaged;

        private void Awake()
        {
            AllCharacters.Add(this);
        }

        private void OnDestroy()
        {
            AllCharacters.Remove(this);
        }

        private void Start()
        {
            currentHP = maxHP;
            CharacterHUDGroupUI.Instance.AddNewHUD(this);
        }


        public void TakeDamage(float damage)
        {
            currentHP -= damage;

            OnTakeDamaged?.Invoke(currentHP, maxHP);

            if (currentHP <= 0)
            {
                die();
            }
        }

        private void die()
        {
            Destroy(gameObject);
        }

    }
}
