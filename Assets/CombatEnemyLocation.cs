using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatEnemyLocation : MonoBehaviour
{
    public bool Filled;
    public int Rank;
    public Sprite Blank;
    public EnemyCombatScript EnemyInLocation;
    public Image EnemyPicture;
    CombatLocationsManager ComLocManager;
    void Start()
    {
        ComLocManager = GetComponentInParent<CombatLocationsManager>();
        EnemyPicture = GetComponent<Image>();
    }
    public void FillIn(EnemyCombatScript InLocation, int EnemyNumber)
    {
        transform.parent.GetComponent<Image>().color = ComLocManager.EnemyLocationHighlights[EnemyNumber];
        EnemyInLocation = InLocation;
        InLocation.Location = this;
        EnemyPicture.sprite = InLocation.UnitSprite;
        Filled = true;
    }
    public void FillInDelayed(EnemyCombatScript crewInLocation)
    {
        FillIn(crewInLocation, crewInLocation.EnemyNumber);
        EnemyPicture.sprite = Blank;
        Invoke("FillInDelayCall", 0.5f);
    }
    public void FillInDelayCall()
    {
        EnemyPicture.sprite = EnemyInLocation.UnitSprite;
    }
    public void MoveOut()
    {
        transform.parent.GetComponent<Image>().color = Color.white;
        Filled = false;
        EnemyPicture.sprite = Blank;
        EnemyInLocation = null;
    }
}
