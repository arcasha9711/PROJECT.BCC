using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCC
{
    public class SelectionUI : MonoBehaviour
    {
        public RectTransform selectionBox;

        private Vector2 startClickPosition = Vector3.zero;
        private List<SelectableCharacter> selectedCharacters = new List<SelectableCharacter>();

        private void Start()
        {
            selectionBox.sizeDelta = Vector2.zero;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1)) // Right Mouse Button Click
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
                {
                    for (int i = 0; i < selectedCharacters.Count; i++)
                    {
                        selectedCharacters[i].SetDestination(hit.point);
                    }
                }
            }

            // Selection Box Start
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                startClickPosition = Input.mousePosition;
            }

            // Selection Box Update - (Dragging)
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Vector2 currentPosition = Input.mousePosition;
                float width = currentPosition.x - startClickPosition.x;
                float height = currentPosition.y - startClickPosition.y;

                selectionBox.anchoredPosition = startClickPosition + new Vector2(width, height) / 2f;
                selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
            }

            // Execute - Selection Order
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (selectionBox.sizeDelta.magnitude < 10f) // If the box is too small, it's considered a click
                {
                    ClickSelect();
                }
                else
                {
                    Select();
                }

                startClickPosition = Vector3.zero;
                selectionBox.sizeDelta = Vector2.zero;
            }
        }

        public void Select()
        {
            Vector2 min = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
            Vector2 max = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);

            selectedCharacters.Clear();
            foreach (var character in SelectableCharacter.SpawnedCharacters)
            {
                Vector3 screenPosition = Camera.main.WorldToScreenPoint(character.transform.position);

                if (screenPosition.x > min.x && screenPosition.x < max.x &&
                    screenPosition.y > min.y && screenPosition.y < max.y)
                {
                    character.Select();
                    selectedCharacters.Add(character);
                }
                else
                {
                    character.Deselect();
                }
            }
        }

        public void ClickSelect()
        {
            Vector2 clickPosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(clickPosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
            {
                SelectableCharacter character = hit.collider.GetComponent<SelectableCharacter>();

                if (character != null)
                {
                    selectedCharacters.Clear();
                    foreach (var ch in SelectableCharacter.SpawnedCharacters)
                    {
                        if (ch == character)
                        {
                            ch.Select();
                            selectedCharacters.Add(ch);
                        }
                        else
                        {
                            ch.Deselect();
                        }
                    }
                }
            }
        }
    }
}