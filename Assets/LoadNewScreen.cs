using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewScreen : MonoBehaviour
{

    public string TOLOAD;
    public bool Automatic = false;

    void Start()
    {
        if (Automatic == true)
            LoadScene();
    }
    public void LoadScene()
    {
        // Check if the TOLOAD string is not empty or null
        if (!string.IsNullOrEmpty(TOLOAD))
        {
            // Load the scene with the specified name
            SceneManager.LoadScene(TOLOAD);
        }
        else
        {
            Debug.LogWarning("Scene name is empty or null. Please assign a valid scene name in the TOLOAD field.");
        }
    }
}
