using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillWeapon : MonoBehaviour, ITrait
{
    [SerializeField]
    private int traitNumber;

    public int TraitNumber
    {
        get { return traitNumber; }
    }
    [SerializeField]
    private Sprite traitIcon;

    public Sprite TraitIcon
    {
        get { return traitIcon; }
    }

    [SerializeField]
    private string traitName;
    public string TraitName
    {
        get { return traitName; }
    }

    [SerializeField]
    private string traitDescription;
    public string TraitDescription
    {
        get { return traitDescription; }
    }
    // Start is called before the first frame update
    public void CombatBonus()
    {

    }

    // Update is called once per frame
    public void SkillBonus()
    {

    }
}
