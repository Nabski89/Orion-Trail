using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialCrewButtons : MonoBehaviour
{
    public Image BigPicture;
    public CrewSelect CrewSelect;
    // Start is called before the first frame update
    public void SelectCrew(GameObject Selected)
    {
        CharacterManager CharMan = Selected.GetComponent<CharacterManager>();

        //fill the first unfilled character slot
        int CrewSlotNum = 0;
        //cycle through avaliable crew slots
        while (CrewSelect.CrewSlot[CrewSlotNum].GetComponent<ClearCrewmember>().CrewFilled == true && CrewSlotNum < 4)
        {
            CrewSlotNum += 1;
        }
        //set the crew picture and return it to visible
        CrewSelect.CharacterImage[CrewSlotNum].sprite = CharMan.CharacterPicture;
        Color imageColor = CrewSelect.CharacterImage[CrewSlotNum].color;
        imageColor.a = 1f;
        CrewSelect.CharacterImage[CrewSlotNum].color = imageColor;

        ClearCrewmember ButtonTracker = CrewSelect.CrewSlot[CrewSlotNum].GetComponent<ClearCrewmember>();
        ButtonTracker.CrewFilled = true;
        //set the big picture info
        BigPicture.sprite = CharMan.CharacterPicture;
    }

}
