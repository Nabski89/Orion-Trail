using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairMinigame : MonoBehaviour, ISkillMinigame
{
    DialogText DialogBox;
    public float SkillModifier { get; }
    //this is supposed let me set my interfaced theing through the inspector
    [SerializeField]
    public GameObject HoldMinigame;
    public Supplies Supply;
    public GameObject StartSound;
    public GameObject FailureSound;
    public CharacterShip CharacterShip;

    public GameObject MinigameHolder
    {
        get { return HoldMinigame; }
        set { HoldMinigame = value; }
    }
    public CrewSkillManager crewSkillManager { get; set; }
    void Start()
    {
        CharacterShip = GetComponentInParent<GenericManager>().ShipReference;
        DialogBox = GetComponentInParent<GenericManager>().MainTextReference;
        crewSkillManager = CharacterShip.GetComponent<CrewSkillManager>();
        Supply = GetComponentInParent<Supplies>();
    }
    public void ActivateSkill()
    {
        RepairType = 0;
        DialogBox.TEXTBOX = "Oh SHIP. The ship is damaged? It came that way because we were on a budget you say. Well either way we should try to fix some of these pipes and surely that will make things run better. I hope.";
        DialogBox.NewText();
        MinigameHolder.SetActive(true);
        GetComponentInChildren<PipeRepair>().RandomizeAllPipe();
    }
    public void SelectRepairType(int TypeNum)
    {

        if (TypeNum >= 0 && TypeNum <= 3)
        {
            if (Supply.MechanicalPart > 0)
            {
                RepairType = TypeNum;
                Instantiate(StartSound);
            }
            else
            {
                Instantiate(FailureSound);
            }
        }
        else if (TypeNum >= 4 && TypeNum <= 5)
        {
            if (Supply.ElectricalPart > 0)
            {
                RepairType = TypeNum;
                Instantiate(StartSound);
            }
            else
            {
                Instantiate(FailureSound);
            }
        }
        else if (TypeNum >= 6 && TypeNum <= 7)
        {
            if (Supply.TechPart > 0)
            {
                RepairType = TypeNum;
                Instantiate(StartSound);
            }
            else
            {
                Instantiate(FailureSound);
            }
        }
        else
        {
            Debug.Log("Unknown command in golf as we try to select a part and it wasn't in the expected range");
        }
    }
    public int RepairType = 0;
    public void EndSkill()
    {
        //the second number is the repair amount, which right now is always 5 but I should make it so bad repairs fix less
        CharacterShip.RepairEngine(RepairType, 5);
        crewSkillManager.SkillCompleted();
    }
}
