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

    public void PopulateAttacks(EnemyCombatScript Enemy)
    {
        //set our ability cooldown to a random thing so everyone doesn't go at once
        AbilityCooldown = Random.Range(1, 5);
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

    public void UpdateAttackCooldown()
    {
        if (AbilityCooldown != 0)
            progressBar.value = AbilityCooldownRemaining;
        //   canvasElement.anchoredPosition = startPosition;
    }
    public void Attack()
    {
        // Cooldown is complete, you can trigger other actions here 
        Atk = 0;
        Block = 0;
        //This resets what we read out of the attack array in case anything has changed.
        AttackSelectorHolder.ReturnWhoIsActive();
        //Buff doesn't get cleared each time welcome to scaling mother fucker
        // Loop through each AttackActive component in the array


        //this is used to put a small delay between our attacks
        float queuedattacks = 0;
        for (int i = 0; i < AttackSelectorHolder.ActiveCheck.Length; i++)
        {
            //    Debug.Log("Time to check if " + i + " is active.");
            // Check if the Active property is set to true
            if (AttackSelectorHolder.ActiveCheck[i].Active == true)
            {
                queuedattacks += 0.5f;
                //Enemies should have 5 attacks. If you are seeing an error here it's probably because you don't have something in the fifth slot. I think

                if (AttackHolder.GetChild(i) != null)
                {
                    StartCoroutine(AttackWithCooldownsIncluded(queuedattacks, i));
                }
                else
                    Debug.LogWarning("Tried to attack but there was no attack in the slot");

                Debug.Log("AttackActive at index " + i + " is active on enemy" + transform.name);

                //Enemy[i].transform.name + " " + Enemy[i].Attack[randomIndex].AttackText + " for " + AttackAmount + " damage and blocks " + BlockAmount;
            }
        }
        //randomize the next attack, this ONLY changes what is active
        AttackSelectorHolder.Shuffle();
        //use that same back of logic to see what our new cooldown is going to be
        //if we have more than one ability going to be activated the entire thing is multiplied by 0.75 then the additional cooldown is added
        for (int i = 0; i < AttackSelectorHolder.ActiveCheck.Length; i++)
        {
            if (AttackSelectorHolder.ActiveCheck[i].Active == true)
            {
                if (AttackHolder.GetChild(i) != null)
                {
                    AbilityCooldown = AbilityCooldown * .75f + AttackHolder.GetChild(i).GetComponent<SlotValue>().Cooldown;
                }
                else
                    Debug.LogWarning("Tried to set our cooldown but there was no attack in the slot");
            }
        }
        progressBar.maxValue = 5;
        AbilityCooldownRemaining = AbilityCooldown;
        UpdateAttackCooldown();
    }
    IEnumerator AttackWithCooldownsIncluded(float delay, int value)
    {
        yield return new WaitForSeconds(delay);
        Transform NewThing = null;
        NewThing = Instantiate(AttackHolder.GetChild(value), transform);
        NewThing.GetComponent<EnemyAttack>().CombatScript = CombatScript;
        NewThing.GetComponent<EnemyAttack>().enabled = true;
        NewThing.GetComponent<AudioSource>().enabled = true;
        TextBoxUI.TEXTBOX += "<br>COSMIC BLAST FROM in the form of " + NewThing.name + "from the enemy" + transform.name;
        yield return null;
    }
}
