using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntertainMinigame : MonoBehaviour, ISkillMinigame
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
        MinigameHolder.SetActive(true);
        GetComponentInChildren<ThrowAParty>().ThrowParty();
    }
    public void EndSkill()
    {
        MinigameHolder.SetActive(false);
        crewSkillManager.SkillCompleted();
    }
}
