using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TriggerUI : MonoBehaviour
{
    public TriggerUI PreviousElement;
    public TriggerUI NextElement;
    private RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    public void Forward()
    {
        if (ChangeHeightWidth == true)
            StartCoroutine(ChangeHeightOrWidth());

    }
    void StartNextElement()
    {
        if (rectTransform.sizeDelta == new Vector2(BigWidth, BigHeight))
            if (NextElement != null)
                NextElement.Forward();
    }
    void StartPreviousElement()
    {
        if (rectTransform.sizeDelta == new Vector2(SmallWidth, SmallHeight))
            if (PreviousElement != null)
                PreviousElement.Backward();
    }
    public void Backward()
    {
        StartCoroutine(ChangeHeightOrWidthBack());
    }
    public bool ChangeHeightWidth = false;
//    bool ChangeHeightWidthComplete = false;
    public float SmallWidth = 0f;
    public float SmallHeight = 0f;
    public float BigWidth = 200f; // Set the desired width in the Inspector
    public float BigHeight = 200f;
    public float duration = 1f;

    IEnumerator ChangeHeightOrWidth()
    {
        float initialWidth = rectTransform.sizeDelta.x;
        float initialHeight = rectTransform.sizeDelta.y;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newWidth = Mathf.Lerp(initialWidth, BigWidth, elapsedTime / duration);
            float newHeight = Mathf.Lerp(initialHeight, BigHeight, elapsedTime / duration);
            rectTransform.sizeDelta = new Vector2(newWidth, newHeight);
            yield return null;
        }

        // Ensure the final width is set exactly to the target width
        rectTransform.sizeDelta = new Vector2(BigWidth, BigHeight);
        StartNextElement();
    }
    IEnumerator ChangeHeightOrWidthBack()
    {
        float initialWidth = rectTransform.sizeDelta.x;
        float initialHeight = rectTransform.sizeDelta.y;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newWidth = Mathf.Lerp(initialWidth, SmallWidth, elapsedTime / duration);
            float newHeight = Mathf.Lerp(initialHeight, SmallHeight, elapsedTime / duration);
            rectTransform.sizeDelta = new Vector2(newWidth, newHeight);
            yield return null;
        }

        // Ensure the final width is set exactly to the target width
        rectTransform.sizeDelta = new Vector2(SmallWidth, SmallHeight);
        StartPreviousElement();
    }
}
