using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nebula : MonoBehaviour
{
    public GameObject NebulaBlocker;

    void OnTriggerEnter2D(Collider2D other)
    {
        ShipController TheShip = other.GetComponent<ShipController>();
        if (TheShip != null)
        {
            NebulaBlocker.SetActive(false);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        ShipController TheShip = other.GetComponent<ShipController>();
        if (TheShip != null)
        {
            NebulaBlocker.SetActive(true);
        }
    }
}
