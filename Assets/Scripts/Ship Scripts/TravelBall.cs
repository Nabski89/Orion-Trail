using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelBall : MonoBehaviour
{
    public Vector3 TargetLocation;
    // Speed of the golf ball
    public float initialSpeed = 5.0f;
    public float decelerationRate = 0.1f;
    // Minimum distance to consider the target reached
    public float minDistance = 0.01f;
    // Time delay before starting the motion
    //10 is low to notice, 45 is high, 90 is insane
    public float SpinAngle = 15;
    public float startDelay = 0.5f;
    public float TravelTime = 4f;

    void Start()
    {
        Vector3 direction = TargetLocation - transform.position;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Create a rotation with the angle around the Z axis
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        // Apply the rotation to the object
        transform.rotation = rotation;

        StartCoroutine(MoveToTargetWithDecay(startDelay));

    }

    IEnumerator MoveToTargetWithDecay(float delay)
    {
        // Delay before starting the motion
        yield return new WaitForSeconds(delay);
        float Power = initialSpeed;
        float TimeElapsed = 0;
        float SpinAngleUsed = 0;
        while (TravelTime > TimeElapsed)
        {
            transform.position = transform.position + transform.right * Time.deltaTime * (2 - (TimeElapsed / TravelTime));
            Power -= 1 * Time.deltaTime;
            TimeElapsed += Time.deltaTime;
            if (TimeElapsed < TravelTime / 2)
                SpinAngleUsed += Time.deltaTime * SpinAngle / TravelTime;
            else
                SpinAngleUsed -= 1.25f * Time.deltaTime * SpinAngle / TravelTime;

            transform.Rotate(0, 0, SpinAngleUsed * Time.deltaTime, Space.World);
            yield return null;
        }
        GetComponentInParent<ShipController>().WarpShip(transform.position);
        Destroy(gameObject);
        /*
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

                // Ensure the object reaches exactly the target position, then move the ship
                transform.position = TargetLocation;
                GetComponentInParent<ShipController>().WarpShip(transform.position);
                Destroy(gameObject);
                */
    }
}