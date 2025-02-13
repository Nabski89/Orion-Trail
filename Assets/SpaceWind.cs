using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class SpaceWind : MonoBehaviour
{
    public float scrollSpeedX = 0.5f; // Speed of scrolling in the X direction
    public float scrollSpeedY = 0.5f; // Speed of scrolling in the Y direction
    private Renderer rend;
    public float WindPower = 1;
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    //oh god this permanently updates the material and everywhere it is used that's truely something.
    void Update()
    {
        float offsetX = Mathf.Repeat(Time.time * scrollSpeedX, 1);
        float offsetY = Mathf.Repeat(Time.time * scrollSpeedY, 1);
        Vector2 offset = new Vector2(offsetX, offsetY);
        rend.sharedMaterial.mainTextureOffset = offset;
    }
    void OnTriggerStay2D(Collider2D other)
    {

        if (other.GetComponent<TravelBall>() != null)
        {
            other.transform.position -= WindPower * transform.right * Time.deltaTime;
        }
    }
}