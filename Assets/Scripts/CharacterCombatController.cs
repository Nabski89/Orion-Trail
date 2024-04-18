using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombatController : MonoBehaviour
{
    public Transform CombatLog;
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
        /*
                if (Character[AttackNumber].HP > 0)
                {
                    if (CharAtkType[AttackNumber] == 6)
                    {
                        int RandomAttack = Random.Range(1, 6);
                        if (RandomAttack < 4)
                        {
                            Enemy.HP -= 1;
                            TextBoxUI.TEXTBOX += "<br>" + Character[AttackNumber].CharName + Character[AttackNumber].Attack[CharAtkType[AttackNumber] - 1].AttackText;
                        }
                        if (CharAtkType[AttackNumber] == 4)
                        {
                            TextBoxUI.TEXTBOX += "<br>" + Character[AttackNumber].CharName + "Retreats back to the ship.";
                            Escape(Char);
                        }
                        if (RandomAttack == 5)
                        {
                            TextBoxUI.TEXTBOX += "<br>Yeah those buttons don't do anything yet. Try again later";
                        }
                    }
                    else
                    {
                        if (CharAtkType[AttackNumber] < 4)
                        {
                            Enemy.HP -= 1;
                            Instantiate(Character[AttackNumber].Attack[CharAtkType[AttackNumber] - 1].CharacterAtkGameObject, CombatLog);

                            TextBoxUI.TEXTBOX += "<br>" + Character[AttackNumber].CharName + Character[AttackNumber].Attack[CharAtkType[AttackNumber] - 1].AttackText;
                        }
                        if (CharAtkType[AttackNumber] == 4)
                        {
                            TextBoxUI.TEXTBOX += "<br>" + Character[AttackNumber].CharName + " Retreats back to the ship.";
                            Escape(Char);
                        }
                        if (CharAtkType[AttackNumber] == 5)
                        {
                            TextBoxUI.TEXTBOX += "<br>Yeah those buttons don't do anything yet. Try again later";
                        }
                        CharAtkType[AttackNumber] = 0;
                    }
                }
                */
    }
}
