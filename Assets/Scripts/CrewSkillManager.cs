using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewSkillManager : MonoBehaviour
{
    public CharacterSkillController[] SkillHolder;
    public GameObject[] Skill;
    // Start is called before the first frame update
    void Awake()
    {
        // Find all CharacterSkillController components in children and store them in SkillHolder
        SkillHolder = GetComponentsInChildren<CharacterSkillController>();
    }

    // Update is called once per frame
    public void SkillUsed(int SkillNum)
    {
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
