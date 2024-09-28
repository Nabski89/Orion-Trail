using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttack : MonoBehaviour
{
    public int Rank;

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
        CrewLayout = GetComponentInParent<CombatController>().CrewLayout;
        if (Rank == 0)
        {
            DamageCharacter(0, 1);
            DamageCharacter(1, 1);
        }
        if (Rank == 1)
        {
            DamageCharacter(2, 1);
            DamageCharacter(3, 1);
        }
        if (Rank == 2)
        {
            DamageCharacter(4, 1);
            DamageCharacter(5, 1);
        }
    }

    //okay I'm going to get lost in the if else statements here.
    //take our rank, double it because we have 2 possible enemies per rank, then maybe add 1 to pick which one we randomly hit
    //if there is no one in that spot then we gotta figure out which spot we weren't in and check the other one
    //if there is still no one in that one then we go onto the next rank, trying it again
    //if we tried and failed all 3 ranks then we give up
    void DamageCharacter(int location, int DamageAmount)
    {
        int HitSpot = (2 * location + Random.Range(0, 2));

        if (CrewLayout.Location[HitSpot].CrewInLocation != null)
            CrewLayout.Location[HitSpot].CrewInLocation.HP -= DamageAmount;
        else
        {
            if (IsEven(HitSpot))
            {
                HitSpot += 1;
                if (CrewLayout.Location[HitSpot].CrewInLocation != null)
                    CrewLayout.Location[HitSpot].CrewInLocation.HP -= DamageAmount;
                else
                    MoveOnToNextRank(location, DamageAmount);
            }
            else
            {
                HitSpot -= 1;
                if (CrewLayout.Location[HitSpot].CrewInLocation != null)
                    CrewLayout.Location[HitSpot].CrewInLocation.HP -= DamageAmount;
                else
                    MoveOnToNextRank(location, DamageAmount);

            }
        }
        StartCoroutine(FlashColor(CrewLayout.Location[location].transform));
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
            location += 1;
            DamageCharacter(location, DamageAmount);
        }
        else
        {
            location = 1;
            DamageCharacter(location, DamageAmount);
        }
    }
}