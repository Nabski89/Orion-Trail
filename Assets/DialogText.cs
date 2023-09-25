using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogText : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public string TEXTBOX = "Your text here";
    int totalCharacters = 2;
    int displayedCharacters = 0;
    private void Start()
    {
        if (textMesh == null)
        {
            Debug.LogError("TextMeshProUGUI component not found.");
            return;
        }
        NewText();
    }
    void Update()
    {
        if (displayedCharacters <= totalCharacters)
        {
            textMesh.text = TEXTBOX.Substring(0, displayedCharacters);
            displayedCharacters += 1;
        }
    }
    //this is a certified ChatGPT moment because I would have done this in update instead of as a co-routine
    public void NewText()
    {
        totalCharacters = TEXTBOX.Length;
        displayedCharacters = 0;
    }
}
