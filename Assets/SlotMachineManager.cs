using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineManager : MonoBehaviour
{
    public SlotMachineRoll[] slotMachineRolls;
    public GenericManager genericManager;
    public CombatController combatController;
    public CharCombUI[] CharUI;
    void Start()
    {
        genericManager = GetComponentInParent<GenericManager>();
        combatController = GetComponentInParent<CombatController>();
        // Get all SlotMachineRoll components in the children
        CrewShipReference = genericManager.GetComponentInChildren<CharacterBulkManager>();
        slotMachineRolls = GetComponentsInChildren<SlotMachineRoll>();
        CharUI = GetComponentsInChildren<CharCombUI>();
    }
    public CharacterBulkManager CrewShipReference;
    CharacterManager[] characterManagers;
    public void ActivateSlots()
    {
        characterManagers = CrewShipReference.GetComponentsInChildren<CharacterManager>();
        // Perform an action for each element in the array
        for (int i = 0; i < slotMachineRolls.Length; i++)
        {
            slotMachineRolls[i].ColorImage1.color = characterManagers[i].CharacterColor[0];
            slotMachineRolls[i].ColorImage2.color = characterManagers[i].CharacterColor[1];
            //haha I got this order wrong
            CharUI[i].BadgeEdges[1].GetComponent<Image>().color = characterManagers[i].CharacterColor[0];
            CharUI[i].BadgeEdges[0].GetComponent<Image>().color = characterManagers[i].CharacterColor[1];
            CharUI[i].BadgeFace.GetComponent<Image>().sprite = characterManagers[i].GetComponentInChildren<BadgeFace>().GetComponent<Image>().sprite;
        }
    }
    public void StartRoll()
    {
        if (Selectable == 0)
        {
            //reset our attack values
            Atk = 0;
            Block = 0;
            Special = 0;
            foreach (SlotMachineRoll roll in slotMachineRolls)
                roll.StartRouletteButton();
        }
    }
    public void StopRoll()
    {
        //hacky way to make sure it is spinning
        if (slotMachineRolls[0].rollSpeed > 0)
        {
            float delay = 0;
            foreach (SlotMachineRoll roll in slotMachineRolls)
            {
                delay += 0.25f;
                roll.StopRouletteButton(delay);
            }
            //make it so we can actually attack
            Selectable = 1;
        }
    }
    public int Atk;
    public int Block;
    public int Special;
    public int Selectable = 0;

    //this is used for the crew to attack
    public void Select(int Selected)
    {
        Atk = 0;
        Block = 0;
        Special = 0;
        int[] tempArray = new int[3];
        tempArray[0] = 0;
        tempArray[1] = 0;
        tempArray[2] = 0;
        if (Selectable > 0)
        {
            Selectable -= 1;
            foreach (SlotMachineRoll SlotCharacter in slotMachineRolls)
            {
                SlotValue Read = SlotCharacter.transform.GetChild(Selected).GetComponent<SlotValue>();
                Atk += Read.Attack;
                Block += Read.Block;
                Special += Read.Buff;
                if (Read.Rank1 == true)
                    tempArray[0] += 1;
                if (Read.Rank2 == true)
                    tempArray[1] += 1;
                if (Read.Rank3 == true)
                    tempArray[2] += 1;
            }
            //check who is the largest guy to hit
            int HitMe = 0;
            if (tempArray[0] >= tempArray[1] && tempArray[0] >= tempArray[2])
                HitMe = 0;
            else if (tempArray[1] >= tempArray[0] && tempArray[1] >= tempArray[2])
                HitMe = 1;
            else
                HitMe = 2;


            genericManager.MainTextReference.TEXTBOX += "<br>Attack for " + Atk + ". Block for " + Block + ". Special count of " + Special + " and it is going to hit in rank " + HitMe;
            if (tempArray[HitMe] == 4)
                genericManager.MainTextReference.TEXTBOX += " 1 Bonus";
            if (tempArray[HitMe] == 5)
                genericManager.MainTextReference.TEXTBOX += " 2 Bonus";

                //initiate an attack hitting a RANK for some bonus damage
            combatController.EngageCombatRound(HitMe, Mathf.Max(1, tempArray[HitMe] - 3));
        }
    }
    public void UpdateHP()
    {
        for (int i = 0; i < slotMachineRolls.Length; i++)
        {
            CharUI[i].textMeshPro.text = characterManagers[i].HP.ToString();
        }

    }
}