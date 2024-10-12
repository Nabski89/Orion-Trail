using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UISwapText : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // Assign the TextMeshPro component in the Inspector
    public string string1 = "First Text"; // Set the first string in the Inspector
    public string string2 = "Second Text"; // Set the second string in the Inspector

    private bool TextType = true;

    public void SwapText()
    {
        if (TextType == true)
            textMeshPro.text = string1;
        else
            textMeshPro.text = string2;
        TextType = !TextType;
    }
}