using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBeaconPath : MonoBehaviour
{
    public GameObject BeaconPrefab; // Assign your Beacon prefab in the Inspector
    public Vector2 PointA;
    public Vector2 PointB;
    public float distanceBetweenBeacons = 2f;
    public Color[] BeaconColorOptions;
    public Color BeaconColor = Color.white;

    void Start()
    {
        BeaconColor = BeaconColorOptions[Random.Range(0, BeaconColorOptions.Length)];
        PointA = transform.parent.transform.position;
        GPTChildFinding();
        PointB = nextSibling.GetChild(Random.Range(0, nextSibling.transform.childCount)).transform.position;
        InstantiateBeaconsAlongLine(PointA, PointB, distanceBetweenBeacons);
    }

    void InstantiateBeaconsAlongLine(Vector2 start, Vector2 end, float distance)
    {
        float totalDistance = Vector2.Distance(start, end);
        int numberOfBeacons = Mathf.FloorToInt(totalDistance / distance);

        while (Vector2.Distance(start, end) > distanceBetweenBeacons * 2)
        {
            float t = distanceBetweenBeacons / Vector2.Distance(start, end);
            Vector2 BeaconSpawn = Vector2.Lerp(start, end, t);
            BeaconSpawn.x += Random.Range(-0.5f, 0.5f);
            BeaconSpawn.y += Random.Range(-0.5f, 0.5f);
            //the .75 is to get it above some other stuff
            Instantiate(BeaconPrefab, new Vector3(BeaconSpawn.x, BeaconSpawn.y, -0.75f), Quaternion.identity, transform);
            start = BeaconSpawn;
        }
    }


    private int childIndex;
    private int siblingCount;
    private Transform nextSibling;
    void GPTChildFinding()
    {
        // Get the parent of this object
        Transform parent = transform.parent.parent.parent;

        // Get the total number of siblings
        siblingCount = parent.childCount;

        // Loop through the siblings to find the index of this object
        for (int i = 0; i < siblingCount; i++)
        {
            if (parent.GetChild(i) == transform.parent.parent)
            {
                childIndex = i;
                break;
            }
        }
        // Set the next sibling
        if (childIndex < siblingCount - 1)
        {
            nextSibling = parent.GetChild(childIndex + 1);
        }
        else
        {
            // If this is the last child, set nextSibling to null
            nextSibling = null;
        }
    }
}