using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntertainMinigame : MonoBehaviour, ISkillMinigame
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

    public void ActivateSkill()
    {
        MinigameHolder.SetActive(true);
    }
    public void EndSkill()
    {
        crewSkillManager.GetComponent<CrewSkillManager>().SkillCompleted();
    }
}
