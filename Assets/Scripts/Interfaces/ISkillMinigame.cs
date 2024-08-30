using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillMinigame
{
    public float SkillModifier { get; }
    public GameObject MinigameHolder { get; set; }
    public void ActivateSkill();
    public void EndSkill();
    public CrewSkillManager crewSkillManager { get; set; }
}