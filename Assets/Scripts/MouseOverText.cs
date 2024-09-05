using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MouseOverText : MonoBehaviour
{
    public PopupUI infoBox;   // Reference to the info box UI element
    public string infoText140max;      // The text to display in the info box
                                       //   private Text infoBoxText;    // The Text component inside the info box

    void Start()
    {
        infoBox = GetComponentInParent<GenericManager>().MouseOver;
        // Find the Text component inside the info box
        //     infoBoxText = infoBox.GetComponentInChildren<Text>();
    }

    // This method is called when the mouse enters the UI element
    public void OnMouseEnter()
    {
        // Set the info box text
        //   if (infoBoxText != null)
        {
            //     infoBoxText.text = infoText;
        }

        // Position the info box near the mouse pointer
        Debug.Log("Moused Over");
        infoBox.transform.position = transform.position;


        Invoke("ShowBox", 0.1f);
        // Show the info box



    }
    public void ShowBox()
    {
        infoBox.gameObject.SetActive(true);
        infoBox.ChangeText(infoText140max);
    }
    // This method is called when the mouse exits the UI element
    public void OnMouseExit()
    {
        // Hide the info box
        infoBox.gameObject.SetActive(false);
    }
}