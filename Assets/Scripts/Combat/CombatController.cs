using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CombatController : MonoBehaviour
{
    public Image CombatBackground;
    public Sprite[] CombatBackgrounds;
    public int BackgroundSelectionNumber;
    LootController LootManager;
    private EventManager EventManager;
    public int[] CharAtkType;
    public EnemyUI[] EnemyUI;
    public EnemyCombatScript[] Enemy;
    public DialogText TextBoxUI;
    //holds all the crewmembers, used to select who is in the fight
    public Transform Crew;
    public CharacterManager[] CrewList;
    public CombatLockdown[] CombatLockdowns;
    public CombatLocationsManager CrewLayout;
    void Start()
    {
        CrewLayout = GetComponentInChildren<CombatLocationsManager>(true);
        EnemyUI = GetComponentsInChildren<EnemyUI>();
        CombatLockdowns = GetComponentsInChildren<CombatLockdown>();
        LootManager = GetComponent<LootController>();
        EventManager = GetComponentInChildren<EventManager>();
        if (EventManager == null)
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
            //       EndCombat();
        }
    }
    public SlotMachineManager slotMachineManager;
    public void InitiateCombat()
    {
        if (CombatStarted == false)
        {
            CombatStarted = true;
            //set up the crew
            CrewList = Crew.GetComponentsInChildren<CharacterManager>();
            CrewLayout.ClearOutForNewCombat();
            for (int i = 0; i < CrewList.Length; i++)
            {
                CrewList[i].GetComponent<CharacterCombatController>().StartCombat();
                CrewLayout.DropInCrew(CrewList[i]);
            }

            slotMachineManager.ActivateSlots();
            Debug.Log("Combat was started");

            //set up the enemy
            Enemy = GetComponentsInChildren<EnemyCombatScript>();
            Debug.Log("enemy length is " + Enemy.Length);
            for (int i = 0; i < EnemyUI.Length; i++)
            {

                EnemyUI[i].gameObject.SetActive(false);
                if (Enemy.Length > i)
                {
                    CrewLayout.DropInEnemy(Enemy[i], i);
                    EnemyUI[i].gameObject.SetActive(true);
                    EnemyUI[i].PopulateAttacks(Enemy[i]);
                }
            }

            //close off other random UI elements
            foreach (CombatLockdown lockdown in CombatLockdowns)
            {
                lockdown.Lockdown();
            }
            StartCoroutine(SetUpScreen());
        }
    }
    IEnumerator SetUpScreen()
    {
        //set our background to either a preset condition or a randomized thing
        if (BackgroundSelectionNumber > -1)
        {
            CombatBackground.sprite = CombatBackgrounds[BackgroundSelectionNumber];
            BackgroundSelectionNumber = -1;
        }
        else
            CombatBackground.sprite = CombatBackgrounds[Random.Range(0, CombatBackgrounds.Length)];

        //do the combat lockdown stuff
        yield return new WaitForSeconds(1);
        MinMaxScreen(1);
        CleanUpEnemyHP();
        yield return new WaitForSeconds(1.25f);
        for (int i = 0; i < Enemy.Length; i++)
            StartCoroutine(SetUpEnemy(i));
        yield return null;
    }
    void CleanUpEnemyHP()
    {
        for (int i = 0; i < EnemyHPBarEmpty.Length; i++)
        {
            while (EnemyHPBarEmpty[i].childCount > 0)
            {
                var child = EnemyHPBarEmpty[i].GetChild(0);
                child.SetParent(null);
                Destroy(child.gameObject);
            }
            while (EnemyHPBar[i].childCount > 0)
            {
                var child = EnemyHPBar[i].GetChild(0);
                child.SetParent(null);
                Destroy(child.gameObject);
            }
        }
    }
    IEnumerator SetUpEnemy(int SetMeUp)
    {
        Enemy[SetMeUp].EnemyNumber = SetMeUp;
        //TODO WHY DIDN"T THIS DO IT!?
        Enemy[SetMeUp].HPBar = EnemyHPBar[SetMeUp];
        //then spawn new empty hp amounts AND filled
        for (int i = 0; i < Enemy[SetMeUp].MaxHP; i++)
        {
            Instantiate(Enemy[SetMeUp].EmptyHP, EnemyHPBarEmpty[SetMeUp].transform);
            yield return new WaitForSeconds(0.05f);
        }
        for (int i = 0; i < Enemy[SetMeUp].HPAmount.Length && i < Enemy[SetMeUp].MaxHP; i++)
        {
            Instantiate(Enemy[SetMeUp].HPAmount[i], EnemyHPBar[SetMeUp].transform);
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
    void EndCombat()
    {
        //  if (EnemyHPBar.childCount == 0)
        {
            TextBoxUI.TEXTBOX += "<br>" + Enemy[0].transform.name + " has been defeated.";
            Destroy(Enemy[0].transform.gameObject);
            MinMaxScreen(0);
            //TODO make enemies black on defeat again
            //     MoveScript.DarkenEnemy();
            Debug.Log("Combat Ends");
            // Find the LootController script and loot
            for (int i = 0; i < CrewList.Length; i++)
            {
                CrewList[i].GetComponent<CharacterCombatController>().EndCombat();
            }
            LootManager.ActivateLooting();
            CombatStarted = false;
            foreach (CombatLockdown lockdown in CombatLockdowns)
            {
                lockdown.UnLockdown();
            }
        }
    }
    public void EngageCombatRound(int Rank, int Bonus)
    {

        Debug.Log("Crew is Attacking Rank: " + Rank + " for " + Bonus);
        //the crew attacks
        CrewLayout.AttackEnemy(Rank, Bonus);
        //the enemies attack
        StartCoroutine(EnemyAttack());
    }
    IEnumerator EnemyAttack()
    {
        yield return new WaitForSeconds(.25f);
        // Debug.Log("How Many Enemy " + EnemyUI.Length);
        for (int i = 0; i < EnemyUI.Length; i++)
        {
            //      Debug.Log("Time for Enemy # " + i);
            if (EnemyUI[i].isActiveAndEnabled)
            {
                Enemy[i].enemyUI = EnemyHPBarEmpty[i].GetComponentInParent<EnemyUI>();
                EnemyUI[i].Attack();
                slotMachineManager.UpdateHP();
                yield return new WaitForSeconds(.25f);
            }
            yield return new WaitForSeconds(.50f);
        }
        yield return null;
    }
    public Transform[] EnemyHPBarEmpty;
    public Transform[] EnemyHPBar;
    public Transform CombatOverlay;
    void MinMaxScreen(int Viewable)
    {
        CombatOverlay.localScale = Vector3.one * Viewable;
    }
    public void CheckEndCombat()
    {
        for (int i = 0; i < Enemy.Length; i++)
        {
            if (Enemy[i].HP > 0)
                return;
        }
        EndCombat();
    }
}
