using System.Collections;
using System.Collections.Generic;
using System.Data;
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
        if (GetComponentInChildren<GOLFDirection>() != null)
        {
            GOLFDirection Command = GetComponentInChildren<GOLFDirection>();
            //The initial speed is based off our fuel usage and is nice small whole numbers so if we don't chop it we ZOOM
            float Power = initialSpeed;
            float TimeElapsed = 0;
            float SpinAngleUsed = Command.TurnDirection;
            TravelTime = Command.NormalLength * Command.FuelAmount;
            while (TravelTime > TimeElapsed)
            {
                TimeElapsed += Time.deltaTime;
                transform.position = transform.position + Power * transform.right * Time.deltaTime;
                transform.Rotate(0, 0, SpinAngleUsed * Time.deltaTime, Space.World);
                yield return null;
            }
            //remove our golf direction, could probably be done more cleanly
            if (transform.childCount > 0)
            {
                Transform firstChild = transform.GetChild(0);
                firstChild.SetParent(null); // Detach the child from the parent
                Destroy(firstChild.gameObject); // Destroy the child GameObject
            }
            StartCoroutine(MoveToTargetWithDecay(0));
        }
        else
        {
            TheShip.WarpShip(transform.position);
            Destroy(gameObject);
        }
    }
}