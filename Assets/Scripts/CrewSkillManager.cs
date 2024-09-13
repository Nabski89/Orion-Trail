using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewSkillManager : MonoBehaviour
{
    public CharacterSkillController[] SkillHolder;
    public GameObject[] Skill;
    public void SkillUsed(int SkillNum)
    {
        if (SkillHolder[0] == null)
            SkillHolder = GetComponentsInChildren<CharacterSkillController>();
        Debug.Log("Used Skill " + SkillNum);
        Skill[SkillNum].GetComponent<ISkillMinigame>().ActivateSkill();
        for (int i = 0; i < SkillHolder.Length; i++)
        {
            SkillHolder[i].CullSkills();
        }
        //I numbered my things badly but don't want to fix it

    }
    public void SkillCompleted()
    {
        //refresh the icons to start a new minigame
        for (int i = 0; i < SkillHolder.Length; i++)
        {
            SkillHolder[i].CallForSkills();
        }
        //disable all our skills minigames
        for (int i = 0; i < Skill.Length; i++)
        {
            if (Skill[i].GetComponent<ISkillMinigame>().MinigameHolder != null)
                Skill[i].GetComponent<ISkillMinigame>().MinigameHolder.SetActive(false);
        }

    }
}
