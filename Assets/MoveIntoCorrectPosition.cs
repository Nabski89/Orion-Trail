using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIntoCorrectPosition : MonoBehaviour
{
    public Vector2 targetPosition;

    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = targetPosition;
    }
}