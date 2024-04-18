using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainLoot : MonoBehaviour
{
    public GameObject LootToSpawn;
    public void SpawnIt()
    {
        if (LootToSpawn != null)
        {
            // Instantiate the loot object
            GameObject spawnedLoot = Instantiate(LootToSpawn, transform);

            // Find the LootController script in the parent object
            LootController lootController = GetComponentInParent<LootController>();

            // Check if LootController script is found
            if (lootController != null)
            {
                // Call the DisableLooting function in LootController
                lootController.DisableLooting();
            }
            else
            {
                Debug.LogWarning("LootController script not found in parent.");
            }
        }
        else
        {
            Debug.LogWarning("Loot object to spawn is not assigned.");
        }
    }
}