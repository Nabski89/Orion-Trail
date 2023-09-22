using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Supplies SupplyScript;
    public GameObject CharacterHolder;
    // Update is called once per frame
    void Update()
    {

    }
    public void MoveShip()
    {
        SupplyScript.Fuel -= 5;
        SupplyScript.Food -= CharacterHolder.transform.childCount;
        TriggerEvent();
    }

    public void TriggerEvent()
    {
        // Example of how to select a random event
        GameObject selectedEvent = SelectRandomEvent();
        if (selectedEvent != null)
        {
            // Do something with the selected event
            Debug.Log("Selected event: " + selectedEvent.name);
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

    public GameObject SelectRandomEvent()
    {
        if (events.Count == 0)
        {
            Debug.LogWarning("No events defined.");
            return null;
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
            if (randomValue < currentWeight)
            {
                return eventData.eventObject;
            }
        }

        // This should not happen, but if it does, return the last event
        return events[events.Count - 1].eventObject;
    }
}
