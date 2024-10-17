using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelBall : MonoBehaviour
{
    public ShipController TheShip;
    // Speed of the golf ball
    public float initialSpeed = 5.0f;
    public float decelerationRate = 0.1f;
    // Minimum distance to consider the target reached
    public float minDistance = 0.01f;
    // Time delay before starting the motion
    //10 is low to notice, 45 is high, 90 is insane
    public float SpinAngle = 15;
    public float startDelay = 0.5f;
    public float TravelTime = 4f;

    public void ActivateBeacon()
    {
        StartCoroutine(MoveToTargetWithDecay(startDelay));
    }
    IEnumerator MoveToTargetWithDecay(float delay)
    {
        // Delay before starting the motion
        yield return new WaitForSeconds(delay);
        //The initial speed is based off our fuel usage and is nice small whole numbers so if we don't chop it we ZOOM
        float Power = initialSpeed / 4;
        float TimeElapsed = 0;
        float SpinAngleUsed = 0;
        while (TravelTime > TimeElapsed)
        {
            TimeElapsed += Time.deltaTime;
            transform.position = transform.position + Power * transform.right * Time.deltaTime * (2 - (TimeElapsed / TravelTime));
            transform.Rotate(0, 0, SpinAngleUsed * Time.deltaTime, Space.World);
            yield return null;
        }
        TheShip.WarpShip(transform.position);
        Destroy(gameObject);
    }
}