using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCC
{
    public class QuaterViewCameraSystem : MonoBehaviour
    {
        public Transform FollowTarget { get; set; } // Null

        public float cameraRotationSpeed = 3f;
        public float cameraMoveSpeed = 3f;

        public Camera mainCamera;
        public Transform cameraPivot;
        public Cinemachine.CinemachineVirtualCamera quaterViewCamera;


        public float cameraBorderThickness = 0.2f;

        private void Update()
        {
            bool isSomeoneSelect = false;
            if (Input.GetKey(KeyCode.Alpha1))
            {
                // To do : 
                // FollowTarget = Character01.transform;
                var target = CharacterBase.AllCharacters.Find(x => x.CharacterID.Equals("Player_01"));
                FollowTarget = target.transform;
                isSomeoneSelect = true;
            }

            if (Input.GetKey(KeyCode.Alpha2))
            {
                // FollowTarget = Character02.transform;
                var target = CharacterBase.AllCharacters.Find(x => x.CharacterID.Equals("Player_02"));
                FollowTarget = target.transform;
                isSomeoneSelect = true;
            }

            if (Input.GetKey(KeyCode.Alpha3))
            {
                // FollowTarget = Character03.transform;
                var target = CharacterBase.AllCharacters.Find(x => x.CharacterID.Equals("Player_03"));

                FollowTarget = target.transform;
                isSomeoneSelect = true;
            }

            if (isSomeoneSelect == false)
            {
                FollowTarget = null;
            }

            float rotateDirection = 0f;
            if (Input.GetKey(KeyCode.Q))
            {
                rotateDirection = -1;
                Vector3 originalRot = quaterViewCamera.transform.localRotation.eulerAngles;
                quaterViewCamera.transform.localRotation = Quaternion.Euler(originalRot +
                    (Vector3.up * rotateDirection * cameraRotationSpeed * Time.deltaTime));
            }

            if (Input.GetKey(KeyCode.E))
            {
                rotateDirection = 1;
                Vector3 originalRot = quaterViewCamera.transform.localRotation.eulerAngles;
                quaterViewCamera.transform.localRotation = Quaternion.Euler(originalRot +
                    (Vector3.up * rotateDirection * cameraRotationSpeed * Time.deltaTime));
            }


            Vector3 cameraForward = mainCamera.transform.forward;
            Vector3 cameraRight = mainCamera.transform.right;
            cameraForward.y = 0;
            cameraRight.y = 0;

            if (FollowTarget != null)
            {
                cameraPivot.position = FollowTarget.position;
            }
            else
            {
                UpdateScreenBorderCheck(cameraForward, cameraRight);
            }            
        }

        private void UpdateScreenBorderCheck(Vector3 cameraForward, Vector3 cameraRight)
        {
            // Mouse Screen Border Check 뷰포트
            Vector3 mousePosition = Input.mousePosition;
            Vector3 viewportMousePosition = mainCamera.ScreenToViewportPoint(mousePosition);

            // Left Check
            if (viewportMousePosition.x < cameraBorderThickness)
            {
                // To do : Camera Move To Left
                cameraPivot.Translate(cameraRight * cameraMoveSpeed * (-1) * Time.deltaTime, Space.World);

                // Top
                if (viewportMousePosition.y > 1 - cameraBorderThickness)
                {
                    // Top Left
                    cameraPivot.Translate(cameraForward * cameraMoveSpeed * Time.deltaTime, Space.World);
                }
                // Bottom
                else if (viewportMousePosition.y < cameraBorderThickness)
                {
                    // Bottom Left
                    cameraPivot.Translate(cameraForward * cameraMoveSpeed * (-1) * Time.deltaTime, Space.World);
                }
            }
            // Right Check
            else if (viewportMousePosition.x > 1 - cameraBorderThickness)
            {
                // To do : Camera Move To Right
                cameraPivot.Translate(cameraRight * cameraMoveSpeed * Time.deltaTime, Space.World);
                // Top
                if (viewportMousePosition.y > 1 - cameraBorderThickness)
                {
                    // Top Left
                    cameraPivot.Translate(cameraForward * cameraMoveSpeed * Time.deltaTime, Space.World);
                }
                // Bottom
                else if (viewportMousePosition.y < cameraBorderThickness)
                {
                    // Bottom Left
                    cameraPivot.Translate(cameraForward * cameraMoveSpeed * (-1) * Time.deltaTime, Space.World);
                }
            }
            // Top Check
            else if (viewportMousePosition.y > 1 - cameraBorderThickness)
            {
                // To do : Camera Move To Top
                cameraPivot.Translate(cameraForward * cameraMoveSpeed * Time.deltaTime, Space.World);
                // Left Check
                if (viewportMousePosition.x < cameraBorderThickness)
                {
                    cameraPivot.Translate(cameraRight * cameraMoveSpeed * (-1) * Time.deltaTime, Space.World);
                }
                // Right Check
                else if (viewportMousePosition.x > 1 - cameraBorderThickness)
                {
                    cameraPivot.Translate(cameraRight * cameraMoveSpeed * Time.deltaTime, Space.World);
                }
            }
            // Bottom Check
            else if (viewportMousePosition.y < cameraBorderThickness)
            {
                // To do : Camera Move To Bottom
                cameraPivot.Translate(cameraForward * cameraMoveSpeed * (-1) * Time.deltaTime, Space.World);
                // Left Check
                if (viewportMousePosition.x < cameraBorderThickness)
                {
                    cameraPivot.Translate(cameraRight * cameraMoveSpeed * (-1) * Time.deltaTime, Space.World);
                }
                // Right Check
                else if (viewportMousePosition.x > 1 - cameraBorderThickness)
                {
                    cameraPivot.Translate(cameraRight * cameraMoveSpeed * Time.deltaTime, Space.World);
                }
            }
        }
    }
}