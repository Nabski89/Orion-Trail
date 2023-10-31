using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoraleCrew : MonoBehaviour
{
    //Use this to change the morale of the crew. Either a random crewmember for the amount stated OR every crewmember depending on if the boolean is checked
    public int MoraleChange;
    public bool IsRandom;
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInParent<GenericManager>().ShipReference.CrewMoraleChange(MoraleChange, IsRandom);
    }
}
