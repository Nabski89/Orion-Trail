using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePutterSelect : MonoBehaviour
{
    public GameObject[] PutterSelections;
    public float[] RotationValues; // Array of rotation values
    private int currentIndex = 0;
    public int PutterNumber = 0;
    public GameObject ClickSoundEffect;

    public void RotateToNextValue(bool moveUp)
    {
        Instantiate(ClickSoundEffect);
        if (RotationValues.Length == 0) return;

        //if we are on the end of the switch, then end the thing
        if (currentIndex == 0 && moveUp == false)
            return;
        if (moveUp)
        {
            currentIndex = (currentIndex + 1) % RotationValues.Length;
        }
        else
        {
            currentIndex = (currentIndex - 1 + RotationValues.Length) % RotationValues.Length;
        }
        StartCoroutine(Rotate(transform.eulerAngles.z, RotationValues[currentIndex], Mathf.Abs(transform.eulerAngles.z - RotationValues[currentIndex]) / 360)); // Adjust the duration as needed
        DoPutterStuff();
    }
    public void DoPutterStuff()
    {
        //deactivate everything
        for (int i = 0; i < PutterSelections.Length; i++)
        {
            if (PutterSelections[i] != null)
            {
                PutterSelections[i].SetActive(false);
            }

        }
        //reenable our good light
        if (PutterSelections[currentIndex] != null)
        {
            PutterSelections[currentIndex].SetActive(true);
        }
    }
    private IEnumerator Rotate(float startAngle, float endAngle, float duration)
    {
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            float angle = Mathf.Lerp(startAngle, endAngle, timeElapsed / duration);
            transform.rotation = Quaternion.Euler(0, 0, angle);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = Quaternion.Euler(0, 0, endAngle);
    }


}