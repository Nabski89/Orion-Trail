using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;





public class TextToRandomNumber : MonoBehaviour
{
    public TextMeshProUGUI textMesh;

    private void Start()
    {
        textMesh.text = Random.Range(0, 100) + "0";
    }
}
