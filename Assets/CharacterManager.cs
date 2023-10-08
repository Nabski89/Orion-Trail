using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterManager : MonoBehaviour
{
    public string CharName;
    public int HP = 50;
    public TextMeshProUGUI HPUI;
    public int Morale = 50;
    public TextMeshProUGUI MoraleUI;
    public int Hunger = 0;
    public TextMeshProUGUI HungerUI;

    public int Charisma;
    public int Survival;
    public int Engineering;
    public int Kinesthetics;
    public int Luck;
    public int Bullets;
    // Start is called before the first frame update
    void Start()
    {
        //get our name from the randomizer
        CharName = GetComponentInChildren<CrewNameRandomizer>().Name;

        //randomize our stats
        Charisma = Random.Range(-1, 6);
        Survival = Random.Range(-1, 6);
        Engineering = Random.Range(-1, 6);
        Kinesthetics = Random.Range(-1, 6);
        Luck = Random.Range(-1, 6);
        Bullets = Random.Range(0, 4);
        HP = Random.Range(4, 9) + Survival * 4 + Kinesthetics;
    }

    // Update is called once per frame
    void Update()
    {
        HPUI.text = HP.ToString();
        MoraleUI.text = Morale.ToString();
        HungerUI.text = Hunger.ToString();

        if (HP < 1)
            Die();
    }
    public void Die()
    {
        CharacterShip Ship = transform.GetComponentInParent<CharacterShip>();
        int StatTotal = Charisma + Survival + Engineering + Kinesthetics + Luck;
        int SadOfLoss = 0;
        while (SadOfLoss < StatTotal)
        {
            SadOfLoss += 1;
            Ship.CrewSad();
        }
        Destroy(gameObject);
    }
    public void BringUpStats()
    {
        CharacterShip Ship = transform.GetComponentInParent<CharacterShip>();
        Ship.StatUI.Maximize(this);
    }





    [System.Serializable]
    public class Attacks
    {
        //     public GameObject eventObject;
        public string AttackText;
        public int DamageMin;
        public int DamageMax;
    }
    public Attacks[] Attack;
}
