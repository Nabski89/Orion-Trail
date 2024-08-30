using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealMinigame : MonoBehaviour, ISkillMinigame
{
    public float SkillModifier { get; }
//this is supposed let me set my interfaced theing through the inspector
    [SerializeField]
    public GameObject HoldMinigame;

    public GameObject MinigameHolder
    {
        get { return HoldMinigame; }
        set { HoldMinigame = value; }
    }
    public CrewSkillManager crewSkillManager { get; set; }
    void Start()
    {
        crewSkillManager = GetComponentInParent<GenericManager>().ShipReference.GetComponent<CrewSkillManager>();
    }
    public void ActivateSkill()
    {
        MinigameHolder.SetActive(true);
        StartUp();
    }

    public SkillUIMovement[] SkillUI;
    public float[] SkillUITiming;
    public Vector3 targetPosition;  // The position to move to
    public float moveDuration = 1.0f;  // Duration of the movement
    public void StartUp()
    {
        for (int i = 0; i < SkillUITiming.Length; i++)
        {
            StartMoving(i);
        }
        Invoke("StartMoving", 2.0f);
        Invoke("EndSkill", 5.0f);
    }
    void StartMoving(int UIOrder)
    {
        SkillUI[UIOrder].Move(UIOrder);
    }

    IEnumerator MoveToPosition(Vector3 target, float duration)
    {
        Vector3 startPosition = transform.position;  // The current position of the object
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            // Calculate the new position using Lerp
            transform.position = Vector3.Lerp(startPosition, target, elapsedTime / duration);

            // Increase elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the object is exactly at the target position at the end
        transform.position = target;
    }

    public void EndSkill()
    {
        crewSkillManager.GetComponent<CrewSkillManager>().SkillCompleted();
    }
}
