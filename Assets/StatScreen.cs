using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatScreen : MonoBehaviour
{
    private Vector3 scaleChange;
    public bool WindowEnable;
    public CharacterManager Character;


    // Start is called before the first frame update
    void Start()
    {
        scaleChange = new Vector3(1f, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {

        if (WindowEnable == true && Character != null)
        {
            if (transform.localScale.y < 0.97f)
                transform.localScale += scaleChange * Time.deltaTime;
            else
                transform.localScale = Vector3.one;
        }
        //     sphere.transform.position += positionChange;
        else
        {
            if (transform.localScale.y > 0.01f)
                transform.localScale -= scaleChange * Time.deltaTime;
            else
                transform.localScale = Vector3.zero;
        }
    }
    public TextMeshProUGUI NameUI;
    public TextMeshProUGUI StatusUI;
    public TextMeshProUGUI SkillUI;
    public void Maximize(CharacterManager CharacterToUI)
    {
        Character = CharacterToUI;
        WindowEnable = true;

        NameUI.text = Character.CharName;
        StatusUI.text = Character.HP.ToString()
        + "\n" + Character.Hunger.ToString()
        + "\n" + Character.Morale.ToString()
        + "\n" + Character.Bullets.ToString()
        ;

        SkillUI.text = Character.Charisma.ToString()
        + "\n" + Character.Survival.ToString()
        + "\n" + Character.Engineering.ToString()
        + "\n" + Character.Kinesthetics.ToString()
        + "\n" + Character.Luck.ToString()
        ;

    }
    public void Minimize()
    {
        WindowEnable = false;
    }
    public Transform EquipmentManager;
    public void Equip(GameObject ToEquip)
    {
        //check if we already have an equipment
        if (Character.Equipment != null)
            Instantiate(Character.Equipment, EquipmentManager);
        //create a new copy of our equipment, and destroy the old one
        Character.Equipment = Instantiate(ToEquip, Character.transform);
        Destroy(ToEquip);

    }
}