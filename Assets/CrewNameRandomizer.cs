using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CrewNameRandomizer : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public string[] textArray;
    // Start is called before the first frame update
    private void Start()
    {
        if (textMesh == null || textArray.Length == 0)
        {
            Debug.LogError("TextMeshPro or textArray not set.");
            return;
        }

        RandomizeTextValue();
    }

    public void RandomizeTextValue()
    {
        if (textArray.Length > 0)
        {
            int randomIndex = Random.Range(0, textArray.Length);
            textMesh.text = textArray[randomIndex];
        }
        else
        {
            Debug.LogWarning("Text array is empty.");
        }
    }
}
