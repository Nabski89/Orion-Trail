using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStartCover : MonoBehaviour
{
    public RectTransform uiElement;
    public CombatController Controller;
    public Vector3 InitialPosition;
    public Vector3 ClosedPosition;
    void Start()
    {
        Controller = GetComponentInParent<CombatController>();
        rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = InitialPosition;
        // InitialPosition = rectTransform.localPosition;
        //time for some GPT code
        StartCoroutine(MoveToPosition());
    }

    // Duration of the move
    public float duration = 1.0f;
    float delay = 0.25f;
    private RectTransform rectTransform;

    IEnumerator MoveToPosition()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            rectTransform.localPosition = Vector3.Lerp(InitialPosition, ClosedPosition, elapsedTime / duration);
            yield return null;
        }
        // Ensure the final position is set to the target position
        rectTransform.localPosition = ClosedPosition;
        yield return new WaitForSeconds(delay);
        StartCoroutine(MoveBack());

    }
    IEnumerator MoveBack()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                rectTransform.localPosition = Vector3.Lerp(ClosedPosition, InitialPosition, elapsedTime / duration);
                yield return null;
            }
            elapsedTime += Time.deltaTime;
            // Ensure the final position is set to the target position
            rectTransform.localPosition = InitialPosition;
        }
    }
}
