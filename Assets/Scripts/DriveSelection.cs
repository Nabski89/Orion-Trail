using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveSelection : MonoBehaviour
{
    //space wind resistance
    //resist the first obstacle you hit
    //how long it travels powered (modified by wind and spin)
    //how much it travels unpowered (modified super hard by obstacles)
    //ball size, dodge obstacles with that?
    public float SpinValue;
    public float FuelFree;
    public float FuelRequired;
    public Golf Golf;

    public void StartGolfButton()
    {
        Golf GolfReference = GetComponentInParent<GenericManager>().GolfReference;
        GolfReference.StartGolf(SpinValue, FuelFree, FuelRequired);
    }
}
