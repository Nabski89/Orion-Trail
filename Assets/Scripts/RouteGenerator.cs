using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteGenerator : MonoBehaviour
{
    public int RoutesToSpawn;

    public Vector3 SpawnNextItem;
    public GameObject[] RouteStart;
    public GameObject[] RouteMid;
    public GameObject[] RouteEnd;
    public GameObject[] Hazards;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < RoutesToSpawn; i++)
            SpawnRoute();
    }

    // Update is called once per frame
    void SpawnRoute()
    {
        int RouteSegments = Random.Range(1, 4);
        for (int j = 0; j < RouteSegments; j++)
        {
            Debug.Log("Building a route");
            GameObject Route = Instantiate(RouteStart[Random.Range(0, RouteStart.Length)], SpawnNextItem, Quaternion.identity, transform);
            SpawnNextItem = Route.GetComponent<RouteData>().RouteSectionEnd.position;
        }
        Instantiate(RouteEnd[Random.Range(0, RouteEnd.Length)], SpawnNextItem, Quaternion.identity, transform);
        SpawnNextItem += Vector3.right * 1;
    }
}
