using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCombatScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int EnemyNumber;
    public int HP = 5;
    public int MaxHP = 5;
    public GameObject EmptyHP;
    public GameObject[] HPAmount;
    public Sprite UnitSprite;
    public EnemyUI enemyUI;
    public Transform HPBar;
    CombatController Controller;
    public CombatEnemyLocation Location;
    void Start()
    {
        Controller = GetComponentInParent<CombatController>();
    }
    [System.Serializable]
    public class Attacks
    {
        public GameObject eventObject;
        public string AttackText;
        public int DamageMin;
        public int DamageMax;
    }
    public Attacks[] Attack;
    public void GetAttacked(int IncomingDamage)
    {
        //     while (IncomingDamage > 0)
        //   {
        HP -= IncomingDamage;
        UpdateHP();
        // }
        if (HP < 1)
            Defeated();
    }
    //new HP update plan, clear out the entire thing every dang time and rebuild it
    void UpdateHP()
    {
        while (HPBar.childCount > 0)
        {
            var child = HPBar.GetChild(0);
            child.SetParent(null);
            Destroy(child.gameObject);
        }
        //make sure we have some HP before we spawn anything or we will be stuck at 1
        if (HP > 0)
        {
            for (int i = 0; i < HP; i++)
            {
                Instantiate(HPAmount[i], HPBar.transform);
            }
        }
    }
    void Defeated()
    {
        enemyUI.TextBoxUI.TEXTBOX += "<br>" + transform.name + " has been defeated.";
        Location.MoveOut();
        enemyUI.gameObject.SetActive(false);
        Controller.CheckEndCombat();
    }
}
