using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public int Ammo;
    public int AmmoChance;
    public int Fuel;
    public int FuelChance;
    public int Food;
    public int FoodChance;
    public GameObject Special;
    public int SpecialChance;
    void Start()
    {
        GetComponentInParent<Supplies>().Fuel +=  Random.Range(-1, Fuel);;
    }

    // Update is called once per frame
}
