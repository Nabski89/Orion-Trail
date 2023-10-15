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
            //turn the event black because it's deadzo
            if (selectedEvent.Subevent1 == null)
            {
                //why am I doing this again?
                GetComponentInParent<Image>().color = new Color32(0, 0, 0, 255);
            }
            //check to make sure we aren't in a combat so we can't just clear out of it
            if (GetComponentInChildren<EnemyCombatScript>() == null)
                selectedEvent.TempEventDestro();
        }
    }
}
