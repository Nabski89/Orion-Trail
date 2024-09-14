using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogText : MonoBehaviour
{
    public GameObject SoundEffect;
    public TextMeshProUGUI textMesh;
    public string TEXTBOX = "Your text here";
    public int displayedCharacters = 0;
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
        if (displayedCharacters <= TEXTBOX.Length)
        {
            textMesh.text = TEXTBOX.Substring(0, displayedCharacters);
            //speed up how much is displayed depending on how much we have to go
            if (displayedCharacters + 300 <= TEXTBOX.Length)
                displayedCharacters += 1;
            if (displayedCharacters + 200 <= TEXTBOX.Length)
                displayedCharacters += 1;
            if (displayedCharacters + 100 <= TEXTBOX.Length)
                displayedCharacters += 1;
            displayedCharacters += 1;
            Instantiate(SoundEffect);

        }
    }
    //this is a certified ChatGPT moment because I would have done this in update instead of as a co-routine
    public void NewText()
    {

        displayedCharacters = 0;
    }
}
