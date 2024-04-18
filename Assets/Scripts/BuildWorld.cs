using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildWorld : MonoBehaviour
{
    public Transform LocationHolder;
    public GameObject EventSetHolder;
    public GameObject Beacon;
    public int EmptySpace = 5;
    public int HazardSpace = 5;
    public int MinorSpace = 5;
    public int MajorSpace = 5;
    public int LegendarySpace = 5;
    public GameObject[] Hazard;
    public GameObject[] Minor;
    public GameObject[] Major;
    public GameObject[] StoryElement;
    public GameObject[] Obstacle;
    int StoryCount = 0;
    // Start is called before the first frame update
    float SpawnX = 0f;
    public float SpawnXOffsetPer = 20;
    float SpawnY = 0f;
    void Start()
    {
        SpawnX = -SpawnXOffsetPer;
        while (StoryCount < StoryElement.Length)
        {
            SpawnNonStory();
            SpawnNonStory();
            SpawnStory();
        }
        //activate all our children to enable the beacons
        ActivateChildren(transform);
    }

    void ActivateChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            child.gameObject.SetActive(true);
            ActivateChildren(child);
        }
    }

    private void SpawnNonStory()
    {
        MoveLocationOver();
        //the plus story count is so that we get more events later
        int SpawnCount = Random.Range(1 + StoryCount / 3, 4 + StoryCount / 2);
        int SpawnTotal = SpawnCount;
        GameObject CurrentEvent = Instantiate(EventSetHolder, LocationHolder);
        while (SpawnCount > 0)
        {
            SpawnY = SpawnTotal * 8 - SpawnCount * 12;

            int EventType = Random.Range(0, EmptySpace + HazardSpace + MinorSpace + MajorSpace + LegendarySpace + 1);
            switch (EventType)
            {
                case int n when (n >= 0 && n < EmptySpace):
                    Debug.Log("WorldBuilding: EmptySpace");
                    break;

                case int n when (n >= EmptySpace && n < EmptySpace + HazardSpace):
                    InstantiateFromArrays(Hazard, CurrentEvent);
                    break;

                case int n when (n >= EmptySpace && n < EmptySpace + HazardSpace + MinorSpace):
                    InstantiateFromArrays(Minor, CurrentEvent);
                    break;

                case int n when (n >= EmptySpace && n < EmptySpace + HazardSpace + MinorSpace + MajorSpace):
                    InstantiateFromArrays(Major, CurrentEvent);
                    break;

                case int n when (n >= EmptySpace && n < EmptySpace + HazardSpace + MinorSpace + LegendarySpace):
                    InstantiateFromArrays(StoryElement, CurrentEvent);
                    break;
            }
            SpawnCount -= 1;
        }
    }
    private void SpawnStory()
    {
        GameObject CurrentEvent = Instantiate(EventSetHolder, LocationHolder);
        MoveLocationOver();
        InstantiateFromArraysStory(StoryElement, CurrentEvent);
        StoryCount += 1;
    }
    private void InstantiateFromArrays(GameObject[] array, GameObject SpawnLocation)
    {
        // Randomly choose an object from the array
        GameObject chosenObject = array[Random.Range(0, array.Length)];
        Debug.Log("WorldBuilding: " + chosenObject);
        // Instantiate the chosen object
        GameObject EventSpawned = Instantiate(chosenObject, new Vector3(SpawnX + Random.Range(-SpawnXOffsetPer/4, SpawnXOffsetPer/4), SpawnY + Random.Range(-2f, 2f), -0.5f), Quaternion.identity, SpawnLocation.transform);
        //give it at least one beacon, maybe more
        Instantiate(Beacon, EventSpawned.transform.position, Quaternion.identity, EventSpawned.transform);

        if (Random.Range(0, 2) > 0)
        {
            Instantiate(Beacon, EventSpawned.transform.position, Quaternion.identity, EventSpawned.transform);
            if (Random.Range(0, 2) > 0)
                Instantiate(Beacon, EventSpawned.transform.position, Quaternion.identity, EventSpawned.transform);
        }


    }
    private void InstantiateFromArraysStory(GameObject[] array, GameObject SpawnLocation)
    {
        // Randomly choose an object from the array
        GameObject chosenObject = array[StoryCount];

        // Instantiate the chosen object
        GameObject EventSpawned = Instantiate(chosenObject, new Vector3(SpawnX + Random.Range(-SpawnXOffsetPer/3, SpawnXOffsetPer/3), SpawnY + Random.Range(-10f, 10f), -0.5f), Quaternion.identity, SpawnLocation.transform);
        //give it at least one beacon, maybe more
        Instantiate(Beacon, EventSpawned.transform.position, Quaternion.identity, EventSpawned.transform);

        if (Random.Range(0, 2) > 0)
        {
            Instantiate(Beacon, EventSpawned.transform.position, Quaternion.identity, EventSpawned.transform);
            if (Random.Range(0, 2) > 0)
                Instantiate(Beacon, EventSpawned.transform.position, Quaternion.identity, EventSpawned.transform);
        }
    }

    private void MoveLocationOver()
    {
        SpawnX += SpawnXOffsetPer;
        SpawnY = 0;
    }
}
