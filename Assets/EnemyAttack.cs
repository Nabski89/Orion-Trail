using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttack : MonoBehaviour
{
    public bool Rank1;
    public bool Rank2;
    public bool Rank3;

    public int DamageAmount;

    CombatLocationsManager CrewLayout;
    // Start is called before the first frame update
    void Start()
    {
        CrewLayout = GetComponentInParent<CombatController>().CrewLayout;
        if (Rank1 == true)
        {
            StartCoroutine(FlashColor(CrewLayout.Location[0].transform));
            DamageCharacter(0, 1);
            StartCoroutine(FlashColor(CrewLayout.Location[1].transform));
            DamageCharacter(1, 1);
        }
        if (Rank2 == true)
        {
            StartCoroutine(FlashColor(CrewLayout.Location[2].transform));
            DamageCharacter(2, 1);
            StartCoroutine(FlashColor(CrewLayout.Location[3].transform));
            DamageCharacter(3, 1);
        }
        if (Rank3 == true)
        {
            StartCoroutine(FlashColor(CrewLayout.Location[4].transform));
            DamageCharacter(4, 1);
            StartCoroutine(FlashColor(CrewLayout.Location[5].transform));
            DamageCharacter(5, 1);
        }
    }

    void DamageCharacter(int location, int DamageAmount)
    {
        if (CrewLayout.Location[location].CrewInLocation != null)
            CrewLayout.Location[location].CrewInLocation.HP -= DamageAmount;
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
}
