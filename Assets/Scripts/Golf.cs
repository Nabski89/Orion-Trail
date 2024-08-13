using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class Golf : MonoBehaviour
{
    //Used for picking our Range 
    public GameObject Edge1;
    public GameObject Edge1b;
    public GameObject Edge2;
    public GameObject Edge2b;
    public float rotationAmount = 0;
    public float RangeMax = 30;

    //Used for picking our Distance 
    public GameObject PowerSlider;
    public float PowerAmount = 0;

    //used for the ball
    public GameObject BeaconBall;
    public ShipController TheShip;
    Supplies TheSupplies;
    public float FuelUsage;

    //end location
    public float StoredForward;
    public float StoredSpread;
    public float StoredPower;
    public GameObject FuelTankSpawn;
    public GameObject BrokenTankSpawn;
    public GameObject FuelTankParent;
    public void Start()
    {
        TheSupplies = GetComponentInParent<Supplies>();
    }
    public void StartGolf(float RotateValue, float FuelFree, float FuelReq)
    {
        //check that we don't already have a ball out then activate our layout stuffs
        TravelBall BallCheck = GetComponentInChildren<TravelBall>();
        if (BallCheck == null)
        {
            Edge1.SetActive(true);
            Edge2.SetActive(true);
            PowerSlider.SetActive(true);
            rotationAmount = RangeMax;

            RangeMax = RotateValue;
            PowerAmount = FuelReq + FuelFree;
            FuelUsage = FuelReq;
            //scale the edges to show how far a thing goes
            Edge1.transform.localScale = new Vector3(PowerAmount, 1, 1);
            Edge1b.transform.localScale = Vector3.zero;
            //       Edge1b.transform.localScale = new Vector3((FuelReq - FuelFree) / FuelReq, 1, 1);
            Edge2.transform.localScale = new Vector3(PowerAmount, 1, 1);
            Edge2b.transform.localScale = Vector3.zero;
            //         Edge2b.transform.localScale = new Vector3((FuelReq - FuelFree) / FuelReq, 1, 1);
            StartCoroutine(PickPosition());
            RotateIt(rotationAmount);
        }
    }
    IEnumerator PickPosition()
    {
        while (true)
        {
            RotateIt(RangeMax);
            if (Input.GetMouseButtonDown(1))
            {
                EndGolf();
                yield break;
            }
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                SelectPower();
                yield break;
            }
            yield return null;
        }
    }

    void RotateIt(float rotationValue)
    {
        // Get the mouse position in screen space
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = (Vector2)(mousePosition - transform.position);

        // Calculate the angle from the current object's position to the mouse position
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the object to point towards the mouse position
        Edge1.transform.rotation = Quaternion.AngleAxis(angle + rotationValue, Vector3.forward);
        Edge2.transform.rotation = Quaternion.AngleAxis(angle - rotationValue, Vector3.forward);
        PowerSlider.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //too hard to bring this out, turn the quaternoin into the angle from right then we can do some trig I guess to get it back
        StoredForward = Quaternion.AngleAxis(angle, Vector3.forward).eulerAngles.z;
    }


    public void SelectPower()
    {
        if (FuelUsage > 0)
        {
            FuelUsage -= 1;
            Debug.Log("Spawn a fuel tank");
            //if we have fuel, use it otherwise use a shitty broken fuel
            //TODO make lacking fuel more punishing
            if (TheSupplies.Fuel > 0)
            {
                TheSupplies.SubtractFuel(1);
                Instantiate(FuelTankSpawn, FuelTankParent.transform);
            }
            else
                Instantiate(BrokenTankSpawn, FuelTankParent.transform);

        }
        else
            SpawnBeacon();
        PowerSlider.transform.localScale = new Vector3(PowerAmount, 1, 1);

    }
    //after we do each fuel check if we need to do another
    public void FeedFuelValue(float fuelOffset)
    {
        SelectPower();
    }

    public ButtonCoverPress CoverButton;
    void EndGolf()
    {
        ReadyClickArea();
        //reset the fuel button cover
        if (CoverButton != null)
            CoverButton.PressButton();
        Edge1.SetActive(false);
        Edge2.SetActive(false);
        PowerSlider.SetActive(false);
        rotationAmount = 0;

    }
    void ReadyClickArea()
    {
        // Iterate through all children of the target GameObject
        for (int i = FuelTankParent.transform.childCount - 1; i >= 0; i--)
        {
            // Destroy each child GameObject
            Destroy(FuelTankParent.transform.GetChild(i).gameObject);
        }
    }
    void SpawnBeacon()
    {
        GameObject NewBeacon = Instantiate(BeaconBall, transform.position, PowerSlider.transform.rotation);
        //use up our fuel, and if we don't have enough our shot goes wide AND only half as far
        TheSupplies.Fuel = Mathf.Max(0, TheSupplies.Fuel - FuelUsage);
        if (TheSupplies.Fuel == 0)
        {
            StoredSpread = StoredSpread * 2;
            StoredPower = StoredPower / 2;

            //TODO play an error sound when we have no fuel
        }
        //push forward our values then activate the ball
        TravelBall TheBeacon = NewBeacon.GetComponent<TravelBall>();
        TheBeacon.TargetLocation = SelectRandomPositionWithinCone(StoredSpread, StoredForward, StoredPower);
        TheBeacon.initialSpeed = PowerAmount;
        TheBeacon.TheShip = GetComponentInParent<ShipController>();
        TheBeacon.ActivateBeacon();

        EndGolf();
    }

    // Function to select a random position within a cone, from GPT
    public Vector3 SelectRandomPositionWithinCone(float angle, float forwardDirection, float distance)
    {
        angle = forwardDirection + Random.Range(-angle, angle);
        angle = angle * Mathf.Deg2Rad;
        // Calculate direction within cone
        Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
        // Scale direction by distance, normalize to 1 first
        return direction.normalized * distance + transform.position;
    }


}