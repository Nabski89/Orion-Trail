using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelBar : MonoBehaviour
{
    public RectTransform Fuel;
    public Vector3 FullPosition; // The up position
    public Vector3 EmptyPosition; // The down position
    public float FillSpeed = 2; //how long it takes to get to the top
    Golf GolfParent;
    public bool Opened = true;
    public GOLFDirection DirectionReference;
    // Start is called before the first frame update
    void Start()
    {
        GolfParent = GetComponentInParent<Golf>();
        StartFilling();
    }
    public void StartFilling()
    {
        StartCoroutine(Fill());
    }
    public void ToggleFuel()
    {
        //toggle if it is open or not
        if (Opened == true)
        {
            Opened = false;
            StartCoroutine(Fill());
        }
    }
    IEnumerator Fill()
    {
        float elapsedTime = 0f;

        while (Fuel.localPosition.y < FullPosition.y && Opened == true
        //         && GolfParent.FuelAmount > 0
         )
        {
            Fuel.localPosition = Vector3.Lerp(EmptyPosition, FullPosition, elapsedTime / FillSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        FuelAmount();
        if (Opened == true)
            StartCoroutine(Drain());

    }

    IEnumerator Drain()
    {
        float elapsedTime = 0f;
        //just a little delay
        while (elapsedTime < 0.1f)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;
        while (Fuel.localPosition.y > EmptyPosition.y && Opened == true)
        {
            Fuel.localPosition = Vector3.Lerp(FullPosition, EmptyPosition, elapsedTime / (FillSpeed / 10));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        FuelAmount();
        if (Opened == true)
            StartCoroutine(Fill());
    }
    //this ends our filling
    void FuelAmount()
    {
        Debug.Log("The fuel value for this cell is " + (Fuel.anchoredPosition.y - EmptyPosition.y) / ((FullPosition.y - EmptyPosition.y) * 2 / 3));
        DirectionReference.FuelAmount = (Fuel.anchoredPosition.y - EmptyPosition.y) / ((FullPosition.y - EmptyPosition.y) * 2 / 3);
    }
}
