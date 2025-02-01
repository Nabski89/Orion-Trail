using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferCard : MonoBehaviour
{
    public int TransferLocationNumber;
    public int PickUpTime = 1;
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("We have picked up a transfer card that takes you to location number" + TransferLocationNumber);
        ShipController TheShip = other.GetComponent<ShipController>();
        if (TheShip != null)
        {
            transform.parent = TheShip.transform;
            //lets just hide this out of the way
            StartCoroutine(BringToShip());
        }

        IEnumerator BringToShip()
        {
            //turn off our collider since it's going to be sitting in our ship
            Vector3 InitialPosition = transform.localPosition;
            GetComponent<BoxCollider2D>().enabled = false;
            //shrink our card and move it into the ship
            float elapsedTime = 0f;
            while (elapsedTime < PickUpTime)
            {
                transform.localPosition = Vector3.Lerp(InitialPosition, Vector3.zero, elapsedTime / PickUpTime);
                transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, elapsedTime / PickUpTime);

                // Increment the elapsed time
                elapsedTime += Time.deltaTime;

                // Wait for the next frame
                yield return null;
            }

            // Make sure it's set where you want it
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.zero;
        }
    }
}
