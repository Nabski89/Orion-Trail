using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveSelectionSelection : MonoBehaviour
{

    public void StartGolfButton()
    {
        GetComponentInChildren<DriveSelection>().StartGolfButton();
    }
}
