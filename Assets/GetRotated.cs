using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GetRotated : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public GameObject Pointer;
    public int RotateDirection;
    public float rotationSpeed = 5;
    private bool isPressed = false;
    public GameObject SoundEffect;
    // This method is called when the button is pressed down
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        Instantiate(SoundEffect);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        Instantiate(SoundEffect);
    }
    void Update()
    {
        if (isPressed)
        {
            float rotationAmount = RotateDirection * rotationSpeed * Time.deltaTime;
            float newZRotation = Pointer.transform.localRotation.eulerAngles.z + rotationAmount;
            newZRotation = Mathf.Clamp(newZRotation, 30f, 150f);
            Pointer.transform.localRotation = Quaternion.Euler(0, 0, newZRotation);
        }
    }
}