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
    Image CrewPicture;
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
    public void MoveOut()
    {
        Filled = false;
        CrewPicture.sprite = Blank;
        CrewInLocation = null;
    }
}