using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnMinigame : MonoBehaviour, ISkillMinigame
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
    DialogText DialogBox;
    void Start()
    {
        DialogBox = GetComponentInParent<GenericManager>().MainTextReference;
        crewSkillManager = GetComponentInParent<GenericManager>().ShipReference.GetComponent<CrewSkillManager>();
    }
    public void ActivateSkill()
    {
        DialogBox.TEXTBOX = "Long space trips are a great time for self improvement. Maybe learning a new skill. Tragically your entire crew would rather lick a doorknob than put forth the effort to learn something they didn't already know. Maybe one day colonists won't be so stubborn";
        DialogBox.NewText();
        MinigameHolder.SetActive(true);
    }

    public void EndSkill()
    {
        MinigameHolder.SetActive(false);
        crewSkillManager.SkillCompleted();
    }
}
