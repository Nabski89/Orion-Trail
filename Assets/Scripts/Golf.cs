using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class Golf : MonoBehaviour
{
    public DialogText DialogBox;
    //Used for picking our Range 

    //Used for picking our Distance 
    public float PowerAmount = 0;

    //used for the ball
    public GameObject BeaconBall;
    public ShipController TheShip;
    Supplies TheSupplies;
    //end location
    public GameObject FuelTankSpawn;
    public GameObject BrokenTankSpawn;
    public Transform FuelTankParent;
    public float FuelAmount;
    GOLFDirection[] customDirections;
    public GameObject[] StageLights;
    public GameObject TriggerButton;
    public void Start()
    {
        DialogBox = GetComponentInParent<GenericManager>().MainTextReference;
        TheSupplies = GetComponentInParent<Supplies>();
    }
    int ReadyStage = 0;
    public void AdvanceGolf()
    {
        switch (ReadyStage)
        {
            case 1:
                Aimed();
                break;
            case 2:
                Fueled();
                break;
            case 3:
                Launched();
                break;
            default:
                Debug.Log("Unknown command in golf");
                break;
        }
    }
    public void StartGolf()
    {
        //enable the ability to select our probe
        SelectProbeMinus.SetActive(true);
        SelectProbePlus.SetActive(true);
        TriggerButton.SetActive(true);
        DialogBox.TEXTBOX = "Preparing ship for warp. Selecting targeting probe driver.";
        DialogBox.NewText();
        ReadyStage = 1;
        for (int i = 0; i < StageLights.Length; i++)
            StageLights[i].SetActive(false);
        StageLights[0].SetActive(true);

        //clear out the old fuel tanks
        for (int i = FuelTankParent.transform.childCount - 1; i >= 0; i--)
            Destroy(FuelTankParent.transform.GetChild(i).gameObject);
    }
    public GameObject SelectProbeMinus;
    public GameObject SelectProbePlus;
    public GameObject PreviewLine;
    public GameObject RotatePointerUp;
    public GameObject RotatePointerDown;
    //this is called from the MovePutterSelect script
    public void Selected()
    {
        //
        customDirections = FindObjectsOfType<GOLFDirection>();
        if (customDirections.Length > 0)
        {
            DialogBox.TEXTBOX += "\nProbe locked in. Ready to select Target Orientation. ";

            StageLights[1].SetActive(true);
            StageLights[2].SetActive(true);
            PreviewLine.SetActive(true);
            PowerAmount = customDirections.Length;
            PreviewLine.transform.localScale = new Vector3(PowerAmount, 1, 1);
            RotatePointerUp.SetActive(true);
            RotatePointerDown.SetActive(true);
        }
        else
        {
            DialogBox.TEXTBOX += "\nInvalid Probe Selected.";
            //     PreviewLine.transform.localScale = new Vector3(0, 1, 1);
            PreviewLine.SetActive(false);
            StageLights[0].SetActive(true);
            StageLights[1].SetActive(false);
            RotatePointerUp.SetActive(false);
            RotatePointerDown.SetActive(false);
        }
    }
    void Aimed()
    {
        if (StageLights[1].activeSelf == false)
            DialogBox.TEXTBOX += "Invalid Probe Selected. Select A Valid Probe To Continue.";
        else
        {
            SelectProbeMinus.SetActive(false);
            SelectProbePlus.SetActive(false);
            RotatePointerUp.SetActive(false);
            RotatePointerDown.SetActive(false);
            StageLights[3].SetActive(true);
            StageLights[4].SetActive(true);
            DialogBox.TEXTBOX += "\nTarget Orientation locked in. Begin Fueling Procedures. ";
            ReadyStage = 2;


            FuelAmount = 0;
            foreach (GOLFDirection Direction in customDirections)
            {
                //this is where we spawn the fuel tanks

                if (TheSupplies.Fuel > 0)
                {
                    FuelAmount += 1;
                    Debug.Log("Spawn a fuel tank");
                    GameObject SpawnedTank = Instantiate(FuelTankSpawn, FuelTankParent);
                    SpawnedTank.GetComponent<FuelBar>().DirectionReference = Direction;
                    //todo figure out why this isn't spawning
                    TheSupplies.SubtractFuel(1);

                }
                else
                    Instantiate(BrokenTankSpawn, FuelTankParent);
            }
        }
    }
    int BadAtFueling = 0;
    void Fueled()
    {
        if (StageLights[5].activeSelf == (true))
            Launched();
        else
        {
            BadAtFueling += 1;
            if (BadAtFueling < 10)
                DialogBox.TEXTBOX += "\nComplete fueling before launch";
            if (BadAtFueling == 10)
                DialogBox.TEXTBOX += "\nCan you please just flip the fuel switch";
            if (BadAtFueling == 20)
                DialogBox.TEXTBOX += "\nWhy are you like this";
        }
        //if we have fuel, use it otherwise use a shitty broken fuel
        //TODO make lacking fuel more punishing

    }

    void Launched()
    {
        TriggerButton.SetActive(false);
        DialogBox.TEXTBOX += "\nFueling Complete. Probe is launched, I repeat probe is launched. Brace for warp. ";
        GameObject NewBeacon = Instantiate(BeaconBall, TheShip.transform.position, PreviewLine.transform.rotation);
        //use up our fuel, and if we don't have enough our shot goes wide AND only half as far
        //push forward our values then activate the ball
        TravelBall TheBeacon = NewBeacon.GetComponent<TravelBall>();
        //        TheBeacon.initialSpeed = 1f;
        TheBeacon.TheShip = TheShip;

        //Set our custom directions to onto the probe so they can be accessed
        foreach (GOLFDirection Direction in customDirections)
            Instantiate(Direction.transform, TheBeacon.transform);

        TheBeacon.ActivateBeacon();

        EndGolf();
    }
    void EndGolf()
    {
        PreviewLine.SetActive(false);
    }
}