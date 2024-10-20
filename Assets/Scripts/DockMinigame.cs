using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DockMinigame : MonoBehaviour, ISkillMinigame
{
    public CrewSkillManager crewSkillManager { get; set; }
    public float SkillModifier { get; }

    //this is supposed let me set my interfaced theing through the inspector
    [SerializeField]
    public GameObject HoldMinigame;

    public GameObject MinigameHolder
    {
        get { return HoldMinigame; }
        set { HoldMinigame = value; }
    }
    DialogText DialogBox;
    void Start()
    {
        DialogBox = GetComponentInParent<GenericManager>().MainTextReference;
        crewSkillManager = GetComponentInParent<GenericManager>().ShipReference.GetComponent<CrewSkillManager>();
    }
    public void ActivateSkill()
    {
        DialogBox.TEXTBOX = "Yeeeehaw. We're coming in for a landing. Lets get the ship to dock with the station this time so we don't have to walk so far.";
        DialogBox.NewText();
        MinigameHolder.SetActive(true);
        GetComponentInChildren<DockingShip>().Reset();
        GetComponentInChildren<DockingGround>().Reset();
    }

    public void EndSkill()
    {
        MinigameHolder.SetActive(false);
        crewSkillManager.SkillCompleted();
    }

}
