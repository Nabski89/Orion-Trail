using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShuffleChildren : MonoBehaviour
{
    public AttackActive[] ActiveCheck;
    // Start is called before the first frame update
    // This is copilot generated
    void Start()
    {
        ActiveCheck = GetComponentsInChildren<AttackActive>();
        Shuffle();
    }
    public void Shuffle()
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

    public void ReturnWhoIsActive()
    {
        ActiveCheck = GetComponentsInChildren<AttackActive>();
        /* This was sanity checking
        // Loop through each AttackActive component in the array
        for (int i = 0; i < ActiveCheck.Length; i++)
        {
            // Check if the Active property is set to true
            if (ActiveCheck[i].Active)
            {
                Debug.Log("AttackActive at index " + i + " is active.");
            }
        }
        */
    }
}