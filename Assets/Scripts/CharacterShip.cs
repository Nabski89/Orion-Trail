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
        DamageEngine(3);
        DamageEngine(2);
        DamageEngine(1);
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

    public void CrewMoraleChange(int MoraleChange, bool IsRandom)
    {
        if (MoraleChange != 0)
        {
            CharacterManager[] characterManagers = GetComponentsInChildren<CharacterManager>();
            if (IsRandom == false)
                foreach (CharacterManager characterManager in characterManagers)
                {
                    characterManager.Morale += MoraleChange;
                    if (MoraleChange < 0)
                        Reference.MainTextReference.TEXTBOX += "<br>The entire crew is sad.";

                    else
                        Reference.MainTextReference.TEXTBOX += "<br>Everyone Liked that";
                }
            else
            {
                int SadCharacter = Random.Range(0, characterManagers.Length);
                characterManagers[SadCharacter].Morale += MoraleChange;
                if (MoraleChange < 0)
                    Reference.MainTextReference.TEXTBOX += "<br>" + characterManagers[SadCharacter].CharName + " feels worse.";
                else
                    Reference.MainTextReference.TEXTBOX += "<br>" + characterManagers[SadCharacter].CharName + " feels better.";

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
    public void DamageEngine(int DamageAmount)
    {
        int PartToDamage = Random.Range(0, ShipPart.Length);
        int i = 0;
        while (ShipPart[PartToDamage].Integrity == 0 && i < 5)
        {
            PartToDamage = Random.Range(0, ShipPart.Length);
            i++;
        }
        //TODO make it cause an accident if it tries 4 times to damage something and can't
        ShipPart[PartToDamage].Integrity = Mathf.Max(0, ShipPart[PartToDamage].Integrity - DamageAmount);
        if (i == 5)
        {
            Debug.Log("Everything is probably damaged already, maybe");
        }
        SetEngineUI();
    }
    public void RepairEngine()
    {
        /**
        int lowestIntegrity = integrityArray[0];
        int lowestIndex = 0;

        for (int i = 1; i < integrityArray.Length; i++)
        {
            if (integrityArray[i] < lowestIntegrity)
            {
                lowestIntegrity = integrityArray[i];
                lowestIndex = i;
            }
        }

        Debug.Log("Item with the lowest integrity: " + lowestIntegrity + " at index " + lowestIndex);
        **/
    }
    public void SetEngineUI()
    {
        int EngieLoop = 0;
        while (EngieLoop < ShipPart.Length)
        {
            ShipPart[EngieLoop].EngineUI.color = EngineStatusColor[ShipPart[EngieLoop].Integrity];
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
