using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParallaxUI : MonoBehaviour
{
    public int PG13;
    RectTransform ThisGuy;
    RectTransform ThisGuysDad;
    void Start()
    {
        ThisGuy = GetComponent<RectTransform>();
        ThisGuysDad = transform.parent.GetComponent<RectTransform>();
        PG13 = GetParentCount(transform);
    }
    public int GetParentCount(Transform targetTransform)
    {
        int parentCount = 0;
        Transform currentParent = targetTransform.parent;

        while (currentParent != null)
        {
            parentCount++;
            currentParent = currentParent.parent;
        }
        return parentCount;
    }

    public float ParallaxMod = 0.95f;
    void Update()
    {
        if (transform.parent != null)
        {
            float x = ThisGuysDad.localPosition.x * ParallaxMod;
            float y = ThisGuysDad.localPosition.y * ParallaxMod;
            ThisGuy.localPosition = new Vector2(x, y);
        }
    }
}
