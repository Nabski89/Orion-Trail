using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewSkillManager : MonoBehaviour
{
    public CharacterSkillController[] SkillHolder;
    // Start is called before the first frame update
    void Start()
    {
        // Find all CharacterSkillController components in children and store them in SkillHolder
        SkillHolder = GetComponentsInChildren<CharacterSkillController>();
    }

    // Update is called once per frame
    public void SkillUsed()
    {
        for (int i = 0; i < SkillHolder.Length; i++)
        {
            SkillHolder[i].CallForSkills();
        }
    }
}
