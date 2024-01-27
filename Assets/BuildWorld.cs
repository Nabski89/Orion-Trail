using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildWorld : MonoBehaviour
{
    public Transform LocationHolder;
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
    float SpawnY = 0f;
    void Start()
    {
        while (StoryCount < StoryElement.Length)
        {
            MoveLocationOver();
            //the plus story count is so that we get more events later
            int SpawnCount = Random.Range(1 + StoryCount / 3, 4 + StoryCount / 2);
            int SpawnTotal = SpawnCount;
            while (SpawnCount > 0)
            {
                SpawnY = SpawnTotal * 2 - SpawnCount * 3 + 1;

                int EventType = Random.Range(0, EmptySpace + HazardSpace + MinorSpace + MajorSpace + LegendarySpace + 1);
                switch (EventType)
                {
                    case int n when (n >= 0 && n < EmptySpace):
                        Debug.Log("WorldBuilding: EmptySpace");
                        break;

                    case int n when (n >= EmptySpace && n < EmptySpace + HazardSpace):
                        InstantiateFromArrays(Hazard);
                        break;

                    case int n when (n >= EmptySpace && n < EmptySpace + HazardSpace + MinorSpace):
                        InstantiateFromArrays(Minor);
                        break;

                    case int n when (n >= EmptySpace && n < EmptySpace + HazardSpace + MinorSpace + MajorSpace):
                        InstantiateFromArrays(Major);
                        break;

                    case int n when (n >= EmptySpace && n < EmptySpace + HazardSpace + MinorSpace + LegendarySpace):
                        InstantiateFromArrays(StoryElement);
                        break;
                }
                SpawnCount -= 1;
            }


            MoveLocationOver();
            InstantiateFromArraysStory(StoryElement);
            StoryCount += 1;
        }
    }


    private void InstantiateFromArrays(GameObject[] array)
    {
        // Randomly choose an object from the array
        GameObject chosenObject = array[Random.Range(0, array.Length)];
        Debug.Log("WorldBuilding: " + chosenObject);
        // Instantiate the chosen object
        Instantiate(chosenObject, new Vector3(SpawnX + Random.Range(-0.15f, 0.15f), SpawnY + Random.Range(-0.15f, 0.15f), -0.5f), Quaternion.identity, LocationHolder);
    }
    private void InstantiateFromArraysStory(GameObject[] array)
    {
        // Randomly choose an object from the array
        GameObject chosenObject = array[StoryCount];

        // Instantiate the chosen object
        Instantiate(chosenObject, new Vector3(SpawnX, SpawnY + Random.Range(-2.15f, 2.15f), -0.5f), Quaternion.identity, LocationHolder);
    }

    private void MoveLocationOver()
    {
        SpawnX += 6;
        SpawnY = 0;
    }
}
