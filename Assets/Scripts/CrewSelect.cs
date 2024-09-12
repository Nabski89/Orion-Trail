using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrewSelect : MonoBehaviour
{
    public GameObject[] CrewSlot;
    public Image[] CharacterImage;
    public CustomCrew CrewCarryOver;
    // Start is called before the first frame update
    public void ClearCrew(int Number)
    {
        // Set the alpha channel to 0 (completely transparent)
        Color imageColor = CharacterImage[Number].color;
        imageColor.a = 0f;
        CharacterImage[Number].color = imageColor;
        //clear them from the crew roster completely
        CrewCarryOver.Crew[Number] = null;
    }
}
