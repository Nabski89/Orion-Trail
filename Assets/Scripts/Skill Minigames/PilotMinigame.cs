using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotMinigame : MonoBehaviour, ISkillMinigame
{
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
        crewSkillManager = GetComponentInParent<GenericManager>().ShipReference.GetComponent<CrewSkillManager>();
    }
    public void ActivateSkill()
    {
        MinigameHolder.SetActive(true);
        MinigameHolder.GetComponent<Golf>().StartGolf();
    }
    public void EndSkill()
    {
        crewSkillManager.SkillCompleted();
    }
}
