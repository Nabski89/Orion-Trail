using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRandomizer : MonoBehaviour
{
    public Sprite[] spriteOptions; // Array of sprites to choose from
    SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    void Start()
    {
        // Get the SpriteRenderer component attached to this GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Check if the spriteOptions array is not empty
        if (spriteOptions.Length > 0)
        {
            // Pick a random index from the spriteOptions array
            int randomIndex = Random.Range(0, spriteOptions.Length);

            // Set the spriteRenderer's sprite to the randomly chosen sprite
            spriteRenderer.sprite = spriteOptions[randomIndex];
        }
        else
        {
            Debug.LogWarning("SpriteOptions array is empty. Please add sprites to the array on item" + transform.name);
        }
    }
}
