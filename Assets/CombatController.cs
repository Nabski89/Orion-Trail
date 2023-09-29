using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatController : MonoBehaviour
{
    public CharacterManager[] Character;
    public int[] CharAtkType;
    public EnemyCombatScript Enemy;
    public Transform CombatOverlay;
    public TextMeshProUGUI TextBoxUI;
    public TextMeshProUGUI CrewName1;
    public TextMeshProUGUI CrewName2;
    public bool WindowEnable;
    //holds all the crewmembers, used to select who is in the fight
    public Transform Crew;
    void Start()
    {
        scaleChange = new Vector3(1f, 0, 0);
    }
    void Update()
    {
        MinMaxScreen();
        if (Enemy != null)
        {
            if (CharAtkType[0] != 0 && CharAtkType[1] != 0)
                CombatAction();
            UpdateHPBars();
            EndCombat();
        }
    }
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

        Character[0] = characterManagers[firstIndex];
        Character[1] = characterManagers[secondIndex];
        Debug.Log("Combat was started");
        SetUpScreen();
    }
    //pulled this out for clarity, TODO go back to edit once the layout is better
    void SetUpScreen()
    {
        WindowEnable = true;
        if (Character[0] != null)
            CrewName1.text = Character[0].CharName;
        if (Character[1] != null)
            CrewName2.text = Character[1].CharName;
    }
    void EndCombat()
    {
        if (Enemy.HP < 1)
            Destroy(Enemy.transform.gameObject);
    }


    public void CombatAction()
    {
        //characters attack then the enemy goes. 
        if (Character[0] != null)
            CharacterAttack(0);
        if (Character[1] != null)
            CharacterAttack(1);
        EnemyAttack();
    }
    void CharacterAttack(int Char)
    {
        Enemy.HP -= 1;
        //TODO make an array that holds attack types, randomrange the length of the array to select your attack type. 1 default attack, 1 feat/stat based attack, 1 weapon based attack, 1 do nothing attack
        //Character[Char];
        CharAtkType[Char] = 0;
    }
    void EnemyAttack()
    {
        Character[0].HP -= 1;
        Character[1].HP -= 1;
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
    public Transform EnemyHPBar;
    void UpdateHPBars()
    {
        EnemyHPBar.transform.localScale = Vector3.one - Vector3.right * ((Enemy.MaxHP - Enemy.HP) / Enemy.MaxHP);
    }
}
