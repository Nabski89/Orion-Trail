using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatLockdown : MonoBehaviour
{
    public float duration = 1.0f;
    // Target scale factor
    public float targetScale = 0.9f;
    public float targetCoverSize = 70;
    public RectTransform ExtraCover;

    // Reference to the RectTransform
    private RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {

        rectTransform = GetComponent<RectTransform>();
    }

    public void Lockdown()
    {
        StartCoroutine(ScaleCoroutineDown());
    }
    public void UnLockdown()
    {
        StartCoroutine(CoverOpen());
    }

    IEnumerator ScaleCoroutineDown()
    {
        // Store the original scale and pivot
        Vector3 originalScale = rectTransform.localScale;

        // Calculate the target scale
        Vector3 targetScaleVector = new Vector3(targetScale, targetScale, 1);

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Interpolate the scale
            rectTransform.localScale = Vector3.Lerp(originalScale, targetScaleVector, elapsedTime / duration);

            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the final scale is set
        rectTransform.localScale = targetScaleVector;
        StartCoroutine(CoverClose());
    }

    IEnumerator CoverClose()
    {

        float YValue = ExtraCover.sizeDelta.y;

        float elapsedTime = 0f;
        float CoverSpeed = targetCoverSize / duration;

        while (elapsedTime < duration)
        {
            ExtraCover.sizeDelta = new Vector2(Mathf.Min(Time.deltaTime * CoverSpeed + ExtraCover.sizeDelta.x, targetCoverSize), YValue);
            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the final scale is set
        ExtraCover.sizeDelta = new Vector2(targetCoverSize, YValue);
    }
    IEnumerator CoverOpen()
    {

        float YValue = ExtraCover.sizeDelta.y;

        float elapsedTime = 0f;
        float CoverSpeed = targetCoverSize / duration;

        while (elapsedTime < duration)
        {
            ExtraCover.sizeDelta = new Vector2(Mathf.Min(-Time.deltaTime * CoverSpeed + ExtraCover.sizeDelta.x, 0), YValue);
            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the final scale is set
        ExtraCover.sizeDelta = new Vector2(0, YValue);
        StartCoroutine(ScaleCoroutineUp());
    }
    IEnumerator ScaleCoroutineUp()
    {
        // Store the original scale and pivot
        Vector3 originalScale = rectTransform.localScale;

        // Calculate the target scale
        Vector3 targetScaleVector = new Vector3(1, 1, 1);

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Interpolate the scale
            rectTransform.localScale = Vector3.Lerp(originalScale, targetScaleVector, elapsedTime / duration);

            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the final scale is set
        rectTransform.localScale = targetScaleVector;
    }
}
