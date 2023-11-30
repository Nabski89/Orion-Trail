using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float BlackHolePower;
    void OnTriggerStay2D(Collider2D other)
    {
        Transform SuckedIn = other.transform;
        // Check if the colliding object has the specified target transform
        // Calculate the move direction
        Vector3 moveDirection = SuckedIn.position - transform.position;

        // Normalize the direction to get a unit vector
        moveDirection.Normalize();

        // Move the other transform towards the trigger transform
        float DistanceFromHole = Vector2.Distance(SuckedIn.position, transform.position);
        SuckedIn.transform.position -= moveDirection * Time.deltaTime * ((BlackHolePower / DistanceFromHole) + BlackHolePower);

        if (DistanceFromHole < 0.5f)
        {
            Destroy(other.gameObject);
        }
    }
}
