using System.Collections;
using System.Collections.Generic;
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
    void Start()
    {
        Controller = GetComponentInParent<CombatController>();
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
    public void GetAttacked(int IncomingDamage)
    {
        Debug.LogWarning("Enemy Under Attack");
        if (IncomingDamage > 0)
        {
            IncomingDamage -= 1;
            HP -= 1;
            Invoke("DestroyHP", 0.1f * IncomingDamage);
            GetAttacked(IncomingDamage);
        }
        if (HP < 1)
        {
            enemyUI.gameObject.SetActive(false);
        }
    }
    void DestroyHP()
    {
        Debug.Log(Controller.EnemyHPBar[EnemyNumber].GetChild(0).gameObject);
        Destroy(HPBar.GetChild(0).gameObject);
    }
    public void WinCombat()
    {

    }
    public void LoseComabt()
    {

    }
}
