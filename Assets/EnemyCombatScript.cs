using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //this is to check how many times we have triggered this event
    public int ClownNumber;
    public GameObject Enemy;

    [System.Serializable]
    public class Attacks
    {
        public GameObject eventObject;
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
