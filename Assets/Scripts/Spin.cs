using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float rotationSpeed = 30f; // Adjust the rotation speed as needed
    public bool RandomStart;
    void Start()
    {
        transform.Rotate(Vector3.forward, Random.Range(0, 360));
    }
    void Update()
    {
        // Rotate the object around the Y-axis
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}