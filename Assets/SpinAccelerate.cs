using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAccelerate : MonoBehaviour
{
    public float rotationSpeed = 30f; // Adjust the rotation speed as needed
    public float SpeedIncrease = 30;
    void Update()
    {
        rotationSpeed += Time.deltaTime * SpeedIncrease;
        // Rotate the object around the Y-axis
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
