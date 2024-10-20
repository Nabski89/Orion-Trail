using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUsed : MonoBehaviour
{

    public int SkillNum;
    public int MoraleCost;
    public void Pushed()
    {
        GetComponentInParent<CrewSkillManager>().SkillUsed(SkillNum);
        GetComponentInParent<CharacterManager>().ChangeMorale(MoraleCost);
    }
}
