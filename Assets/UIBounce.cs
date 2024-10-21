using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBounce : MonoBehaviour
{
    RectTransform canvasElement; // Assign the Canvas element in the Inspector
    public Vector3 startPosition; // Starting position
    public Vector3 endPosition; // Ending position
    public float gravity = 9.8f; // Gravity factor
    public float speed = 2.0f; // Speed of movement

    private bool movingToEnd = true;
    private float velocity = 0.0f;

    void Start()
    {
        canvasElement = GetComponent<RectTransform>();

        canvasElement.anchoredPosition = startPosition;
    }

    void Update()
    {
        float displacement = endPosition.y - canvasElement.anchoredPosition.y;
        float acceleration = gravity * (movingToEnd ? 1 : -1);

        velocity += acceleration * Time.deltaTime;
        velocity = Mathf.Clamp(velocity, -speed, speed); // Limit velocity to prevent overshooting

        canvasElement.anchoredPosition += new Vector2(0, velocity * Time.deltaTime);

        if (movingToEnd && canvasElement.anchoredPosition.y >= endPosition.y)
        {
            canvasElement.anchoredPosition = endPosition;
            movingToEnd = false;
            velocity += acceleration * Time.deltaTime;
            velocity += acceleration * Time.deltaTime;
        }
        else if (!movingToEnd && canvasElement.anchoredPosition.y <= startPosition.y)
        {
            canvasElement.anchoredPosition = startPosition;
            movingToEnd = true;
            velocity += acceleration * acceleration * Time.deltaTime;

        }
    }
}