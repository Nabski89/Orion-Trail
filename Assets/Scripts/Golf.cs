using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class Golf : MonoBehaviour
{
    //Used for picking our Range 
    bool PickRange;
    public GameObject Edge1;
    public GameObject Edge1b;
    public GameObject Edge2;
    public GameObject Edge2b;
    public float RangeSpeed = 20f;
    public float rotationAmount = 0;
    public float RangeMax = 30;
    public float RangeMin = 30;

    //Used for picking our Distance 
    public GameObject PowerSlider;
    public float PowerSpeed = 20f;
    public float PowerAmount = 0;
    public float PowerMax = 30;
    public float PowerMin = 30; //minimum of 1 or it fails

    //used for the ball
    public GameObject BeaconBall;
    public ShipController TheShip;
    public Supplies TheSupplies;
    public float FuelUsage;

    //end location
    public float StoredForward;
    public float StoredSpread;
    public float StoredPower;

    bool PickPower;
    public GameObject FuelTankSpawn;
    public GameObject FuelTankParent;
    void Update()
    {
    }
    public void StartGolf(float RotateValue, float RotateMin, float PowerValue, float PowMin, float FuelReq)
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
            RangeMin = RotateMin;
            PickRange = true;
            PowerAmount = 0;
            PowerMax = PowerValue;
            PowerMin = PowMin;
            FuelUsage = FuelReq;
            //scale the edges to show how far a thing goes
            Edge1.transform.localScale = new Vector3(PowerMin, 1, 1);
            Edge1b.transform.localScale = new Vector3((PowerMax - PowerMin) / PowerMin, 1, 1);
            Edge2.transform.localScale = new Vector3(PowerMin, 1, 1);
            Edge2b.transform.localScale = new Vector3((PowerMax - PowerMin) / PowerMin, 1, 1);
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
            Instantiate(FuelTankSpawn, FuelTankParent.transform);
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


    void EndGolf()
    {
        ReadyClickArea();
        Edge1.SetActive(false);
        Edge2.SetActive(false);
        PowerSlider.SetActive(false);
        rotationAmount = 0;
        PickRange = false;
        PickPower = false;
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
        GameObject NewBeacon = Instantiate(BeaconBall, transform.position, transform.rotation, transform);
        //use up our fuel, and if we don't have enough our shot goes wide AND only half as far
        TheSupplies.Fuel = Mathf.Max(0, TheSupplies.Fuel - FuelUsage);
        if (TheSupplies.Fuel == 0)
        {
            StoredSpread = StoredSpread * 2;
            StoredPower = StoredPower / 2;
        }
        NewBeacon.GetComponent<TravelBall>().TargetLocation = SelectRandomPositionWithinCone(StoredSpread, StoredForward, StoredPower);
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