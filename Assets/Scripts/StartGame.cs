using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneOpen : MonoBehaviour
{
    public string LoadThis;

    public void LoadHere()
    {
        // Check if the TOLOAD string is not empty or null
        if (!string.IsNullOrEmpty(LoadThis))
        {
            // Load the scene with the specified name
            SceneManager.LoadScene(LoadThis);
        }
        else
        {
            Debug.LogWarning("Scene name is empty or null. Please assign a valid scene name in the TOLOAD field.");
        }
    }
}