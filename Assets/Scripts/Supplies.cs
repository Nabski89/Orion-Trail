using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supplies : MonoBehaviour
{
    //use one food per person per move
    public int Food = 80;
    //I'm assuming you have to do 10 stops and each one takes 3-5 fuel depening on distance (in game default is 5)
    public float Fuel = 10;
    public float MaxFuel = 25;
    //Hull Parts
    public int RepairHull = 1;
    //Electronics
    public int RepairElectronic = 1;
    //Engine Parts
    public int RepairEngine = 1;

    public void SubtractFuel(float fuelDown)
    {
        Fuel -= fuelDown;
    }
}
