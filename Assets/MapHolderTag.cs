using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHolderTag : MonoBehaviour
{
    public string[] WelcomeText;
    //    GenericManager Reference;
    DialogText DialogBox;
    Compass Compass;
    public GameObject[] SpecialHazard;
    public GameObject[] SpecialPlanet;
    public GameObject[] EliteEnemies;
    public GameObject[] BossEnemies;
    public PlanetTag[] TaggedPlanets;
    void Start()
    {
        DialogBox = GetComponentInParent<GenericManager>().MainTextReference;
        Compass = GetComponentInParent<GenericManager>().CompassReference;
        TaggedPlanets = GetComponentsInChildren<PlanetTag>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        ShipController TheShip = other.GetComponent<ShipController>();
        if (TheShip != null)
        {
            if (WelcomeText[0] != null)
            {
                DialogBox.TEXTBOX = WelcomeText[Random.Range(0, WelcomeText.Length)];
                DialogBox.NewText();
                //populate our compass map
                Compass.InitializeMap(this);
            }
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        ShipController TheShip = other.GetComponent<ShipController>();
        if (TheShip != null)
        {
            //send an update of our planet location to the compass
            Compass.UpdateMap(this);
        }
    }
}