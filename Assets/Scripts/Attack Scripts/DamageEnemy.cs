using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInParent<CombatController>().Enemy.HP -= 1;
    }
}
