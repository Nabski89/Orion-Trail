using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    public Transform AttackSelectorHolder;
    public Transform AttackHolder;

    public void PopulateAttacks(EnemyCombatScript Enemy)
    {
        //spawn all our attacks
        int index = 0;
        while (index < Enemy.Attack.Length)
        {
            Instantiate(Enemy.Attack[index].eventObject, AttackHolder);
            index++;
        }
        /* TODO SAFETY TEST THIS
        // If Enemy.Attack[] has less than 5 elements, repeat the process
        while (AttackHolder.childCount < 5)
        {
            index = 0;
            while (index < Enemy.Attack.Length)
            {
                Instantiate(Enemy.Attack[index].eventObject, AttackHolder);
                index++;
            }
        }
        */
        // Step 2: Remove random children of AttackHolder until it only has 5 children
        while (AttackHolder.childCount > 5)
        {
            int randomIndex = Random.Range(0, AttackHolder.childCount);

            var child = AttackHolder.GetChild(randomIndex);
            child.parent = null;
            Destroy(child.gameObject);
        }

    }
}
