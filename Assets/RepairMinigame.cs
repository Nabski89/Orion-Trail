using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairMinigame : MonoBehaviour
{
    public float SkillModifier { get; }
    public GameObject MinigameHolder;
    public CrewSkillManager crewSkillManager;
    void Start()
    {
        crewSkillManager = GetComponentInParent<GenericManager>().ShipReference.GetComponent<CrewSkillManager>();
    }

    public void ActivateSkill()
    {
        MinigameHolder.SetActive(true);
        GetComponentInChildren<PipeRepair>().RandomizeAllPipe();
    }
    public void EndSkill()
    {
        MinigameHolder.SetActive(false);
        crewSkillManager.SkillCompleted();
    }
}
