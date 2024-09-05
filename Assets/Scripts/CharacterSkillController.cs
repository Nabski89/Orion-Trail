using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkillController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CallForSkills();
    }

    // Update is called once per frame
    public void CallForSkills()
    {
        int childCount = transform.childCount;

        // If there are no children, return
        if (childCount == 0) return;

        // Deactivate all child objects
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        // Activate one random child object
        int randomIndex = Random.Range(0, childCount);
        transform.GetChild(randomIndex).gameObject.SetActive(true);
    }
    public void CullSkills()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
