using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveSelection : MonoBehaviour
{
    public float RotateValue;
    public float PowerValue;
    public Golf Golf;

    public void StartGolfButton()
    {
        Golf.StartGolf(RotateValue, PowerValue);
    }
}
