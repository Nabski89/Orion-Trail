using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonSpawnSomething : MonoBehaviour
{
    public GameObject ThingToSpawn;
    // This is intended to be used on buttons to spawn sound effects
    void SpawnThis()
    {
        if (ThingToSpawn != null)
            Instantiate(ThingToSpawn);
        else
            Debug.LogWarning("You tried to spawn spawn something that didn't have anything attached!");
    }
}
