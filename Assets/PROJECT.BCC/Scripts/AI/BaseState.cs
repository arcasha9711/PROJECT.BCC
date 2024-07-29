using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCC
{
    public abstract class BaseState
    {
        protected EnemyController _enemy;

        protected BaseState(EnemyController enemy)
        {
            _enemy = enemy;
        }

        public abstract void OnStateEnter();
        public abstract void OnStateUpdate();
        public abstract void OnStateExit();
    }
}
