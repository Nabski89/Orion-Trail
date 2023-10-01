using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFuel : MonoBehaviour
{
    public int FuelMod;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Added " + FuelMod+" Fuel");
        GetComponentInParent<Supplies>().Fuel += FuelMod;
    }
}
