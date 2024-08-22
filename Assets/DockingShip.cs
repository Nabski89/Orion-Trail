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

    private Vector3 originalPosition;   // Store the original position of the canvas
    void Start()
    {
        // Save the original position of the canvas
        originalPosition = Ship.localPosition;

        // Add a listener to the slider to call the MoveCanvas function when the slider value changes
        movementSlider.onValueChanged.AddListener(MoveCanvas);
    }

    void MoveCanvas(float value)
    {
        // Calculate the new position based on the slider value (0 to 1)
        Vector3 newPosition = originalPosition + Ship.right * value * 100;

        // Apply the new position to the canvas object
        Ship.localPosition = newPosition;
    }
}
