using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombatController : MonoBehaviour
{
    public Transform CombatLog;
    public GameObject[] AttackArray;
    // Start is called before the first frame update
    void Start()
    {
        CombatLog = GetComponentInParent<GenericManager>().CombatLog;
    }
    public void Attack(int AttackNumber)
    {
        CharacterManager Char = GetComponentInParent<CharacterManager>();
        if (Char.AtkCooldown < 0.1f)
        {
            GenericManager Reference = GetComponentInParent<GenericManager>();
            Reference.MainTextReference.TEXTBOX += "<br>" + Char.CharName + Char.Attack[AttackNumber].AttackText;
            Char.AtkCooldown = 6 - Char.Kinesthetics;

            CharacterManager[] characterManagers = GetComponentInParent<CharacterShip>().GetComponentsInChildren<CharacterManager>();
            //this is currently how we hit the defeat screen TODO add more defeat options
            // Loop through each CharacterManager and subtract 1 from Morale
            foreach (CharacterManager characterManager in characterManagers)
            {
                characterManager.AtkCooldown += 1;
            }
            Instantiate(Char.Attack[AttackNumber].CharacterAtkGameObject, CombatLog);
        }
    }
}
