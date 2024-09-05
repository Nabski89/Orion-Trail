using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipNStarLink : MonoBehaviour
{
    public Transform Ship;
    public float ParallaxMod = 0.95f;

    void Update()
    {
        float x = Ship.position.x * ParallaxMod;
        float y = Ship.position.y * ParallaxMod;
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
