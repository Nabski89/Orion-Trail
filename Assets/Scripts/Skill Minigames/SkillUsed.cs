using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUsed : MonoBehaviour
{
    public int SkillNum;
    public void Pushed()
    {
        
        GetComponentInParent<CrewSkillManager>().SkillUsed(SkillNum);

    }
}
