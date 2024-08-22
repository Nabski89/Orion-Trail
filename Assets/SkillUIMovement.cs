using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUIMovement : MonoBehaviour
{
    public void Move(float delay)
    {
        Invoke("StartMoving", delay);
    }

    public Vector3 targetPosition;  // The position to move to
    public float moveDuration = 1.0f;  // Duration of the movement
    void StartMoving()
    {
        Debug.Log("MoveUIElement");
        // Start the coroutine to move smoothly to the target position
        StartCoroutine(MoveToPosition(targetPosition, moveDuration));
    }

    IEnumerator MoveToPosition(Vector3 target, float duration)
    {

        Vector3 startPosition = transform.position;  // The current position of the object
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            // Calculate the new position using Lerp
            transform.position = Vector3.Lerp(startPosition, target, elapsedTime / duration);

            // Increase elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the object is exactly at the target position at the end
        transform.position = target;
    }
}