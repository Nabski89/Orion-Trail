using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    public float DestroyDelay;
    void Start()
    {
        Destroy(transform.gameObject, DestroyDelay);
    }
}
