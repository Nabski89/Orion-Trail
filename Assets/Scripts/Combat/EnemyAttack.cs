using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttack : MonoBehaviour
{
    public EnemyCombatScript CombatScript;
    public int Rank;
    public GameObject AttackEffect;
    public int DamageAmount;

    CombatLocationsManager CrewLayout;
    // Start is called before the first frame update
    void Start()
    {
        CrewLayout = GetComponentInParent<CombatController>().CrewLayout;
        GoAttacking();
    }
    void GoAttacking()
    {

        //create the special effect that will move from here to the targetted character



        float delay = 0;
        float delayMod = 0.2f;
        CrewLayout = GetComponentInParent<CombatController>().CrewLayout;
        if (Rank == 0)
        {
            StartCoroutine(DamageCharacterDelayed(delay, 0, 1));
            delay += delayMod;
            StartCoroutine(DamageCharacterDelayed(delay, 1, 1));
            delay += delayMod;
            StartCoroutine(DamageCharacterDelayed(delay, 2, 1));
            delay += delayMod;
        }
        if (Rank == 1)
        {
            StartCoroutine(DamageCharacterDelayed(delay, 3, 1));
            delay += delayMod;
            StartCoroutine(DamageCharacterDelayed(delay, 4, 1));
            delay += delayMod;
            StartCoroutine(DamageCharacterDelayed(delay, 5, 1));
            delay += delayMod;
        }
        if (Rank == 2)
        {
            StartCoroutine(DamageCharacterDelayed(delay, 6, 1));
            delay += delayMod;
            StartCoroutine(DamageCharacterDelayed(delay, 7, 1));
            delay += delayMod;
            StartCoroutine(DamageCharacterDelayed(delay, 8, 1));
            delay += delayMod;
        }
    }


    IEnumerator DamageCharacterDelayed(float Delay, int location, int DamageAmount)
    {
        // Wait for the specified delay 
        yield return new WaitForSeconds(Delay);
        //Invoke the DamageCharacter coroutine
        yield return StartCoroutine(DamageCharacter(location, DamageAmount));
    }
    //okay I'm going to get lost in the if else statements here.
    //if there is still no one in that one then we go onto the next rank, trying it again
    IEnumerator DamageCharacter(int location, int DamageAmount)
    {
        GameObject UIEffect = Instantiate(AttackEffect, CombatScript.Location.transform.position, Quaternion.identity);
        UIEffect.GetComponent<AttackUIEffect>().startPosition = CombatScript.Location.transform.position;


        UIEffect.GetComponent<AttackUIEffect>().endPosition = CrewLayout.Location[location].transform.position;

        Debug.Log("The Spot we are trying to hit is " + location);
        if (CrewLayout.Location[location].CrewInLocation != null)
            CrewLayout.Location[location].CrewInLocation.HP -= DamageAmount;
        StartCoroutine(FlashColor(CrewLayout.Location[location].transform));
        yield return null;
    }
    IEnumerator FlashColor(Transform Element)
    {
        Image SetMe = Element.GetComponent<Image>();
        SetMe.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        SetMe.color = new Color(1, .25f, .25f, 1);
        yield return new WaitForSeconds(0.05f);
        SetMe.color = new Color(1, .5f, .5f, 1);
        yield return new WaitForSeconds(0.05f);
        SetMe.color = Color.white;
    }
    //for readability purposes
    bool IsEven(int num)
    {
        return num % 2 == 0;
    }
    void MoveOnToNextRank(int location, int DamageAmount)
    {
        if (location < 2)
        {
            DamageCharacter(location + 1, DamageAmount);
        }
        else
        {
            DamageCharacter(0, DamageAmount);
        }
    }
}