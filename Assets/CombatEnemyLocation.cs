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
    Image EnemyPicture;
    void Start()
    {
        EnemyPicture = GetComponent<Image>();
    }
    public void FillIn(EnemyCombatScript InLocation)
    {
        EnemyInLocation = InLocation;
        EnemyPicture.sprite = InLocation.UnitSprite;
        Filled = true;
    }
    public void FillInDelayed(EnemyCombatScript crewInLocation)
    {
        EnemyInLocation = crewInLocation;
        EnemyPicture.sprite = Blank;
        Filled = true;
        Invoke("FillInDelayCall", 0.5f);
    }
    public void FillInDelayCall()
    {
        EnemyPicture.sprite = EnemyInLocation.UnitSprite;
    }
    public void MoveOut()
    {
        Filled = false;
        EnemyPicture.sprite = Blank;
        EnemyInLocation = null;
    }
}
