using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFramerate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
           Application.targetFrameRate = 30;
    }

}
