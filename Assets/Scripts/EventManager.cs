using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public DialogText DialogBox;
    public Transform EventHolder;
    public Transform Events;
    public Transform EventScreen;
    public Transform Loot;
    public Transform Ship;
    public bool EventActive;
    CharacterBulkManager CharacterHolder;
    public EventActionButton[] ActionButtonArray;
    public Event TheEvent;
    CrewSkillManager SkillManager;

    void Start()
    {
        CharacterHolder = GetComponentInChildren<CharacterBulkManager>();
        ActionButtonArray = CharacterHolder.GetComponentsInChildren<EventActionButton>(true);
        SkillManager = GetComponentInChildren<CrewSkillManager>();
        ChangeEventButtonStatus(false);
    }
    public void TriggerEvent()
    {
        // Example of how to select a random event
        Debug.Log("Trigger a random event");
        SelectRandomEvent(RandomEvents);
    }
    public void SelectNonRandomEvent(GameObject NonRandomEvent)
    {
        Instantiate(NonRandomEvent, EventHolder);
        StartEvent();
    }
    void StartEvent()
    {
        //disable our skill buttons
        SkillManager.DisableSkillButtons();

        Event selectedEvent = EventHolder.GetComponentInChildren<Event>();
        if (selectedEvent != null)
        {
            TheEvent = selectedEvent;
            EventScreen.gameObject.SetActive(true);
            Ship.gameObject.SetActive(false);
            EventDialog();
            SetCrewResponse();
        }
        else
            Debug.LogWarning("Tried to trigger and event but you fucked up and we didn't have one");
    }
    void EndEvent()
    {
        //Clear Our Current Event
        if (EventScreen.childCount > 0)
            Destroy(EventScreen.GetChild(0).gameObject);
        else
            Debug.LogWarning("EventScreen not found or has no children.");
        //  if (Events.childCount < 1 && Loot.childCount < 1)

        //enable our skill buttons
        SkillManager.EnableSkillButtons();
        ChangeEventButtonStatus(false);
        EventScreen.gameObject.SetActive(false);
        Ship.gameObject.SetActive(true);

        //  else
        //     NextEvent();
    }
    void NextEvent()
    {

    }
    void ChangeEventButtonStatus(bool ActivateEvent)
    {
        foreach (EventActionButton ActionButton in ActionButtonArray)
            ActionButton.transform.parent.gameObject.SetActive(ActivateEvent);
    }
    public void SetCrewResponse()
    {
        Debug.Log("Selected event: " + TheEvent.name);
        //lets enable all our event reaction buttons
        ChangeEventButtonStatus(true);
        CharacterBulkManager CharacterHolder = GetComponentInChildren<CharacterBulkManager>();
        foreach (EventActionButton ActionButton in CharacterHolder.GetComponentsInChildren<EventActionButton>(true))
        {
            ActionButton.transform.parent.gameObject.SetActive(true);
        }
        //and now we go down to the event to check what the crew can do
        TheEvent.CheckCrewResponses();
    }
    void EventDialog()
    {
        DialogBox.TEXTBOX = TheEvent.EventText[Random.Range(0, TheEvent.EventText.Length)];
        DialogBox.NewText();
    }

    public void CharacterResponse(int ResponseNumber)
    {
        //wait shit we can't check the array we need to loop to find which one has the correct response number
        //use something related to the response dialog. The -1 is because it's in an array?
        for (int i = 0; i < TheEvent.PossibleResponses.Length; i++)
        {
            if (TheEvent.PossibleResponses[i].TraitInt == ResponseNumber)
                ResponseNumber = i;
        }
        DialogBox.TEXTBOX = TheEvent.PossibleResponses[ResponseNumber].Dialog;
        DialogBox.NewText();
        ChangeEventButtonStatus(false);
        Invoke("EndEvent", 1);
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
