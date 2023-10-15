using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatController : MonoBehaviour
{
    private Move MoveScript;
    public CharacterManager[] Character;
    public int[] CharAtkType;
    public EnemyCombatScript Enemy;
    public Transform CombatOverlay;
    public DialogText TextBoxUI;
    public TextMeshProUGUI CrewName1;
    public TextMeshProUGUI CrewName2;
    public bool WindowEnable;
    //holds all the crewmembers, used to select who is in the fight
    public Transform Crew;
    void Start()
    {
        MoveScript = GetComponentInChildren<Move>();
        if (MoveScript == null)
        {
            Debug.LogWarning("Hey the move script is no longer in a child of the combat controller. Fix it or combat won't work.");
        }
        scaleChange = new Vector3(1f, 0, 0);
    }

    //Check our screensize, then if we have two attacks queue'd up AND have finished displaying the last attack, take the next attack.
    //After that check if combat is done
    void Update()
    {
        MinMaxScreen();
        if (Enemy != null)
        {
            if (CharAtkType[0] != 0 && CharAtkType[1] != 0 && TextBoxUI.displayedCharacters >= TextBoxUI.TEXTBOX.Length)
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
        {
            TextBoxUI.TEXTBOX += "<br>" + Enemy.transform.name + " been defeated";
            Destroy(Enemy.transform.gameObject);
            WindowEnable = false;
            MoveScript.DarkenEnemy();
            Debug.Log("Combat Ends");
        }
    }


    public void CombatAction()
    {
        //characters attack then the enemy goes. 
        if (Character[0] != null)
        {
            CharacterAttack(0);
            //         TextBoxUI.TEXTBOX += "<br>" + Character[0].CharName + " Attacks";
        }
        if (Character[1] != null)
        {
            CharacterAttack(1);
        }
        EnemyAttack();
    }
    //TODO make an array that holds attack types, randomrange the length of the array to select your attack type. 1 default attack, 1 feat/stat based attack, 1 weapon based attack, 1 do nothing attack
    //for REASONS we can't use the attack type directly in the array, we have to shift it by 1
    void CharacterAttack(int Char)
    {
        if (CharAtkType[Char] == 6)
        {
            int RandomAttack = Random.Range(1, 6);
            if (RandomAttack < 4)
            {
                Enemy.HP -= 1;
                TextBoxUI.TEXTBOX += "<br>" + Character[Char].CharName + Character[Char].Attack[CharAtkType[Char] - 1].AttackText;
            }
            if (RandomAttack == 4 || RandomAttack == 5)
            {
                TextBoxUI.TEXTBOX += "<br>Yeah those buttons don't do anything yet. Try again later";
            }
        }
        else
        {
            if (CharAtkType[Char] < 4)
            {
                Enemy.HP -= 1;
                TextBoxUI.TEXTBOX += "<br>" + Character[Char].CharName + Character[Char].Attack[CharAtkType[Char] - 1].AttackText;
            }
            if (CharAtkType[Char] == 4 || CharAtkType[Char] == 5)
            {
                TextBoxUI.TEXTBOX += "<br>Yeah those buttons don't do anything yet. Try again later";
            }
            CharAtkType[Char] = 0;
        }
    }
    void EnemyAttack()
    {
        //  int         Charisma = Random.Range(0, Enemy.attack.AttackText.length); 
        Character[0].HP -= 1;
        Character[1].HP -= 1;
        int randomIndex = Random.Range(0, Enemy.Attack.Length);
        TextBoxUI.TEXTBOX += "<br>The Enemy Attacks" + Enemy.transform.name + Enemy.Attack[randomIndex].AttackText;
        Debug.Log("Enemy.attack");

        //    Debug.Log("Enemy HP: " + Enemy.HP+ Enemy.attack.Length);
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
        EnemyHPBar.transform.localScale = new Vector3(Mathf.Max(0, Enemy.HP / Enemy.MaxHP), 1, 1);
    }
}
