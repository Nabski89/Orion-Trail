using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHolderTag : MonoBehaviour
{
    public string[] WelcomeText;
    GenericManager Reference;
    DialogText DialogBox;
    void Start()
    {
        DialogBox = GetComponentInParent<GenericManager>().MainTextReference;
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
            }
        }
    }
}
