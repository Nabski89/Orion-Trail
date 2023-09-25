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
    public void MoveShip()
    {
        Debug.Log("Current event amount is  " + EventHolder.childCount);
        if (Starscape.localPosition == StarTargetPosition && ShipScape.localPosition == ShipTargetPosition && EventHolder.childCount == 0)
        {
            SupplyScript.Fuel -= 5;
            SupplyScript.Food -= CharacterHolder.transform.childCount;
            TriggerEvent();

            //Move The Stars
            StarInitialPosition = Starscape.localPosition;
            StarTargetPosition -= new Vector3(moveDistance, 0f, 0f);

            //Move The Ship
            ShipInitialPosition = ShipScape.localPosition;
            ShipTargetPosition += new Vector3(ShipmoveDistance, 0f, 0f);

            //Bookkeeping
            elapsedTime = 0;
        }
    }

    public void TriggerEvent()
    {
        // Example of how to select a random event

        SelectRandomEvent();
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

    //GPT CODE
    //TIME TO LEARN ABOUT SOME STUFF

    [System.Serializable]
    public class EventData
    {
        public GameObject eventObject;
        public int eventWeight;
    }

    public List<EventData> events = new List<EventData>();

    public void SelectRandomEvent()
    {
        if (events.Count == 0)
        {
            Debug.LogWarning("No events defined.");
        }

        int totalWeight = 0;

        // Calculate the total weight of all events
        foreach (EventData eventData in events)
        {
            totalWeight += eventData.eventWeight;
        }

        int randomValue = Random.Range(0, totalWeight);
        int currentWeight = 0;

        // Select an event based on the random value
        foreach (EventData eventData in events)
        {
            currentWeight += eventData.eventWeight;
            if (randomValue < currentWeight && randomValue > currentWeight - eventData.eventWeight)
            {
                Instantiate(eventData.eventObject, EventHolder);
            }
        }
    }
}
