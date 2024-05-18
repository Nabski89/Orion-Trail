using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveSelection : MonoBehaviour
{
    public float RotateValue;
    public float RotateMin;
    public float PowerValue;
    public float PowerMin;
    public float FuelRequired;
    public Golf Golf;

    public void StartGolfButton()
    {
        Golf.StartGolf(RotateValue, RotateMin, PowerValue, PowerMin, FuelRequired);
    }
}
