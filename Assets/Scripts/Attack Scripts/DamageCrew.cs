using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCrew : MonoBehaviour
{
    public void Start()
    {
     //   Debug.LogWarning("we are trying to damage a character named WILL IT WORK?!");
        int CharReference = GetComponent<EnemyAttackStatic>().AttackSlot;
        CombatController Combat = GetComponentInParent<CombatController>();
      //  Debug.LogWarning("we are trying to damage a character named " + Combat + " " + CharReference);
        if (Combat != null)
            Combat.CrewList[CharReference].HP -= 1;
    }

}
