using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatController : MonoBehaviour
{
    public CharacterManager Character1;
    public CharacterManager Character2;
    public TextMeshProUGUI TextBoxUI;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void CombatAction()
    {
        //characters attack then the enemy goes.
        if (Character1 != null)
            CharacterAttack(Character1);
        if (Character2 != null)
            CharacterAttack(Character2);
        EnemyAttack();

    }

    void CharacterAttack(CharacterManager Char)
    {
        //TODO make an array that holds attack types, randomrange the length of the array to select your attack type. 1 default attack, 1 feat/stat based attack, 1 weapon based attack, 1 do nothing attack
        Char.Kinesthetics += 1;
    }
    void EnemyAttack()
    {
        Character1.HP -= 1;
    }
}
