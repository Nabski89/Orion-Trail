using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    public int PG13;
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
    private void Start()
    {
        PG13 = GetParentCount(transform);
    }
    public float ParallaxMod = 0.95f;
    void Update()
    {
        transform.position = (transform.parent.position * ParallaxMod) + Vector3.back * PG13 *-1;
    }
}
