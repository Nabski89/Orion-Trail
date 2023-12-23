using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrewSelect : MonoBehaviour
{
    public GameObject[] CrewSlot;
    public Image[] CharacterImage;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ClearCrew(int Number)
    {
        // Set the alpha channel to 0 (completely transparent)
        Color imageColor = CharacterImage[Number].color;
        imageColor.a = 0f;
        CharacterImage[Number].color = imageColor;
    }
}
