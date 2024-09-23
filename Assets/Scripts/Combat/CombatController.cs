using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatController : MonoBehaviour
{
    LootController LootManager;
    public RectTransform CombatUIBar;
    private Move MoveScript;
    public int[] CharAtkType;
    public EnemyUI[] EnemyUI;
    public EnemyCombatScript Enemy;
    public DialogText TextBoxUI;
    //holds all the crewmembers, used to select who is in the fight
    public Transform Crew;
    public CharacterManager[] CrewList;

    public CombatLockdown[] CombatLockdowns;
    void Start()
    {
        EnemyUI = GetComponentsInChildren<EnemyUI>();
        CombatLockdowns = GetComponentsInChildren<CombatLockdown>();
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
            EndCombat();
        }
    }
    public SlotMachineManager slotMachineManager;
    public void InitiateCombat()
    {
        //GPT debug logging, doing some crew UI
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
        CombatLocationsManager CrewLayout = GetComponentInChildren<CombatLocationsManager>();
        CrewLayout.MoveOutCrew();
        CrewList = Crew.GetComponentsInChildren<CharacterManager>();
        for (int i = 0; i < CrewList.Length; i++)
        {
            CrewList[i].GetComponent<CharacterCombatController>().StartCombat();
            CrewLayout.DropInCrew(CrewList[i]);
        }

        slotMachineManager.ActivateSlots();
        Debug.Log("Combat was started");
        //set up the enemy UI
        for (int i = 0; i < EnemyUI.Length; i++)
        {
            EnemyUI[i].PopulateAttacks(Enemy);
        }

        //close off other random UI elements
        foreach (CombatLockdown lockdown in CombatLockdowns)
        {
            lockdown.Lockdown();
        }

        StartCoroutine(SetUpScreen());
    }
    IEnumerator SetUpScreen()
    {
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
                CrewList[i].GetComponent<CharacterCombatController>().EndCombat();
            }
            LootManager.LootScreen.gameObject.SetActive(true);
            LootManager.ActivateLooting();
            CombatStarted = false;
            foreach (CombatLockdown lockdown in CombatLockdowns)
            {
                lockdown.UnLockdown();
            }
        }
    }
    public void EngageCombatRound()
    {
        EnemyAttack();
    }
    void EnemyAttack()
    {
        for (int i = 0; i < EnemyUI.Length; i++)
        {
            int BlockAmount = 0;
            int AttackAmount = 0;
            if (EnemyUI[i].isActiveAndEnabled)
            {
                EnemyUI[i].Attack();
                BlockAmount += EnemyUI[i].Block;
                AttackAmount += EnemyUI[i].Atk;
            }
            int randomIndex = Random.Range(0, Enemy.Attack.Length);
            Instantiate(Enemy.Attack[randomIndex].eventObject, CombatUIBar.position, CombatUIBar.rotation, CombatUIBar);
            TextBoxUI.TEXTBOX += "<br>The" + Enemy.transform.name + " " + Enemy.Attack[randomIndex].AttackText + " for " + AttackAmount + " damage and blocks " + BlockAmount;
            Debug.Log("Enemy.attack");
        }
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
