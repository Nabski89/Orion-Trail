using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DockMinigame : MonoBehaviour, ISkillMinigame
{
    public CrewSkillManager crewSkillManager { get; set; }
    public float SkillModifier { get; }
    
    //this is supposed let me set my interfaced theing through the inspector
        [SerializeField]
        public GameObject HoldMinigame;

        public GameObject MinigameHolder
        {
            get { return HoldMinigame; }
            set { HoldMinigame = value; }
        }
        
    void Start()
    {
        crewSkillManager = GetComponentInParent<GenericManager>().ShipReference.GetComponent<CrewSkillManager>();
    }
    public void ActivateSkill()
    {
        MinigameHolder.SetActive(true);
        GetComponentInChildren<DockingShip>().Reset();
        GetComponentInChildren<DockingGround>().Reset();
    }

    public void EndSkill()
    {
        MinigameHolder.SetActive(false);
        crewSkillManager.SkillCompleted();
    }

}
