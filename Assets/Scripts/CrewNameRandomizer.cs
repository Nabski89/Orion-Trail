using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CrewNameRandomizer : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public string[] textArray;
    public string Name;
    // Start is called before the first frame update
    public void RandomizeTextValue()
    {
        if (textArray.Length > 0)
        {
            int randomIndex = Random.Range(0, textArray.Length);
            textMesh.text = textArray[randomIndex];
            SetName();
        }
        else
        {
            Debug.LogWarning("Text array is empty.");
        }
    }
    public void SetName()
    {
        textMesh.text = Name;
    }
}
