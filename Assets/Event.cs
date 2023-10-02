using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event : MonoBehaviour
{
    public Sprite EventPicture;
    public Sprite EventPictureLayer2;
    public Color[] Layer2Color;
    public GameObject Subevent1;
    public GameObject Subevent2;
    [TextArea]
    public string[] EventText;
    public string[] Options;


    public void TempEventDestro()
    {
        TriggerSubEvent1();
        Destroy(gameObject);
    }
    public void TriggerSubEvent1()
    {
        //if the sub event exists, then unparent it from this,
        if (Subevent1 != null)
        {
            Subevent1.transform.parent = transform.parent;
            Subevent1.SetActive(true);
            Destroy(gameObject);
        }
    }
}
