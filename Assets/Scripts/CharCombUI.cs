using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharCombUI : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // Assign the TextMeshPro component in the Inspector
    public BadgeColor[] BadgeEdges;
    public BadgeFace BadgeFace;
    // Start is called before the first frame update
    void Awake()
    {
        //figure out what everything is
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        BadgeFace = GetComponentInChildren<BadgeFace>();
        BadgeEdges = GetComponentsInChildren<BadgeColor>();
    }
}
