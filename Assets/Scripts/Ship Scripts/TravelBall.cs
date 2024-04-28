using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelBall : MonoBehaviour
{
    public Vector3 TargetLocation;
    public ShipController ParentShip;
    // Speed of the golf ball
    public float initialSpeed = 5.0f;
    public float decelerationRate = 0.1f;
    // Minimum distance to consider the target reached
    public float minDistance = 0.01f;
    // Time delay before starting the motion
    public float startDelay = 0.0f;

    void Start()
    {
        // Start the motion coroutine with a delay
        StartCoroutine(MoveToTargetWithDecay(startDelay));
    }

    IEnumerator MoveToTargetWithDecay(float delay)
    {
        // Delay before starting the motion
        yield return new WaitForSeconds(delay);

        // Calculate initial direction and distance
        Vector3 direction = (TargetLocation - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, TargetLocation);

        // Continue moving until the target is reached
        while (distance > minDistance)
        {
            // Calculate the current speed based on the distance
            float currentSpeed = Mathf.Max(initialSpeed * (distance / initialSpeed), minDistance);

            // Move the object towards the target
            transform.Translate(direction * currentSpeed * Time.deltaTime, Space.World);

            // Update the distance to the target
            distance = Vector3.Distance(transform.position, TargetLocation);

            // Decelerate the speed
            initialSpeed -= decelerationRate * Time.deltaTime;

            yield return null;
        }

        // Ensure the object reaches exactly the target position
        transform.position = TargetLocation;
        ParentShip.WarpShip(transform.position);
        Destroy(gameObject);
    }
}