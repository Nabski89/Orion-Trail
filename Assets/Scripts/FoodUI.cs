using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodUI : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public Supplies SupplyScript;
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
