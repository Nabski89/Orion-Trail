using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventOrShipManager : MonoBehaviour
{
    public Transform Events;
    public Transform EventScreen;
    public Transform Loot;
    public Transform Ship;
    void Update()
    {
        if (Events.childCount > 0 || Loot.childCount > 0)
        {
            EventScreen.gameObject.SetActive(true);
            Ship.gameObject.SetActive(false);
        }
        else
        {
            EventScreen.gameObject.SetActive(false);
            Ship.gameObject.SetActive(true);
        }
    }
}
