using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpinRate : MonoBehaviour
{
    public bool SpeedUp;
    public bool SlowDown;
    // Update is called once per frame
    void Update()
    {

        if (SpeedUp == true)
        {
            Spin[] spinComponents = GetComponentsInChildren<Spin>();
            foreach (Spin spinComponent in spinComponents)
            {
                // Multiply the RotationSpeed by 10
                spinComponent.rotationSpeed *= 10;
            }
        }
        SpeedUp = false;


        if (SlowDown == true)
        {
            Spin[] spinComponents = GetComponentsInChildren<Spin>();
            foreach (Spin spinComponent in spinComponents)
            {
                // Multiply the RotationSpeed by 10
                spinComponent.rotationSpeed /= 10;
            }
        }
        SlowDown = false;
    }
}