using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    public Vector2 TargetPositionSet;
    public Vector2 TargetPosition;
    public float moveSpeed = 1.0f; // Adjust the speed as needed
    public EventManager MoveManager;
    public SpriteRenderer ShipSprite;

    void Start()
    {
        ShipSelect[] customCrews = FindObjectsOfType<ShipSelect>();

        // Loop through each CustomCrew instance
        foreach (ShipSelect CrewToLoad in customCrews)
        {
            Debug.Log("Load the ship");
            ShipSprite.sprite = CrewToLoad.Ship;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object has the EventLocation script
        Debug.Log("TRIGGER with object with SOMETHING"+ other);
        EventLocation eventLocation = other.GetComponent<EventLocation>();

        if (eventLocation != null)
        {
            // Access the EventObject from the EventLocation script
            GameObject eventObject = eventLocation.eventObject;

            // Do something with the eventObject
            if (eventObject != null && eventLocation.VisitedBefore == false)
            {
                Debug.Log("Collided with object with EventLocation. EventObject: " + eventObject.name);
                MoveManager.SelectNonRandomEvent(eventObject);
                eventLocation.VisitedBefore = true;
                eventLocation.GetComponent<SpriteRenderer>().color = Color.gray;
            }
        }
    }



    public GameObject WarpSoundEffect;
    public GameObject ShipWarpDepart;
    public GameObject ShipWarpLand;
    public void WarpShip(Vector3 NewLocation)
    {
        GameObject ButtonCoverMove = Instantiate(WarpSoundEffect);
        GameObject WarpEffect1 = Instantiate(ShipWarpDepart, transform.position - Vector3.forward, Quaternion.identity);
        GameObject WarpEffect2 = Instantiate(ShipWarpLand, NewLocation - Vector3.forward, Quaternion.identity);

        Destroy(ButtonCoverMove, 1.5f);
        Destroy(WarpEffect1, 1);
        Destroy(WarpEffect2, 1);
        StartCoroutine(MoveToTargetWithDelay(NewLocation));
        //reload the skills
        GetComponentInParent<GenericManager>().ShipReference.GetComponent<CrewSkillManager>().SkillCompleted();

    }
    IEnumerator MoveToTargetWithDelay(Vector3 NewLocation)
    {
        yield return new WaitForSeconds(0.5f);
        transform.position = NewLocation;
    }



    /*

        public GameObject BackingUpSound;
    public GameObject EngineForwardSound;
    public ShipJiggle ShipJiggler;
    void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            //            SetMouseClickTarget();
        }
        CheckMoveSpeed();
        if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            TargetPosition = TargetPositionSet;
        }
        AreWeThereYet();
    }

    void SetMouseClickTarget()
    {
        // Get the mouse position in screen space
        Vector3 mousePosition = Input.mousePosition;

        // Convert the mouse position to a world position
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));
        TargetPositionSet = targetPosition;
        // Print the target position to the console
        Debug.Log("Target Position: " + targetPosition);
        moveSpeed += 3 * Time.deltaTime;
    }

        void AreWeThereYet()
    {
        if (TargetPosition != Vector2.zero)
        {
            transform.position = Vector2.MoveTowards(transform.position, TargetPosition, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(TargetPosition, transform.position) > 0.01f)
                EngineSounds(TargetPosition);
            else
            {
                EngineSoundsDisable();
                moveSpeed = 0;
                TargetPosition = Vector2.zero;
            }
        }
    }
    public void EngineSounds(Vector3 targetPosition)
    {
        if (TargetPosition.x < transform.position.x)
        {
            BackingUpSound.SetActive(true);
            EngineForwardSound.SetActive(false);
        }
        else
        {
            BackingUpSound.SetActive(false);
            EngineForwardSound.SetActive(true);
        }
    }
    public void EngineSoundsDisable()
    {
        BackingUpSound.SetActive(false);
        EngineForwardSound.SetActive(false);
    }

    public void CheckMoveSpeed()
    {
        if (moveSpeed > 0)
            moveSpeed = moveSpeed * (1 - 0.2f * Time.deltaTime) - 0.2f * Time.deltaTime; ;

    }
    */
}