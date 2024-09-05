using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DockingGround : MonoBehaviour
{
    //have 3 different gravity types for planet, celestial, space
    // Start is called before the first frame update


    //Y Distance = 600;
    //X Distance = 3000;

    //want it to take about 30? seconds to land
    public RectTransform GroundMovement;
    public RectTransform LandingZone;
    DockingShip Ship;
    public float DropSpeed = 20;
    public float GroundSpeed = 100;
    public float Gravity = 5;
    void Start()
    {
        Ship = GetComponent<DockingShip>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GroundMovement.localPosition.y < 400)
        {
            Move();
        }
        else
            Touchdown();
    }
    public void Reset()
    {
        LandingZone.localPosition = new Vector2(0, -20);
        GroundMovement.localPosition = new Vector2(0, 0);
        DropSpeed = 20;
        GroundSpeed = 100;
    }
    public void Move()
    {
        //change the drop speed due to gravity
        DropSpeed += Time.deltaTime * Gravity;
        //increase our speeds due to the rocket engine
        DropSpeed += 50 * Ship.transform.right.y * Time.deltaTime * Ship.movementSlider.value;
        GroundSpeed += 50 * Ship.transform.right.x * Time.deltaTime * Ship.movementSlider.value;
        //reduce the speed due to the airbrakes
        //  Debug.Log(Ship.Ship.up);
        DropSpeed -= 10 * Ship.ThrustPower * Ship.Ship.up.y * Time.deltaTime;
        GroundSpeed += 10 * Ship.ThrustPower * Ship.Ship.up.x * Time.deltaTime;
        GroundMovement.localPosition += Vector3.up * Time.deltaTime * DropSpeed - Vector3.right * Time.deltaTime * GroundSpeed;
    }
    void Touchdown()
    {
        GetComponentInParent<DockMinigame>().EndSkill();
    }
}
