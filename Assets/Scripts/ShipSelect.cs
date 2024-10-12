using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipSelect : MonoBehaviour
{
    public Transform ShipIcon;
    public Sprite Ship;

    public void PickShip(ShipSelectable ShipCarry)
    {
        Ship = ShipCarry.ShipPicture;
        ShipIcon.GetComponent<Image>().sprite = ShipCarry.ShipPicture;
    }
}
