using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DockMinigame : MonoBehaviour
{
    public GameObject MinigameHolder;
    public CrewSkillManager crewSkillManager;
    public float SkillModifier { get; }
    void Start()
    {
        crewSkillManager = GetComponentInParent<GenericManager>().ShipReference.GetComponent<CrewSkillManager>();
    }
    public void ActivateSkill()
    {
        MinigameHolder.SetActive(true);
        GetComponent<DockingShip>().Reset();
        GetComponent<DockingGround>().Reset();
    }

    public void EndSkill()
    {
        MinigameHolder.SetActive(false);
        crewSkillManager.SkillCompleted();
    }

}
