using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateButton : MonoBehaviour
{
    public float duration = 0.2f; // Duration of the rotation
    public GameObject soundEffectPrefab; // Prefab of the sound effect to instantiate

    public void PushedButton()
    {
        StartCoroutine(RotateOverTime());
    }

    private IEnumerator RotateOverTime()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, 0, 0);
        if (transform.rotation != endRotation)
            Instantiate(soundEffectPrefab, transform.position, Quaternion.identity);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation; // Ensure it finishes exactly at 0 degrees
        GetComponentInParent<FuelParent>().ReadyCheck();

    }
}
