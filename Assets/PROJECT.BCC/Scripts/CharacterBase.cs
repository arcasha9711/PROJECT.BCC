using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCC
{
    public class CharacterBase : MonoBehaviour
    {
        public static List<CharacterBase> AllCharacters = new List<CharacterBase>();

        [field: SerializeField] public string CharacterID { get; private set; }

        private void Awake()
        {
            AllCharacters.Add(this);
        }

        private void OnDestroy()
        {
            AllCharacters.Remove(this);
        }
    }
}
