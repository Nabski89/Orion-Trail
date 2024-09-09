using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineRoll : MonoBehaviour
{
    public RectTransform SlotWheel;
    public float rollSpeed = 1f;
    public float rollSpeedDefault = 500;
    public float FinishingTime = 0.25f;
    public float combinedHeight = 2;
    public float StopMultiplier = 3;
    public float StopConstant = 3;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StopRoulette());
        SlotWheel = GetComponent<RectTransform>();
        foreach (RectTransform child in transform)
        {
            combinedHeight += child.rect.height;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move the RectTransform up
        Vector2 newPosition = SlotWheel.anchoredPosition;
        newPosition.y += rollSpeed * Time.deltaTime;
        // Reset the Y position if it exceeds the combined height
        if (newPosition.y > 40)
        {
            newPosition.y -= 40;
            transform.GetChild(0).SetAsLastSibling();
        }
        SlotWheel.anchoredPosition = newPosition;
    }

    public void StartRouletteButton()
    {
        rollSpeed = rollSpeedDefault + Random.Range(-rollSpeedDefault / 3, rollSpeedDefault / 3);
    }
    public void StopRouletteButton(float Delay)
    {
        Invoke("Pain", Delay);
    }

    void Pain()
    {
        StartCoroutine(StopRoulette());
    }
    IEnumerator StopRoulette()
    {
        while (rollSpeed > 2)
        {
            rollSpeed -= (rollSpeed * (StopMultiplier * Time.deltaTime)) + StopConstant * Time.deltaTime;

            yield return null;
        }
        rollSpeed = 0;
        //get it to the nearest 40
        Vector3 targetPosition = SlotWheel.anchoredPosition;
        targetPosition.y = Mathf.Round(targetPosition.y / 40) * 40;

        while (Vector3.Distance(SlotWheel.anchoredPosition, targetPosition) > 0.01f)
        {
            SlotWheel.anchoredPosition = Vector3.MoveTowards(SlotWheel.anchoredPosition, targetPosition, FinishingTime * Time.deltaTime);
            yield return null;
        }

        SlotWheel.anchoredPosition = targetPosition;
    }
}
