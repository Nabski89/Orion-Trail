using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBeaconPath : MonoBehaviour
{
    public GameObject BeaconPrefab; // Assign your Beacon prefab in the Inspector
    public Vector2 PointA;
    public Vector2 PointB;
    public float distanceBetweenBeacons = 2f;
    public Color BeaconColor = Color.white;

    void Start()
    {
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
            Instantiate(BeaconPrefab, new Vector3(BeaconSpawn.x, BeaconSpawn.y, 0f), Quaternion.identity, transform);
            start = BeaconSpawn;
        }
    }
}