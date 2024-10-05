using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatScreen : MonoBehaviour
{
    public CharacterManager Character;
    public TextMeshProUGUI NameUI;
    public TextMeshProUGUI StatusUI;
    public TextMeshProUGUI SkillUI;
    StatScreenMover UIMover;
    void Start()
    {
        UIMover = GetComponent<StatScreenMover>();
    }
    public void CharacterStatSelect(CharacterManager CharacterToUI)
    {
        if (UIMover.MainScreen.sizeDelta.x < UIMover.MainScreenWidthGoal / 2)
        {
            UIMover.UnLockdown();
            Maximize(CharacterToUI);
        }
        else
        {
            UIMover.ChangeType();
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
        ClearSkills();
        CharSkills();
    }
    public void Minimize()
    {
        UIMover.Lockdown();
    }
    IEnumerator Hotswap(CharacterManager CharacterToUI)
    {
        //todo make this delay the sum of the delays in the statscreenmover
        yield return new WaitForSeconds(UIMover.MainScreenDuration / 2);
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
    void ClearSkills()
    {
        //destroy all children in the skill UI to clear it out for a new character
        while (SkillHolder.childCount > 0)
        {
            var child = SkillHolder.GetChild(0);
            child.SetParent(null);
            Destroy(child.gameObject);
        }
    }
    void CharSkills()
    {
        ActiveCheck = Character.GetComponentsInChildren<ITrait>();
        Debug.Log(ActiveCheck.Length);
        for (int i = 0; i < ActiveCheck.Length; i++)
        {
            GameObject SkillIcon = Instantiate(SkillUIItem, SkillHolder);
            if (ActiveCheck[i].TraitDescription != null)
                SkillIcon.GetComponent<MouseOverText>().infoText140max = ActiveCheck[i].TraitDescription;
            else
                Debug.LogWarning("You have a skill without a description");
            if (ActiveCheck[i].TraitIcon != null)
                SkillIcon.GetComponent<Image>().sprite = ActiveCheck[i].TraitIcon;
            else
                Debug.LogWarning("You have a skill without a icon");
        }
    }
}