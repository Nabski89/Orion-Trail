using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUIEffect : MonoBehaviour
{
    public GameObject TrailObject;
    public RectTransform canvasElement; // Assign the Canvas element in the Inspector
    public Vector3 startPosition; // Starting position
    public Vector3 endPosition; // Ending position
    public float totalDuration = 5.0f; // Total duration to move from start to end
    public float HeightOffset;
    private float elapsedTime = 0f;


    void Start()
    {
        if (canvasElement == null)
        {
            canvasElement = GetComponent<RectTransform>();
        }

        canvasElement.anchoredPosition = startPosition;

    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        float t = elapsedTime / totalDuration;
        Vector3 currentPosition = Vector3.one;
        if (elapsedTime <= totalDuration)
        {
            //Parabolic function, which I had to get from the internet
            float parabolicT = 4 * t * (1 - t);

            currentPosition = Vector3.Lerp(startPosition, endPosition, t);
            currentPosition.y += parabolicT * HeightOffset;
            canvasElement.anchoredPosition = currentPosition;
        }

        else
        {
            if (elapsedTime >= totalDuration * 1.5f)
                Destroy(transform.gameObject);
         //   canvasElement.anchoredPosition = startPosition;
        //    elapsedTime = 0;
        }
    }
}