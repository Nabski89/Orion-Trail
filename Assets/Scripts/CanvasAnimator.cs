using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAnimator : MonoBehaviour
{
    public Sprite[] sprites; // Array of sprites to cycle through
    public float animationTimeDelay = 0.5f; // Time delay between sprite changes
    private Image targetImage; // Reference to the Image component
    public bool Looping = true;
    void Start()
    {
        // Get the Image component attached to this GameObject
        targetImage = GetComponent<Image>();

        // Start the coroutine to cycle through sprites
        if (Looping == true)
            StartCoroutine(CycleSprites());
        else
            StartCoroutine(CycleOnce());
    }

    IEnumerator CycleSprites()
    {
        int currentSpriteIndex = 0;

        while (true)
        {
            // Update the image with the current sprite
            targetImage.sprite = sprites[currentSpriteIndex];

            // Wait for the specified time delay
            yield return new WaitForSeconds(animationTimeDelay);

            // Move to the next sprite, looping back to the start if necessary
            currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Length;
        }
    }
    IEnumerator CycleOnce()
    {
        int currentSpriteIndex = 0;

        while (currentSpriteIndex+1 < sprites.Length)
        {
            // Update the image with the current sprite
            targetImage.sprite = sprites[currentSpriteIndex];

            // Wait for the specified time delay
            yield return new WaitForSeconds(animationTimeDelay);

            // Move to the next sprite, looping back to the start if necessary
            currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Length;
        }
    }
}