using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    public DialogText DialogBox;
    public Transform[] StaticEvents;
    public float BreakDownTrack;
    public int EatTracker = 0;

    public Transform EventHolder;
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
            selectedEvent.CheckCrewResponses();
            //this used to set color layers but I would rather do it in the event itself
            /*
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
*/
            //set the textbox with a random text from the default types
            DialogBox.TEXTBOX = selectedEvent.EventText[Random.Range(0, selectedEvent.EventText.Length)];
            DialogBox.NewText();
        }
    }
    /* This used to make enemies dark on defeat
        public void DarkenEnemy()
        {
            MainScreen.color = new Color32(0, 0, 0, 255);
        }
    */
    public void SelectNonRandomEvent(GameObject NonRandomEvent)
    {
        Instantiate(NonRandomEvent, EventHolder);
        ActivateEvent();
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
