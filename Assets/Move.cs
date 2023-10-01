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
    public GameObject CharacterHolder;

    private void Start()
    {
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
        Starscape.localPosition = Vector3.Lerp(StarInitialPosition, StarTargetPosition, elapsedTime / moveDuration);
        ShipScape.localPosition = Vector3.Lerp(ShipInitialPosition, ShipTargetPosition, elapsedTime / moveDuration);
        elapsedTime += Time.deltaTime;
    }
    //variables related to moving the starscape
    public float moveDistance = 10f;
    public float ShipmoveDistance = 10f;
    public float moveDuration = 1f;
    public RectTransform Starscape;
    private Vector3 StarInitialPosition;
    private Vector3 StarTargetPosition;
    private float elapsedTime = 0;
    public RectTransform ShipScape;
    private Vector3 ShipInitialPosition;
    private Vector3 ShipTargetPosition;
    public Transform EventHolder;
    public int TravelLocation;
    public void MoveShip()
    {

        Debug.Log("Current event amount is  " + EventHolder.childCount);
        if (Starscape.localPosition == StarTargetPosition && ShipScape.localPosition == ShipTargetPosition && EventHolder.childCount == 0)
        {
            SupplyScript.Food -= CharacterHolder.transform.childCount;
            if (SupplyScript.Fuel > 5)
            {
                SupplyScript.Fuel -= 5;
                //this increments so we can figure out if we are at a static event
                TravelLocation += 1;
                TriggerEvent();

                //Move The Stars
                StarInitialPosition = Starscape.localPosition;
                StarTargetPosition -= new Vector3(moveDistance, 0f, 0f);
                //Move The Ship
                ShipInitialPosition = ShipScape.localPosition;
                ShipTargetPosition += new Vector3(ShipmoveDistance, 0f, 0f);
            }
            else
                SelectFuelEvent();

            //Bookkeeping for the ship movement
            elapsedTime = 0;
        }
    }

    public void TriggerEvent()
    {
        // Example of how to select a random event
        /*        if (transform.GetChild(0).GetChild(TravelLocation).GetComponent<EventLocation>().eventObject != null)
                {
                    Debug.Log("Trigger a story event");
                    SelectNonRandomEvent();
                }
                else
                {
         */
        Debug.Log("Trigger a random event");
        SelectRandomEvent();
        //      }
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
        Instantiate(transform.GetChild(0).GetChild(TravelLocation).GetComponent<EventLocation>().eventObject, EventHolder);
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
    //select a random list from the big list of events, and instantiate it in our event holder
    public void SelectRandomEvent()
    {
        if (RandomEvents.Count == 0)
        {
            Debug.LogWarning("No events defined.");
        }

        int totalWeight = 0;

        // Calculate the total weight of all events
        foreach (EventData eventData in RandomEvents)
        {
            totalWeight += eventData.eventWeight;
        }

        int randomValue = Random.Range(0, totalWeight);
        int currentWeight = 0;

        // Select an event based on the random value
        foreach (EventData eventData in RandomEvents)
        {
            currentWeight += eventData.eventWeight;
            if (randomValue < currentWeight && randomValue > currentWeight - eventData.eventWeight)
                Instantiate(eventData.eventObject, EventHolder);
        }
    }
    //This is the same thing as the random events but for fuel instead
    public void SelectFuelEvent()
    {
        if (NoFuelEvents.Count == 0)
        {
            Debug.LogWarning("No events defined.");
        }
        int totalWeight = 0;
        // Calculate the total weight of all events
        foreach (EventData eventData in NoFuelEvents)
        {
            totalWeight += eventData.eventWeight;
        }
        int randomValue = Random.Range(0, totalWeight);
        int currentWeight = 0;
        // Select an event based on the random value
        foreach (EventData eventData in NoFuelEvents)
        {
            currentWeight += eventData.eventWeight;
            if (randomValue < currentWeight && randomValue > currentWeight - eventData.eventWeight)
                Instantiate(eventData.eventObject, EventHolder);
        }
    }
}
