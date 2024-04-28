using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float HP = 5;
    public float MaxHP = 5;
    void Start()
    {

        CombatController Controller = GetComponentInParent<CombatController>();
        Controller.Enemy = this;
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

    public void WinCombat()
    {

    }
    public void LoseComabt()
    {

    }
}
