using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void PushButton()
    {
        Event selectedEvent = GetComponentInChildren<Event>();
        if (selectedEvent != null)
        {
            selectedEvent.TempEventDestro();
            //turn the event black because it's deadzo
            GetComponentInParent<Move>().MainScreen.color = new Color32(0, 0, 0, 255);
        }
    }
}
