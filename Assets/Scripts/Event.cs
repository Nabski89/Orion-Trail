using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;
using TMPro;
public class Event : MonoBehaviour
{
    public Sprite EventPicture;
    public Sprite EventPictureLayer2;
    public Color[] Layer2Color;
    public GameObject Subevent1;
    public GameObject Subevent2;
    [TextArea]
    public string[] EventText;
    public string[] Options;
    public Move MoveScript;
    void Start()
    {
        MoveScript = GetComponentInParent<Move>();
    }
    public void TempEventDestro()
    {
        TriggerSubEvent1();
        Destroy(gameObject);
    }
    public void TriggerSubEvent1()
    {
        //if the sub event exists, then unparent it from this,
        if (Subevent1 != null)
        {
            Subevent1.transform.parent = transform.parent;
            Subevent1.SetActive(true);
            Destroy(gameObject);
        }
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
                        Button.GetComponent<Image>().sprite = trait.TraitIcon;
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
    }
}
