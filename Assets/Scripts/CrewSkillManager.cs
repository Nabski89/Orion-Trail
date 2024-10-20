using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewSkillManager : MonoBehaviour
{
    public CharacterSkillController[] SkillHolder;
    public GameObject[] Skill;
    public void SkillUsed(int SkillNum)
    {
        SkillSafetyCheck();
        Debug.Log("Used Skill " + SkillNum);
        Skill[SkillNum].GetComponent<ISkillMinigame>().ActivateSkill();
        DisableSkillButtons();
    }
    public void SkillCompleted()
    {
        //refresh the icons to start a new minigame
        EnableSkillButtons();
        //disable all our skills minigames
        for (int i = 0; i < Skill.Length; i++)
        {
            if (Skill[i].GetComponent<ISkillMinigame>().MinigameHolder != null)
                Skill[i].GetComponent<ISkillMinigame>().MinigameHolder.SetActive(false);
        }
    }
    public void DisableSkillButtons()
    {
        SkillSafetyCheck();
        for (int i = 0; i < SkillHolder.Length; i++)
        {
            SkillHolder[i].CullSkills();
        }
    }
    public void EnableSkillButtons()
    {
        SkillSafetyCheck();
        for (int i = 0; i < SkillHolder.Length; i++)
        {
            SkillHolder[i].CallForSkills();
        }
    }
    void SkillSafetyCheck()
    {
        if (SkillHolder[0] == null)
        {
            Debug.LogWarning("We have ended up in the skill saftey check script. Something has gone wrong.");
            SkillHolder = GetComponentsInChildren<CharacterSkillController>();
        }
    }
}
