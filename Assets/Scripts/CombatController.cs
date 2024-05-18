using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatController : MonoBehaviour
{
    LootController LootManager;
    public Transform CombatLog;
    public RectTransform CombatUIBar;
    private Move MoveScript;
    public int[] CharAtkType;
    public EnemyCombatScript Enemy;
    public GameObject[] TransitionBlocker;
    public Transform TransitionBlockerParent;
    public DialogText TextBoxUI;
    //holds all the crewmembers, used to select who is in the fight
    public Transform Crew;
    public CharacterManager[] CrewList;
    void Start()
    {
        LootManager = GetComponent<LootController>();
        MoveScript = GetComponentInChildren<Move>();
        if (MoveScript == null)
        {
            Debug.LogWarning("Hey the move script is no longer in a child of the combat controller. Fix it or combat won't work.");
        }
    }

    //Check our screensize, then if we have two attacks queue'd up AND have finished displaying the last attack, take the next attack.
    //After that check if combat is done
    bool CombatStarted = false;
    void Update()
    {
        if (Enemy != null && CombatStarted == true)
        {
            UpdateCrewActionBar();
            UpdateEnemyActionBar();
            EndCombat();
        }
    }
    public void UpdateCrewActionBar()
    {
        for (int i = 0; i < CrewList.Length; i++)
        {
            CrewList[i].AtkCooldown = Mathf.Clamp(CrewList[i].AtkCooldown - Time.deltaTime, 0, 5);
            float xPos = Mathf.Clamp(CrewList[i].AtkCooldown * 80f - 230, -230f, 170f);
            CrewList[i].ActionBar.localPosition = Vector3.right * xPos;
        }
    }
    public Transform EnemyActionBar;
    public float EnemyAttackCooldown = 2;
    void UpdateEnemyActionBar()
    {
        EnemyActionBar.transform.localScale = new Vector3(Mathf.Min(EnemyActionBar.transform.localScale.x + Time.deltaTime / EnemyAttackCooldown, 1), 1, 1);
        if (EnemyActionBar.transform.localScale == Vector3.one)
        {
            EnemyActionBar.transform.localScale = new Vector3(0, 1, 1);
            EnemyAttack();
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
        CrewList = Crew.GetComponentsInChildren<CharacterManager>();
        for (int i = 0; i < CrewList.Length; i++)
        {
            CrewList[i].CharAtkOverlay.SetActive(true);
            CrewList[i].AtkCooldown = 7 - CrewList[i].Kinesthetics;
        }
        Debug.Log("Combat was started");
        StartCoroutine(SetUpScreen());
    }
    IEnumerator SetUpScreen()
    {
        Instantiate(TransitionBlocker[Random.Range(0, TransitionBlocker.Length)], TransitionBlockerParent);
        yield return new WaitForSeconds(1);
        MinMaxScreen(1);
        CleanUpEnemyHP();
        yield return new WaitForSeconds(1.25f);
        StartCoroutine(SetUpEnemyHP());
        yield return null;
    }
    void CleanUpEnemyHP()
    {
        while (EnemyHPBarEmpty.childCount > 0)
        {
            var child = EnemyHPBarEmpty.GetChild(0);
            child.parent = null;
            Destroy(child.gameObject);
        }
        while (EnemyHPBar.childCount > 0)
        {
            var child = EnemyHPBar.GetChild(0);
            child.parent = null;
            Destroy(child.gameObject);
        }
    }
    IEnumerator SetUpEnemyHP()
    {
        //then spawn new empty hp amounts AND filled
        for (int i = 0; i < Enemy.MaxHP; i++)
        {
            Instantiate(Enemy.EmptyHP, EnemyHPBarEmpty.transform);
            yield return new WaitForSeconds(0.05f);
        }
        for (int i = 0; i < Enemy.HPAmount.Length && i < Enemy.MaxHP; i++)
        {
            Instantiate(Enemy.HPAmount[i], EnemyHPBar.transform);
            yield return new WaitForSeconds(0.1f);
        }
        CombatStarted = true;
        yield return null;
    }
    void EndCombat()
    {
        if (EnemyHPBar.childCount == 0)
        {
            TextBoxUI.TEXTBOX += "<br>" + Enemy.transform.name + " been defeated";
            Destroy(Enemy.transform.gameObject);
            MinMaxScreen(0);
            //TODO make enemies black on defeat again
            //     MoveScript.DarkenEnemy();
            Debug.Log("Combat Ends");
            // Find the LootController script and loot
            for (int i = 0; i < CrewList.Length; i++)
            {
                CrewList[i].CharAtkOverlay.SetActive(false);
            }
            LootManager.LootScreen.gameObject.SetActive(true);
            LootManager.ActivateLooting();
            CombatStarted = false;
        }
    }
    void EnemyAttack()
    {
        //  int         Charisma = Random.Range(0, Enemy.attack.AttackText.length); 
        int randomIndex = Random.Range(0, Enemy.Attack.Length);
        //the position is minus the offset so that when it reaches 0 it will actually activate the thing HARDCODE ALERT, THE STARTING POSITION
        Instantiate(Enemy.Attack[randomIndex].eventObject, CombatUIBar.position, CombatUIBar.rotation, CombatUIBar);
        TextBoxUI.TEXTBOX += "<br>The" + Enemy.transform.name + " " + Enemy.Attack[randomIndex].AttackText + " for 1 damage";
        Debug.Log("Enemy.attack");

        //    Debug.Log("Enemy HP: " + Enemy.HP+ Enemy.attack.Length);
    }
    public Transform EnemyHPBarEmpty;
    public Transform EnemyHPBar;

    public void EnemyDamage(int DamageAmount)
    {
        Enemy.HP -= DamageAmount;
        int HPBars = EnemyHPBar.childCount;
        Destroy(EnemyHPBar.GetChild(HPBars - 1).gameObject);
        if (HPBars > 0)
            Destroy(EnemyHPBar.GetChild(HPBars - 1));
    }
    public Transform CombatOverlay;
    void MinMaxScreen(int Viewable)
    {
        CombatOverlay.localScale = Vector3.one * Viewable;
    }
}
