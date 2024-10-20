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
        while (IncomingDamage > 0)
        {
            IncomingDamage -= 1;
            HP -= 1;
            DestroyHP();
        }
        if (HP < 1)
            Defeated();
    }
    void DestroyHP()
    {
        if (HPBar.GetChild(0) != null)
        {
            Debug.Log("Enemy number " + EnemyNumber + " was dealt damage which should remove a " + Controller.EnemyHPBar[EnemyNumber].GetChild(0).gameObject);
            Transform child = HPBar.GetChild(0);
            child.SetParent(null);
            Destroy(child.gameObject);
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
