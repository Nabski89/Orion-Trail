using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePutterSelect : MonoBehaviour
{
    public GameObject[] PutterSelections;
    //I wrote this by hand then decided to GPT it instead
    // Reference to the RectTransform that will be moved
    public int PutterNumber = 0;

    // Call this function when the button is clicked
    public void OnButtonClick(int CycleDirection)
    {
        PutterSelections[PutterNumber].SetActive(false);
        PutterNumber += CycleDirection;
        if (PutterNumber == PutterSelections.Length)
            PutterNumber = 0;
        if (PutterNumber < 0)
            PutterNumber = PutterSelections.Length - 1;
        PutterSelections[PutterNumber].SetActive(true);

    }
}