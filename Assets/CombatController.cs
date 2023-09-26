using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatController : MonoBehaviour
{
    public CharacterManager Character1;
    public CharacterManager Character2;
    public EnemyCombatScript Enemy;
    public Transform CombatOverlay;
    public TextMeshProUGUI TextBoxUI;
    public TextMeshProUGUI CrewName1;
    public TextMeshProUGUI CrewName2;
    public bool WindowEnable;

    void Start()
    {
        scaleChange = new Vector3(1f, 0, 0);
    }
    public Transform Crew;
    public void InitiateCombat()
    {
        //GPT debug logging
        if (Crew == null)
        {
            Debug.LogWarning("Crew Transform is not assigned.");
            return;
        }
        if (Enemy == null)
        {
            Debug.LogWarning("EnemyCombat script is not assigned.");
            return;
        }

        CharacterManager[] characterManagers = Crew.GetComponentsInChildren<CharacterManager>();

        if (characterManagers.Length < 2)
        {
            Debug.LogWarning("There are not enough CharacterManagers in the Crew.");
            return;
        }

        // Randomly select two different CharacterManager scripts
        int firstIndex = Random.Range(0, characterManagers.Length);
        int secondIndex = Random.Range(0, characterManagers.Length);

        while (secondIndex == firstIndex)
        {
            secondIndex = Random.Range(0, characterManagers.Length);
        }

        Character1 = characterManagers[firstIndex];
        Character2 = characterManagers[secondIndex];
        SetUpScreen();
    }
    //pulled this out for clarity, TODO go back to edit once the layout is better
    void SetUpScreen()
    {
        WindowEnable = true;
        if (Character1 != null)
            CrewName1.text = Character1.CharName;
        if (Character2 != null)
            CrewName2.text = Character2.CharName;
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

    void Update()
    {
        MinMaxScreen();
    }


    private Vector3 scaleChange;
    void MinMaxScreen()
    {

        if (WindowEnable == true)
        {
            if (CombatOverlay.localScale.x < 0.97f)
                CombatOverlay.localScale += scaleChange * Time.deltaTime;
            else
                transform.localScale = Vector3.one;
        }
        //     sphere.transform.position += positionChange;
        else
        {
            if (CombatOverlay.localScale.x > 0.01f)
                CombatOverlay.localScale -= scaleChange * Time.deltaTime;
            else
                CombatOverlay.localScale = new Vector3(0, 1, 1);
        }
    }
}
