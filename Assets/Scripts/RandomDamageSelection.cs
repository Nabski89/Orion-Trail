using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDamageSelection : MonoBehaviour
{
    // Array of damage objects
    public GameObject[] damageObjects;

    // Number of damage objects to activate
    public int damageAmount = 1;

    void Start()
    {
        ActivateRandomDamageObjects();
    }

    void ActivateRandomDamageObjects()
    {
        // Check if there are enough damage objects to activate
        if (damageAmount > damageObjects.Length)
        {
            Debug.LogWarning("Not enough damage objects in the array to activate.");
            return;
        }

        // Create a list to store indices of inactive damage objects
        List<int> inactiveIndices = new List<int>();

        // Find the indices of inactive damage objects
        for (int i = 0; i < damageObjects.Length; i++)
        {
            if (!damageObjects[i].activeSelf)
            {
                inactiveIndices.Add(i);
            }
        }

        // Check if there are enough inactive damage objects
        if (inactiveIndices.Count < damageAmount)
        {
            Debug.LogWarning("Not enough inactive damage objects to activate.");
            return;
        }

        // Activate random damage objects
        for (int i = 0; i < damageAmount; i++)
        {
            // Get a random index from the list of inactive indices
            int randomIndex = Random.Range(0, inactiveIndices.Count);

            // Activate the damage object at the random index
            damageObjects[inactiveIndices[randomIndex]].SetActive(true);

            // Remove the activated index from the list
            inactiveIndices.RemoveAt(randomIndex);
        }
    }
}
