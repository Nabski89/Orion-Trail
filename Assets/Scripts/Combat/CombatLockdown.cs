using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatLockdown : MonoBehaviour
{
    public float Delay = 0;
    public float ReverseDelay = 0;
    public float Duration = 1.0f;
    public float ReverseDuration = 1.0f;
    // Target scale factor
    public bool Scale;
    public float targetScale = 0.9f;
    public bool Move;
    public Vector3 InitialPosition;
    public Vector3 FinalPosition;
    // Reference to the RectTransform
    private RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Lockdown()
    {
        if (Scale == true)
            StartCoroutine(ScaleCoroutineDown());
        if (Move == true)
            StartCoroutine(Move1());

    }
    public void UnLockdown()
    {
        if (Scale == true)
            ScaleCoroutineUp();
        if (Move == true)
            StartCoroutine(Move2());
    }

    IEnumerator ScaleCoroutineDown()
    {
        yield return new WaitForSeconds(Delay);
        // Store the original scale and pivot
        Vector3 originalScale = rectTransform.localScale;

        // Calculate the target scale
        Vector3 targetScaleVector = new Vector3(targetScale, targetScale, 1);

        float elapsedTime = 0f;

        while (elapsedTime < Duration)
        {
            // Interpolate the scale
            rectTransform.localScale = Vector3.Lerp(originalScale, targetScaleVector, elapsedTime / Duration);

            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the final scale is set
        rectTransform.localScale = targetScaleVector;
    }
    IEnumerator ScaleCoroutineUp()
    {
        yield return new WaitForSeconds(ReverseDelay);
        // Store the original scale and pivot
        Vector3 originalScale = rectTransform.localScale;

        // Calculate the target scale
        Vector3 targetScaleVector = new Vector3(1, 1, 1);

        float elapsedTime = 0f;

        while (elapsedTime < ReverseDuration)
        {
            // Interpolate the scale
            rectTransform.localScale = Vector3.Lerp(originalScale, targetScaleVector, elapsedTime / ReverseDuration);

            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the final scale is set
        rectTransform.localScale = targetScaleVector;
    }
    IEnumerator Move1()
    {
        yield return new WaitForSeconds(Delay);
        float elapsedTime = 0f;

        while (elapsedTime < Duration)
        {
            // Interpolate the scale
            rectTransform.localPosition = Vector3.Lerp(InitialPosition, FinalPosition, elapsedTime / Duration);

            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the final scale is set
        rectTransform.localPosition = FinalPosition;
    }
    IEnumerator Move2()
    {
        yield return new WaitForSeconds(ReverseDelay);
        float elapsedTime = 0f;

        while (elapsedTime < ReverseDuration)
        {
            // Interpolate the scale
            rectTransform.localPosition = Vector3.Lerp(FinalPosition, InitialPosition, elapsedTime / ReverseDuration);

            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the final scale is set
        rectTransform.localPosition = InitialPosition;
    }
}
