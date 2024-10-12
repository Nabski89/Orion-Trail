using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatScreenMover : MonoBehaviour
{

    public RectTransform Piston1;
    public RectTransform Piston2;
    public RectTransform MainScreen;
    public float Piston1Delay;
    public float Piston2Delay;
    public float MainScreenDelay;
    public int Piston1WidthGoal;
    public int Piston2WidthGoal;
    public int MainScreenWidthGoal;
    public float Piston1Duration;
    public float Piston2Duration;
    public float MainScreenDuration;
    public void Lockdown()
    {
        Debug.Log("Put Away Stat Screen");
        StartCoroutine(Move(Piston1, Piston1Delay + Piston2Duration + MainScreenDuration, 0, Piston1Duration));
        StartCoroutine(Move(Piston2, Piston2Delay + MainScreenDuration, 0, Piston2Duration));
        StartCoroutine(Move(MainScreen, 0, 0, MainScreenDuration));
    }
    public void UnLockdown()
    {
        Debug.Log("Bring Out Stat Screen");
        StartCoroutine(Move(Piston1, Piston1Delay, Piston1WidthGoal, Piston1Duration));
        StartCoroutine(Move(Piston2, Piston2Delay, Piston2WidthGoal, Piston2Duration));
        StartCoroutine(Move(MainScreen, MainScreenDelay, MainScreenWidthGoal, MainScreenDuration));
    }
    public void ChangeType()
    {
        Debug.Log("Change who is on the stat screen");
        //bring it down to zero then bring it back out
        StartCoroutine(Move(MainScreen, 0, 0, MainScreenDuration / 2));
        StartCoroutine(Move(MainScreen, MainScreenDuration, MainScreenWidthGoal, MainScreenDuration / 2));
    }
    IEnumerator Move(RectTransform MoveMe, float Delay, int WidthGoal, float Duration)
    {

        yield return new WaitForSeconds(Delay);
        {
            float startWidth = MoveMe.sizeDelta.x;
            float elapsedTime = 0f;

            while (elapsedTime < Duration)
            {
                elapsedTime += Time.deltaTime;
                float newWidth = Mathf.Lerp(startWidth, WidthGoal, elapsedTime / Duration);
                MoveMe.sizeDelta = new Vector2(newWidth, MoveMe.sizeDelta.y);
                yield return null;
            }
            MoveMe.sizeDelta = new Vector2(WidthGoal, MoveMe.sizeDelta.y);

        }
    }
}