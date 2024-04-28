using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatUI : MonoBehaviour
{
    public float Speed = 30f;
    public float EndLocation = 150;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * Speed;
        if (transform.position.x > EndLocation)
        {
            Destroy(gameObject);
        }
    }
}
