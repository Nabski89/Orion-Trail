using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleChildren : MonoBehaviour
{
    // Start is called before the first frame update
    // This is copilot generated
    void Start()
    {
        // Get all child transforms
        List<Transform> children = new List<Transform>();
        foreach (Transform child in transform)
        {
            children.Add(child);
        }

        // Shuffle the list of children
        for (int i = 0; i < children.Count; i++)
        {
            Transform temp = children[i];
            int randomIndex = Random.Range(i, children.Count);
            children[i] = children[randomIndex];
            children[randomIndex] = temp;
        }

        // Reassign the shuffled children back to the parent
        for (int i = 0; i < children.Count; i++)
        {
            children[i].SetSiblingIndex(i);
        }
    }
}