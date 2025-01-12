using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyUI : MonoBehaviour
{
    public EnemyCombatScript CombatScript;

    public ShuffleChildren AttackSelectorHolder;
    public Transform AttackHolder;
    public DialogText TextBoxUI;
    public Slider progressBar; // Reference to the UI Slider
    public float AbilityCooldown;
    public float AbilityCooldownRemaining;
    void Start()
    {
        AttackSelectorHolder = GetComponentInChildren<ShuffleChildren>();

        progressBar.maxValue = AbilityCooldown;
        progressBar.value = AbilityCooldown;
        AbilityCooldownRemaining = AbilityCooldown;
    }
    void Update()
    {
        UpdateAttackCooldown();
    }
    public void UpdateAttackCooldown()
    {
        if (AbilityCooldown != 0)
            progressBar.value = AbilityCooldownRemaining;
        //   canvasElement.anchoredPosition = startPosition;
    }
    public void PopulateAttacks(EnemyCombatScript Enemy)
    {
        //set our ability cooldown to a random thing so everyone doesn't go at once
        AbilityCooldown = Random.Range(0, 5);
        CombatScript = Enemy;
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
        // Cooldown is complete, you can trigger other actions here 
        // Reset cooldown for with a little wiggle
        progressBar.maxValue = AbilityCooldown;
        AbilityCooldownRemaining = AbilityCooldown + Random.Range(-1.1f, 1.1f);
        progressBar.value = AbilityCooldownRemaining;



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
                //Enemies should have 5 attacks. If you are seeing an error here it's probably because you don't have something in the fifth slot. I think
                Transform NewThing = null;
                if (AttackHolder.GetChild(i) != null)
                {
                    NewThing = Instantiate(AttackHolder.GetChild(i), transform);
                }
                else
                    Debug.LogWarning("Tried to attack but there was no attack in the slot");
                NewThing.GetComponent<EnemyAttack>().CombatScript = CombatScript;
                NewThing.GetComponent<EnemyAttack>().enabled = true;
                NewThing.GetComponent<AudioSource>().enabled = true;
                //      Debug.Log("AttackActive at index " + i + " is active.");
                TextBoxUI.TEXTBOX += "<br>COSMIC BLAST FROM " + transform.name;
                //Enemy[i].transform.name + " " + Enemy[i].Attack[randomIndex].AttackText + " for " + AttackAmount + " damage and blocks " + BlockAmount;
            }
        }
        //randomize the next attack
        AttackSelectorHolder.Shuffle();
    }
}
