using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BadgeColor : MonoBehaviour
{
    public int BadgeNumber;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().color = GetComponentInParent<CharacterManager>().CharacterColor[BadgeNumber];
    }
}
