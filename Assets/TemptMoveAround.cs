using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TemptMoveAround : MonoBehaviour
{
    public Vector2 TargetPosition;
    public float moveSpeed = 1.0f; // Adjust the speed as needed
    public Move MoveManager;
    public GameObject BackingUpSound;
    public GameObject EngineForwardSound;

    void Start()
    {
    }
    void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            // Get the mouse position in screen space
            Vector3 mousePosition = Input.mousePosition;

            // Convert the mouse position to a world position
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));
            TargetPosition = targetPosition;
            // Print the target position to the console
            Debug.Log("Target Position: " + targetPosition);
        }

        if (TargetPosition != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, TargetPosition, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(TargetPosition, transform.position) > 0.01f)
                EngineSounds(TargetPosition);
            else
                EngineSoundsDisable();
        }

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

            // Do something with the eventObject (replace this with your desired actions)
            if (eventObject != null && eventLocation.VisitedBefore == false)
            {
                Debug.Log("Collided with object with EventLocation. EventObject: " + eventObject.name);
                MoveManager.SelectNonRandomEvent(eventObject);
                eventLocation.VisitedBefore = true;
                eventLocation.GetComponent<SpriteRenderer>().color = Color.gray;
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
}