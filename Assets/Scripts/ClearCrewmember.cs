using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCrewmember : MonoBehaviour
{
    public InitialCrewButtons CrewManager;
    public int CharacterNumber;
    public int CrewClearCheckNumber;
    public bool CrewFilled = false;
    public void Press()
    {
        GetComponentInParent<CrewSelect>().ClearCrew(CharacterNumber);
        CrewFilled = false;
        CrewManager.CrewClearCheck[CrewClearCheckNumber] = false;
    }
}
