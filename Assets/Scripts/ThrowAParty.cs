using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAParty : MonoBehaviour
{
    public GameObject MoraleChanger;
    public GameObject MoraleChangerTarget;
    public string[] EntertainMessage;
    public void ThrowParty()
    {
        GetComponentInParent<GenericManager>().MainTextReference.TEXTBOX += "<br>" + EntertainMessage[Random.Range(0, EntertainMessage.Length)];
        Instantiate(MoraleChanger, transform);
        Instantiate(MoraleChangerTarget, transform);

        Invoke("Pain", 1);
    }

    void Pain()
    {
        GetComponentInParent<EntertainMinigame>().EndSkill();
    }
}