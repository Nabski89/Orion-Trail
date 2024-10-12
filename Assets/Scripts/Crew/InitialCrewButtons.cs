using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InitialCrewButtons : MonoBehaviour
{
    public Image BigPicture;
    public TextMeshProUGUI CharacterTextBox;
    public bool[] CrewClearCheck;
    public GameObject[] CrewArray;
    public CrewSelect CrewSelect;
    public CustomCrew CrewCarryOver;
    // Start is called before the first frame update
    public void SelectCrew(int Selected)
    {
        if (CrewClearCheck[Selected] == false)
        {
            GameObject CrewGameObject = CrewArray[Selected];
            CharacterManager CharMan = CrewGameObject.GetComponent<CharacterManager>();

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
            CharacterTextBox.text = CharMan.CharacterLore[0];

            //spawn the skills 
            ShowOffSkills(CrewGameObject);
            ShowOffTraits(CrewGameObject);
            //set the carry over crew
            CrewCarryOver.Crew[CrewSlotNum] = CrewGameObject;
            CrewClearCheck[Selected] = true;
        }
    }
    public Transform BigSkillHolder;
    public Transform BigTraitHolder;
    public GameObject BlankImage;
    void ShowOffSkills(GameObject Crew)
    {
        foreach (Transform child in BigSkillHolder.transform)
        {
            Destroy(child.gameObject);  // Destroy each child GameObject
        }

        SkillUsed[] skillUsedComponents = Crew.GetComponentsInChildren<SkillUsed>();

        foreach (SkillUsed skillUsed in skillUsedComponents)
        {
            // Instantiate the new prefab
            GameObject newObject = Instantiate(BlankImage, BigSkillHolder);

            // Get the Image component from the new prefab
            Image newImage = newObject.GetComponent<Image>();
            if (newImage != null)
            {
                // Get the Image component from the SkillUsed script
                Image BlankImage = skillUsed.GetComponent<Image>();
                if (BlankImage != null)
                {
                    // Set the new prefab's image to the skill's image
                    newImage.sprite = BlankImage.sprite;
                }
                else
                {
                    Debug.LogError("SkillUsed does not have an Image component.");
                }
            }
        }
    }

    void ShowOffTraits(GameObject Crew)
    {
        //Clean Up
        foreach (Transform child in BigTraitHolder.transform)
            Destroy(child.gameObject);
        ITrait[] TraitList = Crew.GetComponentsInChildren<ITrait>();

        foreach (ITrait traitUsed in TraitList)
        {
            // Instantiate the new prefab and get its image component
            GameObject newObject = Instantiate(BlankImage, BigTraitHolder);
            Image newImage = newObject.GetComponent<Image>();

            // Get the Image component from the New Trait script
            Image Yeah = newObject.GetComponent<Image>();
            if (Yeah != null)
            {
                   Debug.Log("Spawn a trait.");
                // Set the new prefab's image to the skill's image
                newImage.sprite = traitUsed.TraitIcon;
            }
            else
                Debug.LogError("The Trait Here does not have a sprite component.");
        }
    }
}
