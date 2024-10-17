using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DriveSelection : MonoBehaviour
{
    public Transform FuelIcon;
    // Start is called before the first frame update
    void Start()
    {
        Transform FuelHolder = GetComponentInChildren<HorizontalLayoutGroup>().transform;
        GOLFDirection[] customDirections = FindObjectsOfType<GOLFDirection>();
        foreach (GOLFDirection Direction in customDirections)
        {
            Instantiate(FuelIcon, FuelHolder);
        }
    }
}
