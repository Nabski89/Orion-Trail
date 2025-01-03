using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CombatCrewLocation : MonoBehaviour, IPointerClickHandler
{
    public bool Filled;
    public int Rank;
    public Sprite Blank;
    public CharacterManager CrewInLocation;
    public Image CrewPicture;
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


    public void OnPointerClick(PointerEventData eventData)
    {
        // Check if the click is within the bounds of the visible sprite
        if (RectTransformUtility.RectangleContainsScreenPoint(CrewPicture.rectTransform, eventData.position, eventData.pressEventCamera))
        {
            // Perform the button's action here
            Debug.Log("Button clicked!");
        }
    }
}