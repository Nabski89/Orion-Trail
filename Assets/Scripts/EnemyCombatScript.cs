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
        Controller.InitiateCombat();
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
        Debug.LogWarning("Incoming Damage " + IncomingDamage);
        while (IncomingDamage > 0)
        {
            IncomingDamage -= 1;
            Debug.LogWarning("Damage Dealt");
            HP -= 1;
            DestroyHP();
        }
        if (HP < 1)
        {
            Defeated();
        }
    }
    void DestroyHP()
    {
        Debug.LogWarning("Trying to destroy an HP");
        Debug.Log(Controller.EnemyHPBar[EnemyNumber].GetChild(0).gameObject);
        Transform child = HPBar.GetChild(0);
        child.SetParent(null);
        Destroy(child.gameObject);
    }
    void Defeated()
    {
        Location.MoveOut();
        enemyUI.gameObject.SetActive(false);
        Controller.CheckEndCombat();
    }
}
