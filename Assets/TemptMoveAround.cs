using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TemptMoveAround : MonoBehaviour
{
    public Vector2 TargetPosition;
    public float moveSpeed = 1.0f; // Adjust the speed as needed

    void Start()
    {
        TargetPosition = new Vector2(0.0f, 0.0f);

    }
    void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            // Get the mouse position in screen space
            Vector3 mousePosition = Input.mousePosition;

            // Convert the mouse position to a world position
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));
            TargetPosition = targetPosition;
            // Print the target position to the console
            Debug.Log("Target Position: " + targetPosition);
        }

        if (TargetPosition != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, TargetPosition, moveSpeed * Time.deltaTime);
        }
    }
}