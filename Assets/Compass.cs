using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    public GameObject SunIcon;
    public GameObject PlanetIconToSpawn;
    public PlanetTag[] PlanetIcons;
    public void InitializeMap(MapHolderTag MapInfo)
    {
        //clear everything old out first
        foreach (Transform child in transform)
            Destroy(child.gameObject);
        //then spawn all the new planets
        PlanetIcons = MapInfo.TaggedPlanets;

        foreach (PlanetTag planetTag in PlanetIcons)
        {
            // Instantiate the PlanetIconToSpawn prefab
            GameObject planetIcon = Instantiate(PlanetIconToSpawn, transform);
            //we get the first child of the planet icon and give it a distance
            planetIcon.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition += Vector2.right * planetTag.transform.localPosition.x * 3;
            planetIcon.transform.GetChild(0).GetComponent<Image>().sprite = planetTag.GetComponent<SpriteRenderer>().sprite;
            planetIcon.GetComponent<RectTransform>().rotation = planetTag.transform.rotation;
            // Optionally, set the planetTag or other properties on the instantiated object
            //    planetIcon.GetComponent<YourPlanetIconScript>().SetTag(planetTag);

        }
    }
    public void UpdateMap(MapHolderTag MapInfo)
    {

    }
    //the actual map updating is done by the planetary system, this is just a tag
}
