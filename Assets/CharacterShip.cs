using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterShip : MonoBehaviour
{
    public StatScreen StatUI;
    GenericManager Reference;

    // Start is called before the first frame update
    void Start()
    {
        Reference = GetComponentInParent<GenericManager>();
        LoadCustomCrew();
        SetEngineUIBlack();
    }
    public void LoadCustomCrew()
    {
        Debug.Log("load the crew");
        // Find all instances of CustomCrew in the scene
        CustomCrew[] customCrews = FindObjectsOfType<CustomCrew>();

        // Loop through each CustomCrew instance
        foreach (CustomCrew CrewToLoad in customCrews)
        {
            Debug.Log("We found a crew");
            // Check if the current instance is attached to the GameObject this script is on
            if (CrewToLoad.gameObject != null)
            {
                Debug.Log("Remove the old crew");
                // Destroy existing children
                foreach (Transform child in transform)
                    Destroy(child.gameObject);

                // Instantiate copies of each game object in the Crew array
                InstantiateCrewMembers(CrewToLoad.Crew);
                Destroy(CrewToLoad.gameObject);
            }
        }
    }

    private void InstantiateCrewMembers(GameObject[] crew)
    {
        // Instantiate copies of each game object in the Crew array as children
        foreach (GameObject crewMemberPrefab in crew)
        {
            Instantiate(crewMemberPrefab, transform);
        }
    }



    public void CrewHunger()
    {
        CharacterManager[] characterManagers = GetComponentsInChildren<CharacterManager>();
        //this is currently how we hit the defeat screen TODO add more defeat options
        if (characterManagers.Length <= 2)
            GetComponentInParent<LoadNewScreen>().LoadScene();
        // Loop through each CharacterManager and subtract 1 from Morale
        foreach (CharacterManager characterManager in characterManagers)
        {
            characterManager.Hunger += 1;
        }
    }

    public void CrewMoraleChange(int HowSad, bool IsRandom)
    {
        if (HowSad != 0)
        {
            CharacterManager[] characterManagers = GetComponentsInChildren<CharacterManager>();
            if (IsRandom == false)
                foreach (CharacterManager characterManager in characterManagers)
                {
                    characterManager.Morale += HowSad;
                    Reference.MainTextReference.TEXTBOX += "<br>The entire crew is sad.";
                }
            else
            {
                int SadCharacter = Random.Range(0, characterManagers.Length);
                characterManagers[SadCharacter].Morale += HowSad;
                Reference.MainTextReference.TEXTBOX += "<br>" + characterManagers[SadCharacter].CharName + " is sad.";
            }
        }
    }

    //mechanics of the ship itself
    [System.Serializable]
    public class Engine
    {
        //     public GameObject eventObject;
        public string Name;
        public int Integrity;
        public Image EngineUI;
    }
    public Engine[] ShipPart;
    public Color[] EngineStatusColor;
    public void SetEngineUIBlack()
    {
        int EngieLoop = 0;
        while (EngieLoop < ShipPart.Length)
        {
            ShipPart[EngieLoop].EngineUI.color = EngineStatusColor[0];
            ShipPart[EngieLoop].Integrity = 4;
            EngieLoop += 1;
        }
    }
    public void ChangeEngineValue(int Part, int Amount)
    {
        ShipPart[Part].Integrity = Mathf.Clamp(ShipPart[Part].Integrity + Amount, 0, 5);
        if (ShipPart[Part].Integrity == 5)
            ShipPart[Part].EngineUI.color = EngineStatusColor[2];
        if (ShipPart[Part].Integrity < 3)
            ShipPart[Part].EngineUI.color = EngineStatusColor[1];

    }
}
