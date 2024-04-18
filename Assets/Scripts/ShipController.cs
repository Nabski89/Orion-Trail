using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShipController : MonoBehaviour
{
    public Vector2 TargetPositionSet;
    public Vector2 TargetPosition;
    public float moveSpeed = 1.0f; // Adjust the speed as needed
    public Move MoveManager;
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
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object has the EventLocation script
        Debug.Log("TRIGGER with object with SOMETHING");
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
    public void WarpShip(Vector3 NewLocation)
    {
        transform.position = NewLocation;
    }
}