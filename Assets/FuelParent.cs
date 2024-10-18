using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelParent : MonoBehaviour
{
    public GameObject ProgressLight;
    public GameObject ReadyLight;
    FuelBar[] customFuel;
    public void ReadyCheck()
    {
        customFuel = FindObjectsOfType<FuelBar>();
        foreach (FuelBar Fuel in customFuel)
        {
            if (Fuel.Opened == true)
                return;
        }
        ProgressLight.SetActive(false);
        ReadyLight.SetActive(true);
    }
}
