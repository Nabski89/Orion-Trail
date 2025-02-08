using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    public GameObject SunIcon;
    public GameObject PlanetIconToSpawn;
    public PlanetTag[] PlanetReference;
    public void InitializeMap(MapHolderTag MapInfo)
    {
        //clear everything old out first
        foreach (Transform child in transform)
            Destroy(child.gameObject);
        //then spawn all the new planets
        PlanetReference = MapInfo.TaggedPlanets;
        for (int i = 0; i < PlanetReference.Length; i++)
        {
            // Create the Planet Icon
            GameObject planetIcon = Instantiate(PlanetIconToSpawn, transform);
            //we get the first child of the planet icon and give it a distance as well as the matching planet sprite
            planetIcon.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition += Vector2.right * PlanetReference[i].transform.localPosition.x * 3;
            planetIcon.transform.GetChild(0).GetComponent<Image>().sprite = PlanetReference[i].GetComponent<SpriteRenderer>().sprite;
            //Then set the rotation
            planetIcon.GetComponent<RectTransform>().rotation = PlanetReference[i].transform.rotation;
        }
    }
    public void ClearMap()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
        PlanetReference = null;
    }
    void Update()
    {
        if (PlanetReference != null)
        {
            for (int i = 0; i < PlanetReference.Length; i++)
            {
                RectTransform planetIcon = transform.GetChild(i).GetComponent<RectTransform>();
                planetIcon.rotation = PlanetReference[i].transform.rotation;
            }
        }
    }
    //the actual map updating is done by the planetary system, this is just a tag
}
