using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipJiggle : MonoBehaviour
{
    public TemptMoveAround TrueShip;
    public float JiggleRate;
    // Update is called once per frame
    void Update()
    {
        JiggleRate = TrueShip.moveSpeed;
        if (TrueShip.TargetPosition != Vector2.zero)
            JiggleRate = JiggleRate / 5;
        transform.localPosition = new Vector3(Random.Range(-.01f, .01f) * JiggleRate, Random.Range(-.01f, .01f) * JiggleRate, 0f);

    }
}