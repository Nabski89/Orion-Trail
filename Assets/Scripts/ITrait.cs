using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ITrait
{
    public int TraitNumber { get; }
    public string TraitName { get; }
    public Sprite TraitIcon { get; }
    public string TraitDescription { get; }
    public void CombatBonus();
    public void SkillBonus();
}
