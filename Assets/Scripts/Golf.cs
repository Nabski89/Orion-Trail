using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class Golf : MonoBehaviour
{
    //Used for picking our Range 
    bool PickRange;
    public GameObject Edge1;
    public GameObject Edge2;
    public float RangeSpeed = 20f;
    public float rotationAmount = 0;
    public float RangeMax = 30;

    //Used for picking our Distance 
    public GameObject PowerSlider;
    public float PowerSpeed = 20f;
    public float PowerAmount = 0;
    public float PowerMax = 30;

    //used for the ball
    public GameObject BeaconBall;
    public ShipController TheShip;

    //end location
    public float StoredForward;
    public float StoredSpread;
    public float StoredPower;

    bool PickPower;
    void Update()
    {
        if (PickPower == true)
        {
            if (Input.GetMouseButtonDown(0))
                PowerAmount = 0;

            if (Input.GetMouseButton(0))
            {
                PowerAmount += PowerSpeed * Time.deltaTime;

                if (PowerAmount > PowerMax)
                {
                    PowerAmount = 0;
                    PowerMax = PowerMax * 1.1f;
                }
                ScaleIt();
            }
            if (Input.GetMouseButtonUp(0))
            {
                RotateIt(rotationAmount);
                StoredPower = PowerAmount;
                SpawnBeacon();
            }

        }
        if (PickRange == true)
        {
            if (Input.GetMouseButtonDown(0))
                RangeSpeed = Mathf.Abs(RangeSpeed);
            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                //Move the range around
                rotationAmount = Mathf.Clamp(rotationAmount + RangeSpeed * Time.deltaTime, 0, RangeMax);
                if (rotationAmount == RangeMax || rotationAmount == 0)
                    RangeSpeed = RangeSpeed * -1;
                RotateIt(rotationAmount);
            }
            if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                PickRange = false;
                PickPower = true;
                StoredSpread = rotationAmount;
                PowerSlider.SetActive(true);
                RotateIt(rotationAmount);
            }
        }
        if (Input.GetMouseButtonDown(1))
            EndGolf();
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

    void ScaleIt()
    {
        PowerSlider.transform.localScale = new Vector3(PowerAmount, 1, 1);
    }

    public void StartGolf()
    {
        Edge1.SetActive(true);
        Edge2.SetActive(true);
        PickRange = true;
    }

    void EndGolf()
    {
        Edge1.SetActive(false);
        Edge2.SetActive(false);
        PowerSlider.SetActive(false);
        rotationAmount = 0;
        PickRange = false;
        PickPower = false;
    }

    void SpawnBeacon()
    {
        GameObject NewBeacon = Instantiate(BeaconBall, transform.position, transform.rotation);
        NewBeacon.GetComponent<TravelBall>().TargetLocation = SelectRandomPositionWithinCone(StoredSpread, StoredForward, StoredPower);
        NewBeacon.GetComponent<TravelBall>().ParentShip = TheShip;
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
        return direction.normalized * distance;
    }
}