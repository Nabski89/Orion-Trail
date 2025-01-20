using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpGateLevelEnd : MonoBehaviour
{
    public GameObject MapToGenerate;
    public Transform ParentObject;
    public SpriteRenderer SpaceBackground;
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("We hit" + other);
        ShipController TheShip = other.GetComponent<ShipController>();
        if (TheShip != null)
        {
            //enable the card system
            //select what system you want to go to
            StartNewZone();
            //warp the ship to the new location

            MapHolderTag[] TotalSystems = ParentObject.GetComponentsInChildren<MapHolderTag>();
            //the minus 1 is because we just instantiated one of them. The -7 is to put you at the start of the system
            TheShip.transform.position = Vector3.right * ((TotalSystems.Length - 1) * 50);
            Debug.Log(Vector3.right * ((TotalSystems.Length - 1) * 50));
        }
    }
    public void StartNewZone()
    {
        //we move over 50 for the world map
        //then 14 is the total width
        Transform ParentObject = GetComponentInParent<NonCanvasTag>().transform;
        MapHolderTag[] TotalSystems = ParentObject.GetComponentsInChildren<MapHolderTag>();
        Instantiate(MapToGenerate, Vector3.right * (TotalSystems.Length * 50), Quaternion.identity, ParentObject);

        StartCoroutine(ChangeColorCoroutine(TargetColor, duration));
    }
    // Start is called before the first frame update
    void Start()
    {
        NonCanvasTag NonCanvasHolder = GetComponentInParent<NonCanvasTag>();
        ParentObject = NonCanvasHolder.transform;
        SpaceBackground = NonCanvasHolder.SpaceBackground;
    }








    public Color WarpColor = Color.red;
    public Color TargetColor = Color.red; // The color you want to change to
    public float duration = 2.5f; // Duration for the color change

    IEnumerator ChangeColorCoroutine(Color newColor, float time)
    {
        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            // Interpolate between the initial color and the new color
            SpaceBackground.color = Color.Lerp(WarpColor, newColor, elapsedTime / time);

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the final color is set after the loop
        SpaceBackground.color = newColor;
    }
}
