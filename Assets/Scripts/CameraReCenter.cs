using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraReCenter : MonoBehaviour
{
    public GameObject Ship;
    public Vector3 offset;  // Offset relative to the ship
    private Vector3 targetPosition; // The target position with offset

    void Update()
    {
        targetPosition = Ship.transform.position + offset;
      //  transform.rotation = Ship.transform.rotation;
        // Smoothly move to the target position over 0.5 seconds
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / 0.5f);
    }
}
