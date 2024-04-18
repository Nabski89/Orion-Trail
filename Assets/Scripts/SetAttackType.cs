using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAttackType : MonoBehaviour
{
public int AttackNum;
public int CharControledNumber;
public CombatController CombatControl;

public void SetAttackNumber()
{
    CombatControl.CharAtkType[CharControledNumber] = AttackNum;
}
}
