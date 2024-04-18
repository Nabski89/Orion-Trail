using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Kill : MonoBehaviour
{
    public void KillCharacter()
    {
        GetComponentInParent<CharacterManager>().Die();
    }
}
