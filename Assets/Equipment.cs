using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public GameObject Attack;
    public int AttackNumber;
    void Start()
    {
        GetComponentInParent<CharacterManager>().Attack[AttackNumber].CharacterAtkGameObject = Attack;
    }
    public void ButtonEquip()
    {
        GetComponentInParent<StatScreen>().Equip(transform.gameObject);
    }
}
