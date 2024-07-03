using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCC
{
    public class PlayerCtrl : MonoBehaviour
    {
        public enum Type { Range }
        public Type type;
        public Transform bulletPos;
        public GameObject bullet;

        private SelectableCharacter selectableCharacter;

        private void Awake()
        {            
            selectableCharacter = transform.root.GetComponent<SelectableCharacter>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // 캐릭터가 선택되고, 적에게 공격할 때에만 총알이 나가도록
                if (selectableCharacter.IsSelected() && selectableCharacter.GetCurrentTarget() != null)
                {
                    Attack();
                }
            }
        }

        public void Attack()
        {
            if (type == Type.Range)
            {
                StartCoroutine(Shot());
            }
        }

        IEnumerator Shot()
        {
            GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation); // 총알 발사

            Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
            bulletRigid.velocity = bulletPos.forward * 50; // 탄속도

            yield return null;
        }
    }
}

