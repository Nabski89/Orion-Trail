using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int HP = 5;
    public int MaxHP = 5;
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
        //     public GameObject eventObject;
        public string AttackText;
        public int DamageMin;
        public int DamageMax;
    }

    public List<Attacks> attack = new List<Attacks>();

    public void WinCombat()
    {

    }
    public void LoseComabt()
    {

    }
}
