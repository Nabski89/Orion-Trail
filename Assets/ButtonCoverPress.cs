using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCoverPress : MonoBehaviour
{
    public GameObject ClickSound;
    public RectTransform buttonRectTransform; // Assign the RectTransform of the button
    public Vector3 upPosition; // The up position
    public Vector3 downPosition; // The down position
    public float moveDuration = 0.5f; // Duration of the move

    private bool isUp = false; // Track the current position state

    public void PressButton()
    {
        StartCoroutine(MoveButton());
    }
    //this is gpt stuff, which is why the !isUp and target position nonsense exists, but it makes sense so whatever. I was originally doing it with offsets insted of set positions but set positions is fine too for now.
    IEnumerator MoveButton()
    {
        Vector3 startPosition = buttonRectTransform.localPosition;
        Vector3 targetPosition = isUp ? downPosition : upPosition;
        float elapsedTime = 0f;
        if (ClickSound != null)
        {
            GameObject ButtonCoverMove = Instantiate(ClickSound);
            Destroy(ButtonCoverMove, 0.5f);
        }
        while (elapsedTime < moveDuration)
        {
            if (elapsedTime < moveDuration / 2)
                buttonRectTransform.localPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            buttonRectTransform.localPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the button ends at the exact target position
        buttonRectTransform.localPosition = targetPosition;

        // Toggle the isUp state
        isUp = !isUp;
    }
}
