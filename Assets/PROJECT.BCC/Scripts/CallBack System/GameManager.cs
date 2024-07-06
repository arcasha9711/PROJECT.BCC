using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCC
{
    public class GameManager : MonoBehaviour
    {
        public bool IsGameOver;

        public CharacterBaseCallback PlayerCharacter;

        private void Start()
        {
            PlayerCharacter.onCharacterDead += GameOverCheck; // 캐릭터 자체에 이벤트를 만들어 둔다.
        }

        public void GameOverCheck()
        {
            // To do : Game over
            Debug.Log("Game Over");
        }
    }
}
