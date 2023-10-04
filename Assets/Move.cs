using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    public Image MainScreen;
    public Image ColorLayer;
    public DialogText DialogBox;
    public Supplies SupplyScript;
    public CharacterShip CharacterHolder;
    public Transform[] StaticEvents;
    public int StaticEventNumber;
    public float BreakDownTrack;
    private void Start()
    {
        // Loop through the starscape children and add them to the StaticEvents array
        int childCount = Starscape.transform.childCount;
        StaticEvents = new Transform[childCount];
        for (int i = 0; i < childCount; i++)
        {
            StaticEvents[i] = Starscape.transform.GetChild(i);
        }

        SupplyScript = GetComponentInParent<Supplies>();
        //set the initial position of the stars and ship
        StarInitialPosition = Starscape.localPosition;
        StarTargetPosition = StarInitialPosition;

        ShipInitialPosition = ShipScape.localPosition;
        ShipTargetPosition = ShipInitialPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveSpeed != 0 && EventHolder.childCount == 0)
        {
            BreakDownTrack += Time.deltaTime;
            if (BreakDownTrack > 1)
            {
                BreakDownTrack = 0;
                if (Random.Range(0, 60) < 2)
                {
                    SelectRandomEvent(BreakEvents);
                }
            }
            ShipScape.transform.position += Vector3.right * Time.deltaTime * MoveSpeed / 10;
            Starscape.transform.position -= Vector3.right * Time.deltaTime * 3 / 10 * MoveSpeed;
            SupplyScript.Fuel -= Time.deltaTime / 10;
            if (SupplyScript.Fuel < 1)
            {
                SelectRandomEvent(NoFuelEvents);
            }
            CharacterManager SadCrewmate = null;
            CharacterManager[] characterManagers = CharacterHolder.GetComponentsInChildren<CharacterManager>();
            // Loop through each CharacterManager and subtract 1 from Morale
            foreach (CharacterManager characterManager in characterManagers)
            {
                if (characterManager.Morale < 1)
                    SadCrewmate = characterManager;
            }
            if (SadCrewmate != null)
            {
                SelectRandomEvent(MoraleEvents);
            }
            if (ShipScape.transform.position.x > StaticEvents[StaticEventNumber].position.x - 33)
            {
                SelectNonRandomEvent();
                MoveSpeed = 0;
            }
            ActivateEvent();
        }
    }
    //variables related to moving the starscape
    public float MoveSpeed = 10f;
    public RectTransform Starscape;
    private Vector3 StarInitialPosition;
    private Vector3 StarTargetPosition;
    public RectTransform ShipScape;
    private Vector3 ShipInitialPosition;
    private Vector3 ShipTargetPosition;
    public Transform EventHolder;
    public int TravelLocation;
    public void MoveShip()
    {
        if (MoveSpeed == 0 && EventHolder.childCount == 0)
        {
            MoveSpeed = 10f;

        }
    }

    public void TriggerEvent()
    {
        // Example of how to select a random event
        Debug.Log("Trigger a random event");
        SelectRandomEvent(RandomEvents);
    }
    public void ActivateEvent()
    {
        Event selectedEvent = EventHolder.GetComponentInChildren<Event>();
        if (selectedEvent != null)
        {
            // Do something with the selected event
            Debug.Log("Selected event: " + selectedEvent.name);

            Event EventActivate = selectedEvent.GetComponent<Event>();
            MainScreen.sprite = EventActivate.EventPicture;
            //reset the color because we are making events black after we kill them
            MainScreen.color = new Color32(255, 255, 255, 255);
            //if we have a second layer for color, activate it
            if (EventActivate.EventPictureLayer2 != null)
            {
                ColorLayer.color = new Color32(0, 0, 0, 255);
                ColorLayer.sprite = EventActivate.EventPictureLayer2;
                if (EventActivate.Layer2Color != null)
                    ColorLayer.color = EventActivate.Layer2Color[Random.Range(0, EventActivate.Layer2Color.Length)];
            }
            else
                ColorLayer.color = new Color32(255, 255, 225, 0);

            //set the textbox with a random text from the default types
            DialogBox.TEXTBOX = EventActivate.EventText[Random.Range(0, EventActivate.EventText.Length)];
            DialogBox.NewText();
        }
    }

    public void DarkenEnemy()
    {
        MainScreen.color = new Color32(0, 0, 0, 255);
    }

    public void SelectNonRandomEvent()
    {

        if (StaticEvents[StaticEventNumber].GetComponent<EventLocation>() != null)
        {
            Instantiate(StaticEvents[StaticEventNumber].GetComponent<EventLocation>().eventObject, EventHolder);
        }
        else
            TriggerEvent();
        StaticEventNumber += 1;
    }
    //GPT CODE
    //TIME TO LEARN ABOUT SOME STUFF
    [System.Serializable]
    public class EventData
    {
        public GameObject eventObject;
        public int eventWeight;
    }
    public List<EventData> RandomEvents = new List<EventData>();
    public List<EventData> NoFuelEvents = new List<EventData>();
    public List<EventData> BreakEvents = new List<EventData>();
    public List<EventData> MoraleEvents = new List<EventData>();
    //select a random list from the big list of events, and instantiate it in our event holder
    public void SelectRandomEvent(List<EventData> EventType)
    {
        if (EventType.Count == 0)
        {
            Debug.LogWarning("No events defined.");
        }

        int totalWeight = 0;

        // Calculate the total weight of all events
        foreach (EventData eventData in EventType)
        {
            totalWeight += eventData.eventWeight;
        }

        int randomValue = Random.Range(0, totalWeight);
        int currentWeight = 0;

        // Select an event based on the random value
        foreach (EventData eventData in EventType)
        {
            currentWeight += eventData.eventWeight;
            if (randomValue < currentWeight && randomValue > currentWeight - eventData.eventWeight)
                Instantiate(eventData.eventObject, EventHolder);
        }
    }
}
