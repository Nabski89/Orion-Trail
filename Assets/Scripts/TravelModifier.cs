using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelModifier : MonoBehaviour
{
    public GameObject SoundEffect;
    // This function is called when another collider enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        GameObject HazardSoundEffect = Instantiate(SoundEffect);
        Destroy(HazardSoundEffect, 10f);
        // Check if the other GameObject has the TravelBall component
        TravelBall travelBall = other.GetComponent<TravelBall>();
        if (travelBall != null)
        {
            // Increase SpinAngleUsed by a factor of 2
            travelBall.SpinAngle *= 20;
        }
    }
}
