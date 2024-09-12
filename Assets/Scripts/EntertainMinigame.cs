using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntertainMinigame : MonoBehaviour, ISkillMinigame
{
    public float SkillModifier { get; }
    //this is supposed let me set my interfaced theing through the inspector
    [SerializeField]
    public GameObject HoldMinigame;
    public CrewSkillManager crewSkillManager { get; set; }
    void Start()
    {
        crewSkillManager = GetComponentInParent<GenericManager>().ShipReference.GetComponent<CrewSkillManager>();
    }
    public GameObject MinigameHolder
    {
        get { return HoldMinigame; }
        set { HoldMinigame = value; }
    }
    public void ActivateSkill()
    {
        MinigameHolder.SetActive(true);
        GetComponentInChildren<ThrowAParty>().ThrowParty();
    }
    public void EndSkill()
    {
        crewSkillManager.SkillCompleted();
    }
}
