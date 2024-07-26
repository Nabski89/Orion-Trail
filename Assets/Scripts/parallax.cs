using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    public int PG13;
    void Start()
    {
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
        float x = transform.parent.position.x * ParallaxMod;
        float y = transform.parent.position.y * ParallaxMod;
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
