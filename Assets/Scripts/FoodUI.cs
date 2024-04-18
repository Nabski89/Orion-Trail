using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodUI : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private Supplies SupplyScript;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        SupplyScript = GetComponentInParent<Supplies>();
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = " " +SupplyScript.Food;
    }
}
