using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCC
{
    public abstract class SceneBase : MonoBehaviour
    {
        public virtual IEnumerator OnStartScene()
        {
            yield return null;
        }

        public virtual IEnumerator OnEndScene()
        {
            yield return null;
        }
    }
}
