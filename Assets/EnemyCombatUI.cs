using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatUI : MonoBehaviour
{
    public float Speed = 30f;
    public float EndLocation = 150;

    public DamageCrew[] DamList;
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * Speed;
        if (transform.position.x > EndLocation)
        {
            for (int i = 0; i < DamList.Length; i++)
            {
                if (DamList[i] != null)
                    DamList[i].enabled = true;
            }
            //this delay is because we are doing things in start which if you destroy the gameobject  doesn't have time to trigger //TODO make this better code
            Destroy(gameObject, 0.1f);
        }

    }
}