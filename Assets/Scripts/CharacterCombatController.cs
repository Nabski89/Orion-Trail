using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCombatController : MonoBehaviour
{
    public Transform CombatLog;
    public GameObject[] AttackArray;
    // Start is called before the first frame update
    void Start()
    {
        CombatLog = GetComponentInParent<GenericManager>().CombatLog;
    }
    public RectTransform ReadyUpButton;
    public RectTransform ClearOutButton;
    void Update()
    {
        if (InCombat == true)
        {
            float ReadyPosition = Mathf.Min(ReadyUpButton.localPosition.x + Time.deltaTime * 12, 230);
            ReadyUpButton.localPosition = new Vector3(ReadyPosition, 0, 0);

            float ClearPosition = Mathf.Min(ClearOutButton.localPosition.x + Time.deltaTime * 3, 230);
            ClearOutButton.localPosition = new Vector3(ClearPosition, 0, 0);
        }
    }
    public bool InCombat = false;
    public void StartCombat()
    {
        InCombat = true;
        ReadyUpButton.localPosition = new Vector3(0, 0, 0);
        ClearOutButton.localPosition = new Vector3(0, 0, 0);
    }
    public void EndCombat()
    {
        InCombat = false;
    }
    public RectTransform ButtonHolder;

    public void Attack(int AttackNumber)
    {
        CharacterManager Char = GetComponentInParent<CharacterManager>();

        //check that we are ready to attack, and that we have waited for long enough to use the attack of our choice
        if (Char.AtkCooldown < 0.1f)
        {
            // Get the x position of the ReadyUpButton
            float readyUpButtonX = ReadyUpButton.anchoredPosition.x;

            // Sum the x positions of the first AttackNumber children
            //I do not know why I have to set it to -500 but I do and it's bullshit BUG ALERT TODO MAKE THIS BETTER
            float sumXPositions = -500f;

            for (int i = 0; i < AttackNumber + 1; i++)
            {
                if (i >= transform.childCount)
                {
                    Debug.LogError("Not enough children to sum their x positions.");
                    return;
                }

                RectTransform childRect = transform.GetChild(i).GetComponent<RectTransform>();
                if (childRect != null)
                {
                    sumXPositions += childRect.rect.width;
                }
                else
                {
                    Debug.LogError("Child does not have a RectTransform component.");
                    return;
                }
            }

            // Compare the x position of ReadyUpButton with the sum of children's x positions
            if (readyUpButtonX > sumXPositions)
            {
                AttackButForReal(AttackNumber);
            }
            else
            {
                Debug.LogError("ReadyUpButton's x position is not greater than the sum of the first AttackNumber children's widths: " + sumXPositions);
            }
        }
    }
    public GameObject ButtonToSpawn;
    public void AttackButForReal(int AttackNumber)
    {
        CharacterManager Char = GetComponentInParent<CharacterManager>();

        //check that we are ready to attack, and that we have waited for long enough to use the attack of our choice
        if (Char.AtkCooldown < 0.1f)
        {
            if (Char.AtkCooldown < 0.1f)
            {


                GenericManager Reference = GetComponentInParent<GenericManager>();
                Reference.MainTextReference.TEXTBOX += "<br>" + Char.CharName + Char.Attack[AttackNumber].AttackText;
                Char.AtkCooldown = 6 - Char.Kinesthetics;

                CharacterManager[] characterManagers = GetComponentInParent<CharacterShip>().GetComponentsInChildren<CharacterManager>();
                //this is currently how we hit the defeat screen TODO add more defeat options
                // Loop through each CharacterManager and subtract 1 from Morale
                foreach (CharacterManager characterManager in characterManagers)
                {
                    characterManager.AtkCooldown += 1;
                }
                ReadyUpButton.localPosition = new Vector3(0, 0, 0);
                Instantiate(Char.Attack[AttackNumber].CharacterAtkObject, CombatLog);

                // Instantiate the new button, and then set the width to the sprite width of the attack object
                GameObject newButton = GameObject.Instantiate(ButtonToSpawn, ButtonHolder);
                Debug.Log("bro why is it failing");
                Debug.Log("value to assign the next attack button is " + Char.Attack.Length);
                int NewAtk = Random.Range(0, Char.Attack.Length);
                Debug.Log("value to assign the next attack button is " + NewAtk);
                newButton.GetComponent<CombatButton>().ActionCall = NewAtk;
                Debug.Log("spawned new combat button");
                Image ButtonImage = newButton.GetComponent<Image>();
                ButtonImage.sprite = Char.Attack[NewAtk].CharacterAtkObject.GetComponent<Image>().sprite;
                Debug.Log("replaced combat button image");
                newButton.GetComponent<RectTransform>().sizeDelta = new Vector2(ButtonImage.sprite.border.z+ButtonImage.sprite.border.x, ButtonImage.sprite.border.w+ButtonImage.sprite.border.y);

            }
        }
    }
}
