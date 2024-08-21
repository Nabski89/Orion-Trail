using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUsed : MonoBehaviour
{
    public void Pushed()
    {
        GetComponentInParent<CrewSkillManager>().SkillUsed();

    }
}
