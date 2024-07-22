using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCC
{
    //광역피해 스크립트(원)
    public class SplashLogic : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            for (int i = 0; i < detectedObjects.Count; i++)
            {
                Gizmos.DrawLine(transform.position, detectedObjects[i].transform.position);
            }

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        public List<GameObject> detectedObjects = new List<GameObject>();
        public float radius = 3f;


        [ContextMenu("Detect")]
        public void DetectObjectsBySphere()
        {
            detectedObjects.Clear();
            Collider[] overlappedObjects = Physics.OverlapSphere(transform.position, radius);
            for (int i = 0; i < overlappedObjects.Length; i++)
            {
                Vector3 dir = transform.position - overlappedObjects[i].transform.position;
                Ray ray = new Ray(transform.position, dir.normalized);
                if (Physics.Raycast(ray, out RaycastHit hit, radius))
                {
                    if (hit.transform == overlappedObjects[i].transform)
                    {
                        detectedObjects.Add(overlappedObjects[i].gameObject);
                    }
                }
            }
        }
    }
}