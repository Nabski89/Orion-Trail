using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFood : MonoBehaviour
{
    public int FoodMod;
    public int LowerRandom;
    public int UpperRandom;
    // Start is called before the first frame update, +1 is because random ints don't ever hit the max
    void Start()
    {
        int FuelToAdd = FoodMod + Random.Range(LowerRandom, UpperRandom + 1);
        Debug.Log("Added " + FoodMod + " Food");
        GenericManager Reference = GetComponentInParent<GenericManager>();
        Debug.Log("The generic manager is "+ Reference);

        Reference.MainTextReference.TEXTBOX += "<br>" + FuelToAdd + " meals have been added to the fridge.";
        Supplies SupplyReference = GetComponentInParent<Supplies>();
        SupplyReference.Food += FuelToAdd;

        //TODO Split food into raw and cooked food
        /*
        int FuelSurplus = (int)(SupplyReference.Fuel - SupplyReference.MaxFuel);
        //     FuelSurplus = SupplyReference.Fuel;
        if (FuelSurplus > 0)
        {
            Reference.MainTextReference.TEXTBOX += "<br>You have no more fuel reserve space...";

            Reference.MainTextReference.TEXTBOX += "<br>Are you really going to jettison starship fuel into the void?... You are, Stop... STOP!";
            Reference.ShipReference.CrewMoraleChange(-1 * FuelSurplus, true);
            SupplyReference.Fuel = SupplyReference.MaxFuel;
        }
*/
    }
}
