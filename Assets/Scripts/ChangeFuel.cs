using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFuel : MonoBehaviour
{
    public int FuelMod;
    public int LowerRandom;
    public int UpperRandom;
    // Start is called before the first frame update, +1 is because random ints don't ever hit the max
    void Start()
    {
        int FuelToAdd = FuelMod + Random.Range(LowerRandom, UpperRandom + 1);
        Debug.Log("Added " + FuelMod + " Fuel");
        GenericManager Reference = GetComponentInParent<GenericManager>();
        Reference.MainTextReference.TEXTBOX += "<br>" + FuelToAdd + " Fuel has been added to the ship fuel tank.";
        Supplies SupplyReference = GetComponentInParent<Supplies>();
        SupplyReference.Fuel += FuelToAdd;
        int FuelSurplus = (int)(SupplyReference.Fuel - SupplyReference.MaxFuel);
        if (FuelSurplus > 0)
        {
            Reference.MainTextReference.TEXTBOX += "<br>You have no more fuel reserve space...";

            Reference.MainTextReference.TEXTBOX += "<br>Are you really going to jettison starship fuel into the void?... You are, Stop... STOP!";
            Reference.ShipReference.CrewMoraleChange(-1 * FuelSurplus, true);
            SupplyReference.Fuel = SupplyReference.MaxFuel;
        }

    }
}
