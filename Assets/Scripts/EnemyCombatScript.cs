using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCombatScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int HP = 5;
    public int MaxHP = 5;
    public GameObject EmptyHP;
    public GameObject[] HPAmount;
    public Sprite UnitSprite;
    public EnemyUI enemyUI;
    void Start()
    {
        CombatController Controller = GetComponentInParent<CombatController>();
        Controller.InitiateCombat();
    }

    // Update is called once per frame
    void Update()
    {

    }
    //   public GameObject Enemy;

    [System.Serializable]
    public class Attacks
    {
        public GameObject eventObject;
        public string AttackText;
        public int DamageMin;
        public int DamageMax;
    }
    public Attacks[] Attack;
    public void GetAttacked()
    {
        Debug.LogWarning("Enemy Under Attack");
        HP -= 1;
        if (HP < 1)
        {
             enemyUI.gameObject.SetActive(false);
        }
    }
    public void WinCombat()
    {

    }
    public void LoseComabt()
    {

    }
}
