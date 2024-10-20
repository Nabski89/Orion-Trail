using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairMinigame : MonoBehaviour, ISkillMinigame
{
    DialogText DialogBox;
    public float SkillModifier { get; }
    //this is supposed let me set my interfaced theing through the inspector
    [SerializeField]
    public GameObject HoldMinigame;

    public GameObject MinigameHolder
    {
        get { return HoldMinigame; }
        set { HoldMinigame = value; }
    }
    public CrewSkillManager crewSkillManager { get; set; }
    void Start()
    {
        DialogBox = GetComponentInParent<GenericManager>().MainTextReference;
        crewSkillManager = GetComponentInParent<GenericManager>().ShipReference.GetComponent<CrewSkillManager>();
    }
    public void ActivateSkill()
    {
        DialogBox.TEXTBOX = "Oh SHIP. The ship is damaged? It came that way because we were on a budget you say. Well either way we should try to fix some of these pipes and surely that will make things run better. I hope.";
        DialogBox.NewText();
        MinigameHolder.SetActive(true);
        GetComponentInChildren<PipeRepair>().RandomizeAllPipe();
    }
    public void EndSkill()
    {
        crewSkillManager.SkillCompleted();
    }
}
