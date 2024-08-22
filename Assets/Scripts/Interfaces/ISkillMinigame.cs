using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillMinigame
{
     public float SkillModifier { get; }
    public void ActivateSkill();
}