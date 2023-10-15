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
            displayedCharacters += 1;
            GameObject SoundBlip = Instantiate(SoundEffect);
            Destroy(SoundBlip,.05f);
        }
    }
    //this is a certified ChatGPT moment because I would have done this in update instead of as a co-routine
    public void NewText()
    {

        displayedCharacters = 0;
    }
}
