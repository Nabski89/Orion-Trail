using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineManager : MonoBehaviour
{
    public SlotMachineRoll[] slotMachineRolls;
    public GenericManager genericManager;
    void Start()
    {
        genericManager = GetComponentInParent<GenericManager>();
        // Get all SlotMachineRoll components in the children

        slotMachineRolls = GetComponentsInChildren<SlotMachineRoll>();
    }
    public CharacterBulkManager CrewShipReference;
    void ActivateSlots()
    {
        CharacterManager[] characterManagers = CrewShipReference.GetComponentsInChildren<CharacterManager>();
        // Perform an action for each element in the array
        for (int i = 0; i < slotMachineRolls.Length; i++)
        {
            slotMachineRolls[i].ColorImage1.color = characterManagers[i].CharacterColor[0];
            slotMachineRolls[i].ColorImage2.color = characterManagers[i].CharacterColor[1];
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void StartRoll()
    {
        ActivateSlots();
        Selectable = 1;
        Atk = 0;
        Block = 0;
        Special = 0;
        foreach (SlotMachineRoll roll in slotMachineRolls)
            roll.StartRouletteButton();
    }
    public void StopRoll()
    {
        float delay = 0;
        foreach (SlotMachineRoll roll in slotMachineRolls)
        {
            delay += 0.25f;
            roll.StopRouletteButton(delay);
        }
    }
    public int Atk;
    public int Block;
    public int Special;
    public int Selectable = 1;
    public void Select(int Selected)
    {
        if (Selectable > 0)
        {
            Selectable -= 1;
            foreach (SlotMachineRoll SlotCharacter in slotMachineRolls)
            {
                SlotValue Read = SlotCharacter.transform.GetChild(Selected).GetComponent<SlotValue>();
                Atk += Read.Attack;
                Block += Read.Block;
                Special += Read.Buff;

            }
            genericManager.MainTextReference.TEXTBOX += "<br>Attack for " + Atk + ". Block for " + Block + ". Special count of " + Special;
        }
    }
}