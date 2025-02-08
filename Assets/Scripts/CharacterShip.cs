using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterShip : MonoBehaviour
{
    public StatScreen StatUI;
    GenericManager Reference;
    Supplies SupplyReference;

    // Start is called before the first frame update
    void Start()
    {
        Reference = GetComponentInParent<GenericManager>();
        SupplyReference = Reference.GetComponent<Supplies>();
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
        if (customCrews.Length > 0)
        {
            Debug.Log("We found a custom crew");
            // Destroy existing children
            foreach (Transform child in transform)
                Destroy(child.gameObject);
            // Instantiate copies of each game object in the Crew array
            foreach (GameObject crewMemberPrefab in customCrews[0].Crew)
                Instantiate(crewMemberPrefab, transform);
            Destroy(customCrews[0]);
        }
        else
            Debug.Log("We did not find a custom crew");
        Invoke("LoadTheCrewSkills", 0.1f);
        Debug.Log("Ready the new crews skills");

    }

    void LoadTheCrewSkills()
    {
        GetComponent<CrewSkillManager>().GetNewSkills();
        GetComponentInParent<EventManager>().SetUpCharacters();
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
    //type 1 for mechanical, 2 for electrical, 3 for advanced, 4 for any
    public void RepairEngine(int Type, int RepairValue)
    {

        //set it to the last part type then set it to the bonus part if we don't have any
        int PartType = 3;
        if (Type < 6)
            PartType = 2;
        if (Type < 4)
            PartType = 1;
        //switch case to select what types of parts we are repairing
        switch (PartType)
        {
            case 1:
                if (SupplyReference.MechanicalPart < 1)
                    SupplyReference.MechanicalPart -= 1;
                break;
            case 2:
                if (SupplyReference.ElectricalPart > 0)
                    SupplyReference.ElectricalPart -= 1;
                break;
            case 3:
                if (SupplyReference.TechPart > 0)
                    SupplyReference.TechPart -= 1;
                break;
            default:
                Debug.Log("We encountered and error while trying to repair the engine");
                break;
        }
        //actually repair the parts, Warning this caps out at a repair value of 5
        ShipPart[Type].Integrity = Mathf.Min(ShipPart[Type].Integrity + RepairValue, 5);
        //reset the UI
        SetEngineUI();
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
