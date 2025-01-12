using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepOnTheShip : MonoBehaviour
{
    public Transform Ship;

    void Update()
    {
        if (Ship != null)
        {
            transform.position = Ship.position;
            transform.rotation = Ship.rotation;
        }
    }
}
