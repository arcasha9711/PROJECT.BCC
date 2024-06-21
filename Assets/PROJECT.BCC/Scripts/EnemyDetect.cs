using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCC
{
    public class EnemyDetect : MonoBehaviour
    {
         public string enemyTag = "Enemy";

        // 적을 감지
        public delegate void EnemyDetectedHandler(GameObject enemy);
        public event EnemyDetectedHandler OnEnemyDetected;

        // 적이 콜라이더를 나갔을 때 호출될 이벤트
        public delegate void EnemyLostHandler(GameObject enemy);
        public event EnemyLostHandler OnEnemyLost;

        private void OnTriggerEnter(Collider other)
        {
            // 적 태그와 비교하여 적이 감지되었는지 확인
            if (other.CompareTag(enemyTag))
            {
                Debug.Log("적 감지" + other.name);
                OnEnemyDetected?.Invoke(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            // 적 태그와 비교하여 적이 콜라이더를 나갔는지 확인
            if (other.CompareTag(enemyTag))
            {
                Debug.Log("적 나감" + other.name);
                OnEnemyLost?.Invoke(other.gameObject);
            }
        }
    }
}
