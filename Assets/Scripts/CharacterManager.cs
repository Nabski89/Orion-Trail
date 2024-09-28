using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    //By default show 1/2, 1/3 on completed run, then 1/3/4, then 1/3/5 if you finished their quest.
    public string[] CharacterLore;
    public Sprite CharacterPicture;
    public string CharName;
    public int HP = 50;
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
    public GameObject Equipment;
    //attack info
    public float AtkCooldown;
    // Start is called before the first frame update
    void Start()
    {
        //get our name from the randomizer if we don't have a preset one
        if (CharName == null)
            CharName = GetComponentInChildren<CrewNameRandomizer>().Name;

        //randomize our stats
        Charisma = Random.Range(0, 6);
        Survival = Random.Range(0, 6);
        Engineering = Random.Range(0, 6);
        Kinesthetics = Random.Range(0, 6);
        Luck = Random.Range(0, 6);
        Bullets = Random.Range(0, 4);
        HP = Random.Range(4, 9) + Survival * 2 + Kinesthetics;
    }

    // Update is called once per frame
    void Update()
    {
        MoraleUI.text = Morale.ToString();
        HungerUI.text = Hunger.ToString();
    }
    public void Die()
    {
        CharacterShip Ship = transform.GetComponentInParent<CharacterShip>();
        int StatTotal = Charisma + Survival + Engineering + Kinesthetics + Luck;
        Ship.CrewMoraleChange(StatTotal, false);
        Destroy(gameObject);
    }
    public void BringUpStats()
    {
        CharacterShip Ship = transform.GetComponentInParent<CharacterShip>();
        Ship.StatUI.CharacterStatSelect(this);
    }

    public void Equip()
    {

    }


    [System.Serializable]
    public class Attacks
    {
        public GameObject CharacterAtkObject;
        public string AttackText;
    }
    public Attacks[] Attack;
    public Color[] CharacterColor;
}
