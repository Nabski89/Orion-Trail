using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventActionButton : MonoBehaviour
{
    EventManager EventManager;
    public int TraitResponseValue;
    void Start()
    {
        EventManager = GetComponentInParent<EventManager>();
    }
    public void PushedButton()
    {
        EventManager.CharacterResponse(TraitResponseValue);
    }
}
