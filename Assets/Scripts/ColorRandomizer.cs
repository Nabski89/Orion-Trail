using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ColorRandomizer : MonoBehaviour
{
    public Color[] ColorOptions;
    // Start is called before the first frame update
    void Start()
    {
        if (ColorOptions.Length != 0)
            GetComponent<Image>().color = ColorOptions[Random.Range(0, ColorOptions.Length)];
    }
}
