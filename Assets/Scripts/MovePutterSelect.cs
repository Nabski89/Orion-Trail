using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePutterSelect : MonoBehaviour
{
    Golf GolfParent;
    public GameObject[] PutterSelections;
    public float[] RotationValues; // Array of rotation values
    private int currentIndex = 0;
    public GameObject ClickSoundEffect;
    void Start()
    {
        GolfParent = GetComponentInParent<Golf>();
    }
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
        //the rotation value in the second one is going to be not referenced to the overall rotation
        StartCoroutine(Rotate()); // Adjust the duration as needed
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
        GolfParent.Selected();
    }
    public Transform CanvasRotation;
    private IEnumerator Rotate()
    {
        float timeElapsed = 0;
        //if we do local angle it always sets it at zero. Euler works but only when it isn't rotated. I hoped the below would work but SHRUG
        float startAngle = transform.localEulerAngles.z;
        float endAngle = transform.parent.parent.localRotation.z - RotationValues[currentIndex];
        float duration = Mathf.Abs((startAngle - endAngle) / 360);
        while (timeElapsed < duration)
        {
            float angle = Mathf.Lerp(startAngle, endAngle, timeElapsed / duration);
            transform.localRotation = Quaternion.Euler(0, 0, angle);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = Quaternion.Euler(0, 0, endAngle);
    }
}