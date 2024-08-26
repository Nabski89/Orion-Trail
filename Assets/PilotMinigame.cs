using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotMinigame : MonoBehaviour
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
        GetComponent<DockingShip>().Reset();
        GetComponent<DockingGround>().Reset();
    }
}
