using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShip : MonoBehaviour
{
    public StatScreen StatUI;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CrewHunger()
    {
        CharacterManager[] characterManagers = GetComponentsInChildren<CharacterManager>();
        // Loop through each CharacterManager and subtract 1 from Morale
        foreach (CharacterManager characterManager in characterManagers)
        {
            characterManager.Hunger += 1;
        }
    }
    public void CrewSad()
    {
        CharacterManager[] characterManagers = GetComponentsInChildren<CharacterManager>();
        // Loop through each CharacterManager and subtract 1 from Morale
        foreach (CharacterManager characterManager in characterManagers)
        {
            characterManager.Morale -= 1;
        }
    }
}
