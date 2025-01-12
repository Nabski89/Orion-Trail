using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveTowardsShip : MonoBehaviour
{

    public Transform target; // The target Transform, e.g., ShipController
    public float speed = 5.0f; // Movement speed
    public bool isInArea = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("We hit" + other);
        ShipController TheShip = other.GetComponent<ShipController>();
        if (TheShip != null)
        {
            target = TheShip.transform;
            isInArea = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        ShipController TheShip = other.GetComponent<ShipController>();
        if (TheShip != null)
        {
            target = null;
            isInArea = false;
        }
    }

    void Update()
    {
        if (isInArea && target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.parent.transform.position += direction * speed * Time.deltaTime;
        }
    }
}
