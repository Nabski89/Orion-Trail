using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;
using TMPro;
public class Event : MonoBehaviour
{
    [TextArea]
    public string[] EventText;
    public string[] Options;
    public EventManager Manager;
    public GameObject[] ThingsToSpawn;
    void Start()
    {
        Manager = GetComponentInParent<EventManager>();
    }

    public void CheckCrewResponses()
    {
        //get all of the traits in our crew via the general manager
        GenericManager Holder = GetComponentInParent<GenericManager>();
        ITrait[] TraitList = Holder.ShipReference.GetComponentsInChildren<ITrait>();
        Debug.Log("Checking Traits. There are a total of " + TraitList.Length);


        foreach (ITrait trait in Holder.ShipReference.GetComponentsInChildren<ITrait>())
        {
            MonoBehaviour traitMonoBehaviour = trait as MonoBehaviour;
            if (traitMonoBehaviour != null)
            {
                //Debug.Log(traitMonoBehaviour.transform.name);
                for (int i = 0; i < PossibleResponses.Length; i++)
                {
                    //Debug.Log("Checked A Trait");
                    if (PossibleResponses[i].TraitInt == trait.TraitNumber)
                    {
                        Debug.Log("We Found A Trait we want to use and it is " + traitMonoBehaviour.transform.name);
                        EventActionButton Button = traitMonoBehaviour.GetComponentInParent<CharacterManager>().GetComponentInChildren<EventActionButton>();
                        if (trait.TraitIcon != null)
                        {
                            Button.TraitResponseValue = trait.TraitNumber;
                            Button.GetComponent<Image>().sprite = trait.TraitIcon;
                        }
                        else
                            Debug.LogWarning("Failed to find a trait icon? Check out " + trait);
                        Button.GetComponentInChildren<TextMeshProUGUI>().text = PossibleResponses[i].ActionText;
                    }
                }
            }
            else
            {
                Debug.LogWarning("Item does not inherit from MonoBehaviour");
            }
        }

    }

    public CrewResponse[] PossibleResponses;

    [System.Serializable]
    public class CrewResponse
    {
        public string DevNote;
        public string Dialog;
        public int TraitInt;
        public string ActionText;
        public GameObject SpawnBonus;
    }
}
