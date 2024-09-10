using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialCrewButtons : MonoBehaviour
{
    public Image BigPicture;
    public bool[] CrewClearCheck;
    public GameObject[] CrewArray;
    public CrewSelect CrewSelect;
    public CustomCrew CrewCarryOver;
    // Start is called before the first frame update
    public void SelectCrew(int Selected)
    {
        if (CrewClearCheck[Selected] == false)
        {
            CharacterManager CharMan = CrewArray[Selected].GetComponent<CharacterManager>();

            //fill the first unfilled character slot
            int CrewSlotNum = 0;
            //cycle through avaliable crew slots
            while (CrewSelect.CrewSlot[CrewSlotNum].GetComponent<ClearCrewmember>().CrewFilled == true && CrewSlotNum < 4)
            {
                CrewSlotNum += 1;
            }
            //Clear out the last slot in case we want to override it
            ClearCrewmember ButtonTracker = CrewSelect.CrewSlot[CrewSlotNum].GetComponent<ClearCrewmember>();
            if (CrewSlotNum == 4 && ButtonTracker.CrewFilled == true)
                CrewSelect.CrewSlot[CrewSlotNum].GetComponent<ClearCrewmember>().Press();
            //set the crew picture and return it to visible
            CrewSelect.CharacterImage[CrewSlotNum].sprite = CharMan.CharacterPicture;
            Color imageColor = CrewSelect.CharacterImage[CrewSlotNum].color;
            imageColor.a = 1f;
            CrewSelect.CharacterImage[CrewSlotNum].color = imageColor;

            //assign our stuff to the button
            ButtonTracker.CrewFilled = true;
            ButtonTracker.CrewClearCheckNumber = Selected;

            //set the big picture info
            BigPicture.sprite = CharMan.CharacterPicture;

            //set the carry over crew
            CrewCarryOver.Crew[CrewSlotNum] = CrewArray[Selected];
            CrewClearCheck[Selected] = true;
        }
    }
}
