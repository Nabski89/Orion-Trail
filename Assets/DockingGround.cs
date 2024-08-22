using System.Collections;
using System.Collections.Generic;
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
    public float DropSpeed = 20;
    public float GroundSpeed = 100;
    public float Gravity = 5;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DropSpeed += Time.deltaTime * Gravity;
        GroundMovement.localPosition +=
         Vector3.up * Time.deltaTime * DropSpeed
         -
         Vector3.right * Time.deltaTime * GroundSpeed
         ;

    }
}
