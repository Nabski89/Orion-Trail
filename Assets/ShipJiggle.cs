using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipJiggle : MonoBehaviour
{
    public TemptMoveAround TrueShip;
    // Update is called once per frame
    void Update()
    {
        if (TrueShip.TargetPosition.x != transform.parent.position.x)
        {
            transform.localPosition = new Vector3(Random.Range(-.01f, .01f),0, 0f);
        }
        if (TrueShip.TargetPosition.y != transform.parent.position.y)
        {
            transform.localPosition = new Vector3(0, Random.Range(-.01f, .01f), 0f);
        }
    }
}
