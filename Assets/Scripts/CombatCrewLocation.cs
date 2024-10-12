using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatCrewLocation : MonoBehaviour
{
    public bool Filled;
    public int Rank;
    public Sprite Blank;
    public CharacterManager CrewInLocation;
    public Image CrewPicture;
    void Start()
    {
        CrewPicture = GetComponent<Image>();
    }
    public void FillIn(CharacterManager crewInLocation)
    {
        CrewInLocation = crewInLocation;
        CrewPicture.sprite = crewInLocation.CharacterPicture;
        Filled = true;
    }
    public void FillInDelayed(CharacterManager crewInLocation)
    {
        FillIn(crewInLocation);
        CrewPicture.sprite = Blank;
        Invoke("FillInDelayCall", 0.5f);
    }
    public void FillInDelayCall()
    {
        CrewPicture.sprite = CrewInLocation.CharacterPicture;
    }
    public void MoveOut()
    {
        Filled = false;
        CrewPicture.sprite = Blank;
        CrewInLocation = null;
    }
}