using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RotateView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Transform Ship;
    public RectTransform Compass;
    public float rotationSpeed = 50f;
    public bool isButtonDown = false;
    // Start is called before the first frame update
    void Update()
    {
        // Rotate the ship and compass while the button is held down
        if (isButtonDown == true)
        {
            // Rotate the ship clockwise along the z-axis
            Ship.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

            // Rotate the compass counterclockwise along the z-axis
            Compass.Rotate(Vector3.forward * -rotationSpeed * Time.deltaTime);
        }
    }
    // Called when the button is pressed
    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonDown = true;
    }

    // Called when the button is released
    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonDown = false;
    }
}
