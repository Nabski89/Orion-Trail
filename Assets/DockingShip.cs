 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DockingShip : MonoBehaviour
{
    public RectTransform Ship;
    public Slider movementSlider;       // The UI Slider controlling the movement
    public Vector3 forwardOffset = new Vector3(0, 0, 100);  // Offset for forward movement
    public Vector3 downOffset = new Vector3(0, -100, 0);    // Offset for downward movement


    public Slider rotationSlider;              // The slider controlling the rotation
    private float minRotation = -40f;  // Minimum z-rotation in degrees
    private float maxRotation = 80f;   // Maximum z-rotation in degrees
    private Vector3 originalPosition;   // Store the original position of the canvas



    public Slider thrusterSlider;       // The UI Slider controlling the movement
    public float ThrustPower;
    void Start()
    {
        // Save the original position of the canvas
        originalPosition = Ship.localPosition;

        // Add a listener to the slider to call the MoveCanvas function when the slider value changes
        movementSlider.onValueChanged.AddListener(MoveCanvas);


        // Add listener to slider value changes
        rotationSlider.onValueChanged.AddListener(UpdateRotation);

        thrusterSlider.onValueChanged.AddListener(ActivateThruster);
        // Set initial rotation based on the slider's starting value
        UpdateRotation(rotationSlider.value);
    }
    public void Reset()
    {
        Ship.localPosition = new Vector2(-500, 250);
    }
    void MoveCanvas(float value)
    {
        // Calculate the new position based on the slider value (0 to 1)
        Vector3 newPosition = originalPosition + Ship.right * value * 100;

        // Apply the new position to the canvas object
        Ship.localPosition = newPosition;
    }

    void UpdateRotation(float value)
    {
        // Calculate the new z-rotation based on slider value
        float newRotation = Mathf.Lerp(minRotation, maxRotation, value);

        // Apply the new rotation to the RectTransform
        Ship.localRotation = Quaternion.Euler(0f, 0f, newRotation);
    }
    void ActivateThruster(float value)
    {
        ThrustPower = value;
    }
}
