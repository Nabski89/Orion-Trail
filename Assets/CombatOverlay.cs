using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatOverlay : MonoBehaviour
{
    public RectTransform rectTransform;
    public float speed = 50f; // Adjust the speed as needed
    public float targetWidth = 436f;
    public bool CombatModeEnabled;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("RectTransform not assigned. Please assign the RectTransform in the Inspector.");
        }
    }

    private void Update()
    {
        // Calculate the new width by gradually increasing it
        float newWidth = Mathf.MoveTowards(rectTransform.sizeDelta.x, targetWidth, speed * Time.deltaTime);
        rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.sizeDelta.y);

        // Set the position to be -half of the new width
        rectTransform.anchoredPosition = new Vector2(-newWidth * 0.5f, rectTransform.anchoredPosition.y);

        // Stop scaling once the target width is reached
         if (Mathf.Approximately(newWidth, targetWidth))
        {
            if (CombatModeEnabled == false)
                targetWidth = 436f;
            else
                targetWidth = 0;
            CombatModeEnabled = !CombatModeEnabled;
            enabled = false; // Disable the script
        }
    }
}