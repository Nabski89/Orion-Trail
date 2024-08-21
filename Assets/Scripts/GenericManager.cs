using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This lives on the canvas so it can be referenced by get component in parent to look for other stuff with ease
public class GenericManager : MonoBehaviour
{
    public CharacterShip ShipReference;
    public DialogText MainTextReference;
    public Transform CombatLog;
    public Golf GolfReference;
    public PopupUI MouseOver;
}
