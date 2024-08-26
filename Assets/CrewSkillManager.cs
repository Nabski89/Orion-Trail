using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewSkillManager : MonoBehaviour
{
    public CharacterSkillController[] SkillHolder;
    public GameObject[] Skill;
    // Start is called before the first frame update
    void Start()
    {
        // Find all CharacterSkillController components in children and store them in SkillHolder
        SkillHolder = GetComponentsInChildren<CharacterSkillController>();
    }

    // Update is called once per frame
    public void SkillUsed(int SkillNum)
    {
        Debug.Log("Used Skill " + SkillNum);
        for (int i = 0; i < SkillHolder.Length; i++)
        {
            SkillHolder[i].CullSkills();
        }
        //I numbered my things badly but don't want to fix it
        Skill[SkillNum - 1].GetComponent<ISkillMinigame>().ActivateSkill();
        Debug.Log("Used Skill " + SkillNum);
    }
    public void SkillCompleted()
    {
        for (int i = 0; i < SkillHolder.Length; i++)
        {
            SkillHolder[i].CallForSkills();
        }
    }
}
