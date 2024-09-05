using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatButton : MonoBehaviour
{
    public int ActionCall;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void ButtonPressed()
    {
        GetComponentInParent<CharacterCombatController>().AttackButForReal(ActionCall);
    }
}
