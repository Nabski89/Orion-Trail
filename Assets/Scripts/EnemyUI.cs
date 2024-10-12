using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyUI : MonoBehaviour
{
    public ShuffleChildren AttackSelectorHolder;
    public Transform AttackHolder;
    public DialogText TextBoxUI;
    void Start()
    {
        AttackSelectorHolder = GetComponentInChildren<ShuffleChildren>();
    }
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
            child.SetParent(null);
            Destroy(child.gameObject);
        }
    }
    public int Atk;
    public int Block;
    public int Buff;

    public void Attack()
    {
        Atk = 0;
        Block = 0;
        //reset that array so it doesn't just say the default ones
        AttackSelectorHolder.ReturnWhoIsActive();
        //Buff doesn't get cleared each time welcome to scaling mother fucker
        // Loop through each AttackActive component in the array
        for (int i = 0; i < AttackSelectorHolder.ActiveCheck.Length; i++)
        {
        //    Debug.Log("Time to check if " + i + " is active.");
            // Check if the Active property is set to true
            if (AttackSelectorHolder.ActiveCheck[i].Active == true)
            {
               Transform NewThing = Instantiate(AttackHolder.GetChild(i), transform);
               NewThing.GetComponent<EnemyAttack>().enabled = true;
               NewThing.GetComponent<AudioSource>().enabled = true;
          //      Debug.Log("AttackActive at index " + i + " is active.");
                TextBoxUI.TEXTBOX += "<br>COSMIC BLAST FROM "+ transform.name;
                //Enemy[i].transform.name + " " + Enemy[i].Attack[randomIndex].AttackText + " for " + AttackAmount + " damage and blocks " + BlockAmount;

            }
        }
        //randomize the next attack
        AttackSelectorHolder.Shuffle();
    }
}
