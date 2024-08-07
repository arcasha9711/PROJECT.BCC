using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCC
{
    public class SniperEnemy : EnemyAI
    {
        private Sniper sniper;

        private void Awake()
        {
            sniper = GetComponent<Sniper>();
            sniper.enabled = false; // 플레이어 스크립트 기능을 비활성화
        }

        protected override void Attack()
        {
            sniper.Attack(); // 기존 Sniper 스크립트의 Attack 메서드 호출
            targetPlayer = null; // 사격 후 타겟팅 해제
        }

        protected override float GetAttackRange()
        {
            return sniper.range; // 기존 Sniper 스크립트의 사거리 사용
        }
    }
}
