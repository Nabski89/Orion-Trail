using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DockingMiniUI : MonoBehaviour
{
    public RectTransform MovingGroundTarget;
    public RectTransform UIShip;
    public Vector2 StartingPosition;
    public float XRatio = 72 / 2600;
    public float YRatio = 70 / 400;

    //MAIN MATH
    //start is -400x-300
    //landed is -3000x100
    //X of 2600 Y of 400
    //Missed the target is -3400

    //UI MATH
    //Start -40x30
    //Landed 32x-40
    // X of 72 Y of 70

    //That is a 
    void Update()
    {
        float X = MovingGroundTarget.transform.localPosition.x * XRatio;
        float Y = MovingGroundTarget.transform.localPosition.y * YRatio;
        UIShip.localPosition = StartingPosition - new Vector2(X, Y);
    }
}
