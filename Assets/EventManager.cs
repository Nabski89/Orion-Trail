using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // Update is called once per frame
    public void PushButton()
    {
        Event selectedEvent = GetComponentInChildren<Event>();
        if (selectedEvent != null)
        {
            //turn the event black because it's deadzo
            if (selectedEvent.Subevent1 == null)
            {
                GetComponentInParent<Move>().MainScreen.color = new Color32(0, 0, 0, 255);
            }
            selectedEvent.TempEventDestro();
        }
    }
}
