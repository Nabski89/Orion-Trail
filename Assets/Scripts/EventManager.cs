using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public void PushButton()
    {
        Event selectedEvent = GetComponentInChildren<Event>();
        if (selectedEvent != null)
        {
            //check to make sure we aren't in a combat so we can't just clear out of it
            if (GetComponentInChildren<EnemyCombatScript>() == null)
                selectedEvent.TempEventDestro();
        }
    }
}
