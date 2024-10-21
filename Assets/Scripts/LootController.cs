using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootController : MonoBehaviour
{
    public GameObject[] LootToSpawn;
    public Transform LootScreen;
    public int LootAmount = 3;
    float initialDelay = 0.0f;
    // Start is called before the first frame update
    public void ActivateLooting()
    {
        LootScreen.gameObject.SetActive(true);
        //clear the list
        foreach (Transform child in LootScreen)
        {
            Destroy(child.gameObject);
        }
        //spawn new list
        initialDelay = 0;
        LootAmount = 3;
        while (LootAmount > 0)
        {
            Invoke("CreateLoot", initialDelay);
            LootAmount -= 1;
            // Increase the delay
            initialDelay += 0.25f;
        }
    }
    public void DisableLooting()
    {
        // Delete all children
        foreach (Transform child in LootScreen)
        {
            Destroy(child.gameObject);
        }
        // Disable the LootScreen
        LootScreen.gameObject.SetActive(false);
    }

    public void CreateLoot()
    {
        Instantiate(LootToSpawn[Random.Range(0, LootToSpawn.Length)], LootScreen);
    }
}
