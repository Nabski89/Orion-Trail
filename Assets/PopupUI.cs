using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PopupUI : MonoBehaviour
{
    public TextMeshProUGUI MouseoverTextUI;
    public GameObject[] TextboxHeightAdjustment;
    public void OnMouseExit()
    {
        // Hide the info box
        gameObject.SetActive(false);
    }
    public void ChangeText(string ChangedText)
    {
        MouseoverTextUI.text = ChangedText;

        // Calculate the threshold for each section, max text length is 140 and we have 6 elements (140 / 6 = 23)
        // int sectionLength = 140 / 6;

        // Get the current length of ChangedText
        int textLength = ChangedText.Length;

        // Determine how many sections should be enabled based on text length
        int activeSections = Mathf.Clamp(textLength / 23, 0, 6);
        Debug.Log("Moused over something and the length is "+textLength+" We will need this many sections " + activeSections);
        // Disable all sections first
        for (int i = 0; i < TextboxHeightAdjustment.Length; i++)
        {
            TextboxHeightAdjustment[i].SetActive(false);
        }

        // Enable the appropriate number of sections
        for (int i = 0; i < activeSections; i++)
        {
            TextboxHeightAdjustment[i].SetActive(true);
        }


    }
}
