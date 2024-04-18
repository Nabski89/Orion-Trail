using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMoveByButtonSmooth : MonoBehaviour
{
    public bool Left = false;
    public Vector2 LeftLocation;
    public Vector2 RightLocation;
    // Start is called before the first frame update
    void Start()
    {
        LeftLocation = transform.position;
    }
    public void MoveItOver()
    {
        Left = !Left;
        if (Left == true)
            targetPosition = LeftLocation;
        else
            targetPosition = RightLocation;
        StartCoroutine(MoveTowardsTarget());
    }

    //Some GPT nonsense, editted for what I want
    public Vector2 targetPosition; // Set your target position

    public float moveSpeed = 100f; // Set your move speed

    // Call this function to initiate the movement
    private IEnumerator MoveTowardsTarget()
    {
        while (Vector2.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition;
    }
}