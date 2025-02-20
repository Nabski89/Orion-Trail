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
    void Update()
    {
        StopRoll();
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
            //every time we roll costs at least a second
            combatController.RealTimeTimer += 1f;
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
        //if we are back to paused time then we can stop the slot machine
        if (combatController.RealTimeTimer < 0.1f)
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
        int[] DamageCount = new int[3];
        DamageCount[0] = 0;
        DamageCount[1] = 0;
        DamageCount[2] = 0;
        if (Selectable > 0)
        {
            Selectable -= 1;

            //cycle through each of our things in the selected slot and activate them
            foreach (SlotMachineRoll SlotCharacter in slotMachineRolls)
            {
                SlotValue Slot = SlotCharacter.transform.GetChild(Selected).GetComponent<SlotValue>();
                //increase the timer
                combatController.RealTimeTimer += Slot.Cooldown;
                Atk += Slot.Attack;
                Block += Slot.Block;
                Special += Slot.Buff;
                if (Slot.Rank1 == true)
                    DamageCount[0] += 1;
                if (Slot.Rank2 == true)
                    DamageCount[1] += 1;
                if (Slot.Rank3 == true)
                    DamageCount[2] += 1;
            }
            //check who is the largest guy to hit
            int HitMe = 0;
            if (DamageCount[0] >= DamageCount[1] && DamageCount[0] >= DamageCount[2])
                HitMe = 0;
            else if (DamageCount[1] >= DamageCount[0] && DamageCount[1] >= DamageCount[2])
                HitMe = 1;
            else
                HitMe = 2;


            genericManager.MainTextReference.TEXTBOX += "<br>Attack for " + Atk + ". Block for " + Block + ". Special count of " + Special + " and it is going to hit in rank " + HitMe;
            if (DamageCount[HitMe] == 4)
                genericManager.MainTextReference.TEXTBOX += " 1 Bonus";
            if (DamageCount[HitMe] == 5)
                genericManager.MainTextReference.TEXTBOX += " 2 Bonus";

            //initiate an attack hitting a RANK for some bonus damage
            combatController.EngageCombatRound(HitMe, Mathf.Max(0, DamageCount[HitMe] - 3));
        }
    }
    public void UpdateHP()
    {
        for (int i = 0; i < slotMachineRolls.Length; i++)
        {
            CharUI[i].textMeshPro.text = characterManagers[i].HP.ToString();
        }
    }
    public void DisableSelectedHighlight()
    {
        SlotValue[] slotValues = GetComponentsInChildren<SlotValue>();
        foreach (SlotValue slotValue in slotValues)
        {
            if (slotValue.SelectedOutline != null)
                slotValue.SelectedOutline.gameObject.SetActive(false);
        }
    }
}