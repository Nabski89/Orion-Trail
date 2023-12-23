using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCrewmember : MonoBehaviour
{
    public int CharacterNumber;
    public bool CrewFilled = false;
    public void Press()
    {
        GetComponentInParent<CrewSelect>().ClearCrew(CharacterNumber);
        CrewFilled = false;
    }
}
