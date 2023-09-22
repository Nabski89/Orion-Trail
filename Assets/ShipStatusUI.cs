using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShipStatusUI : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    public string[] Status;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = Status[0];
    }
}
