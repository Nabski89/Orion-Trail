using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class Golf : MonoBehaviour
{
    DialogText DialogBox;
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
                Selected();
                break;
            case 2:
                Aimed();
                break;
            case 3:
                Fueled();
                break;
            case 4:
                Launched();
                break;
            default:
                Debug.Log("Unknown command in golf");
                break;
        }
    }
    public void StartGolf()
    {
        TriggerButton.SetActive(true);
        DialogBox.TEXTBOX = "Preparing ship for warp. Selecting targeting probe driver.";
        DialogBox.NewText();
        ReadyStage = 1;
        for (int i = 0; i < StageLights.Length; i++)
            StageLights[i].SetActive(false);
        StageLights[0].SetActive(true);
    }
    public GameObject PreviewLine;
    public GameObject RotatePointerUp;
    public GameObject RotatePointerDown;
    void Selected()
    {
        customDirections = FindObjectsOfType<GOLFDirection>();
        if (customDirections[0] != null)
        {
            DialogBox.TEXTBOX += "\nProbe locked in. Ready to select Target Orientation. ";
            ReadyStage = 2;
            StageLights[1].SetActive(true);
            StageLights[2].SetActive(true);
            PreviewLine.SetActive(true);
            PowerAmount = customDirections.Length;
            PreviewLine.transform.localScale = new Vector3(PowerAmount, 1, 1);
            RotatePointerUp.SetActive(true);
            RotatePointerDown.SetActive(true);
        }
    }
    void Aimed()
    {
        RotatePointerUp.SetActive(false);
        RotatePointerDown.SetActive(false);
        StageLights[3].SetActive(true);
        StageLights[4].SetActive(true);
        DialogBox.TEXTBOX += "\nTarget Orientation locked in. Begin Fueling Procedures. ";
        ReadyStage = 3;


        FuelAmount = 0;
        foreach (GOLFDirection Direction in customDirections)
        {
            //this is where we spawn the fuel tanks

            //  if (TheSupplies.Fuel > 0)
            {
                FuelAmount += 1;
                Debug.Log("Spawn a fuel tank");
                Instantiate(FuelTankSpawn, FuelTankParent);
                //todo figure out why this isn't spawning
                TheSupplies.SubtractFuel(1);

            }
            //   else
            {
                Debug.Log("Spawn a damaged fuel tank");
                Instantiate(BrokenTankSpawn, FuelTankParent);
            }
        }
    }
    void Fueled()
    {
        StageLights[5].SetActive(true);

        ReadyStage = 4;
        DialogBox.TEXTBOX += "\nYeah okay togopal I guess you could call that a fueling event, but it looks like you got badly fucked up by quarternoins AGAIN";
        //if we have fuel, use it otherwise use a shitty broken fuel
        //TODO make lacking fuel more punishing
        Launched();
    }

    void Launched()
    {
        TriggerButton.SetActive(false);
        DialogBox.TEXTBOX += "\nFueling Complete. Probe is launched, I repeat probe is launched. Brace for warp. ";
        GameObject NewBeacon = Instantiate(BeaconBall, TheShip.transform.position, PreviewLine.transform.rotation);
        //use up our fuel, and if we don't have enough our shot goes wide AND only half as far
        //push forward our values then activate the ball
        TravelBall TheBeacon = NewBeacon.GetComponent<TravelBall>();
        TheBeacon.initialSpeed = 1f;
        TheBeacon.TheShip = TheShip;
        TheBeacon.ActivateBeacon();

        EndGolf();
    }
    void EndGolf()
    {
        //clear out the old fuel tanks
        for (int i = FuelTankParent.transform.childCount - 1; i >= 0; i--)
            Destroy(FuelTankParent.transform.GetChild(i).gameObject);
        PreviewLine.SetActive(false);
    }
}