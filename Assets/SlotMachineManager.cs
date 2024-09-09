using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachineManager : MonoBehaviour
{
    public SlotMachineRoll[] slotMachineRolls;

    void Start()
    {
        // Get all SlotMachineRoll components in the children
        slotMachineRolls = GetComponentsInChildren<SlotMachineRoll>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartRoll()
    {
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
    public int Selectable = 3;
    public void Select(int Selected)
    {
        Selectable -= 1;
        SlotValue Read = slotMachineRolls[Selected].transform.GetChild(1).GetComponent<SlotValue>();
        Atk += Read.Attack;
        Block += Read.Block;
        Special += Read.Buff;
    }
}