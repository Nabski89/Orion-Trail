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
        Charisma = Random.Range(0, 4);
        Survival = Random.Range(0, 4);
        Engineering = Random.Range(0, 4);
        Kinesthetics = Random.Range(0, 4);
        Luck = Random.Range(0, 4);
        Bullets = Random.Range(0, 4);
    }

    // Update is called once per frame
    void Update()
    {
        HPUI.text = HP.ToString();
        MoraleUI.text = Morale.ToString();

        if (Morale < 1)
        {
            HP -= 1;
            Morale += Random.Range(0, 4);
        }

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
}
