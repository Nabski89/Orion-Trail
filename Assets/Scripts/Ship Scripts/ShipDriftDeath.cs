using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDriftDeath : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position -= (2 * Vector3.up + 4 * Vector3.left) * Time.deltaTime;
        transform.Rotate(Vector3.forward, 3 * Time.deltaTime);
    }
}
