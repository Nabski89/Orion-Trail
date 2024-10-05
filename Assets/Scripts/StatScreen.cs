using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatScreen : MonoBehaviour
{
    public Vector3 scaleChange;
    public CharacterManager Character;
    public TextMeshProUGUI NameUI;
    public TextMeshProUGUI StatusUI;
    public TextMeshProUGUI SkillUI;

    public void CharacterStatSelect(CharacterManager CharacterToUI)
    {
        if (transform.localScale.x < 0.5)
            Maximize(CharacterToUI);
        else
        {
            StartCoroutine(Hotswap(CharacterToUI));
        }
    }
    public void Maximize(CharacterManager CharacterToUI)
    {
        Character = CharacterToUI;

        NameUI.text = Character.CharName;
        StatusUI.text =
        // Character.HP.ToString()+
         "\n" + Character.Hunger.ToString()
        + "\n" + Character.Morale.ToString()
        + "\n" + Character.Bullets.ToString()
        ;

        SkillUI.text = Character.Charisma.ToString()
        + "\n" + Character.Survival.ToString()
        + "\n" + Character.Engineering.ToString()
        + "\n" + Character.Kinesthetics.ToString()
        + "\n" + Character.Luck.ToString()
        ;
        CharPicture();
        CharSkills();
        StartCoroutine(Maxi());
    }
    IEnumerator Maxi()
    {
        while (transform.localScale.y < 0.97f)
        {
            transform.localScale += scaleChange * Time.deltaTime;
            yield return null;
        }
        transform.localScale = Vector3.one;
    }
    public void Minimize()
    {
        StartCoroutine(Mini());
    }
    IEnumerator Mini()
    {
        while (transform.localScale.y > 0.01f)
        {
            transform.localScale -= scaleChange * Time.deltaTime;
            yield return null;
        }
        transform.localScale = Vector3.zero;

    }
    IEnumerator Hotswap(CharacterManager CharacterToUI)
    {
        while (transform.localScale.y > 0.01f)
        {
            transform.localScale -= scaleChange * Time.deltaTime;
            yield return null;
        }
        Maximize(CharacterToUI);
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
    public Image CrewPicture;
    void CharPicture()
    {
        CrewPicture.sprite = Character.CharacterPicture;
    }
    public ITrait[] ActiveCheck;
    public GameObject SkillUIItem;
    public Transform SkillHolder;
    void CharSkills()
    {
        ActiveCheck = Character.GetComponentsInChildren<ITrait>();
        Debug.Log(ActiveCheck.Length);
        for (int i = 0; i < ActiveCheck.Length; i++)
        {
            GameObject SkillIcon = Instantiate(SkillUIItem, SkillHolder);

            SkillIcon.GetComponent<MouseOverText>().infoText140max = ActiveCheck[i].TraitDescription;
            SkillIcon.GetComponent<Image>().sprite = ActiveCheck[i].TraitIcon;
        }

    }
}