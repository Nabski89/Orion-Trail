using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelBar : MonoBehaviour
{
    public RectTransform Target;
    public RectTransform Fuel;
    public Vector3 FullPosition; // The up position
    public Vector3 EmptyPosition; // The down position
    public float FillSpeed = 2; //how long it takes to get to the top
    public float MaxFill = 80;
    public float MalfChance = 10;
    // Start is called before the first frame update
    void Start()
    {
        Target.anchoredPosition = new Vector3(0, Random.Range(10, MaxFill), 0);
        StartCoroutine(Fill());
    }


    IEnumerator Fill()
    {
        float elapsedTime = 0f;

        while (Fuel.localPosition.y < FullPosition.y)
        {
            if (Input.GetMouseButtonDown(0))
            {
                FuelAmount();
                yield break;
            }
            Fuel.localPosition = Vector3.Lerp(EmptyPosition, FullPosition, elapsedTime / FillSpeed);
            elapsedTime += Time.deltaTime;

            if (Random.Range(0, 100) < MalfChance)
            {
                Fuel.localPosition = Vector3.Lerp(EmptyPosition, FullPosition, elapsedTime / FillSpeed);
                elapsedTime += Time.deltaTime;
            }
            yield return null;
        }
        Fuel.localPosition = FullPosition;
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
        while (Fuel.localPosition.y > EmptyPosition.y)
        {
            if (Input.GetMouseButtonDown(0))
            {
                FuelAmount();
                yield break;
            }
            Fuel.localPosition = Vector3.Lerp(FullPosition, EmptyPosition, elapsedTime / (FillSpeed / 10));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Fuel.localPosition = EmptyPosition;
        StartCoroutine(Fill());
    }

    void FuelAmount()
    {
        Golf GolfReference = GetComponentInParent<GenericManager>().GolfReference;
        GolfReference.FeedFuelValue(Mathf.Abs(Fuel.anchoredPosition.y - Target.anchoredPosition.y));
        Debug.Log("The fuel value for this cell was off from the target by " + Mathf.Abs(Fuel.anchoredPosition.y - Target.anchoredPosition.y));
    }
}
