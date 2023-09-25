using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
public class ScrollTextBox : MonoBehaviour
{
    public int ScrollAmount;
    public GameObject TextBox;
    public void MoveTextBox()
    {
        TextBox.transform.position += ScrollAmount*Vector3.up;
    }
}
