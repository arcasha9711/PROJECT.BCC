using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace BCC
{
    public class RagdollController : MonoBehaviour
    {
        public Rigidbody[] rigidbodies;
        public Collider[] colliders;
        public Animator animator;

        [Header("Ragdoll Force Active")]
        public HumanBodyBones targetBone;
        public Vector3 forceDirection;
        public float forcePower;

        private void Awake()
        {
            rigidbodies = GetComponentsInChildren<Rigidbody>();
            colliders = GetComponentsInChildren<Collider>();
            animator = GetComponent<Animator>();

            SetRagdollActive(false);
        }

        [ContextMenu("Active Ragdoll With Power")]
        public void ActiveForceRagdollWithPower() // 에디터에서 임시로 확인용 Menu 함수

        {   
            //public에 선언 된 임시 변수들을 활용하여 Ragdoll에 Force를 줘서 활성화 시킨다.
            ForceActiveRagdollWithPower(targetBone, forceDirection, forcePower); 
        }

        public void ForceActiveRagdollWithPower(HumanBodyBones bone, Vector3 direction, float power)
        {
            SetRagdollActive(true); // Ragdoll을 활성화 시킨다.

            // rigidbody로 인식 된 모든 몸체에 힘을 가한다.
            foreach (var rigid in rigidbodies)
            {
                rigid.AddForce(direction * power, ForceMode.Force);
            }

            //var boneTransform = animator.GetBoneTransform(bone);           // Animator를 통해서, HumanBodyBones의 Transform을 가져온다.
            //var targetRigidbody = boneTransform.GetComponent<Rigidbody>(); // 가져온 Bone Transform에서 Rigidbody를 가져온다.
            //targetRigidbody.AddForce(direction * power, ForceMode.Force);  // 가져온 Rigidbody에 힘을 가한다.
        } 


        public void SetRagdollActive(bool isActive)
        {
            if(isActive)
            {
                animator.enabled = false;
            }

            foreach (var rb in rigidbodies)
            {
                rb.isKinematic = !isActive;
            }

            foreach (var col in colliders)
            {
                col.enabled = isActive;
            }
        }
    }
}