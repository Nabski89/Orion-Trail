using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatLocationsManager : MonoBehaviour
{
    public CombatCrewLocation[] Location;
    public CombatEnemyLocation[] EnemyLocation;
    // Update is called once per frame
    public void ClearOutForNewCombat()
    {
        for (int i = 0; i < Location.Length; i++)
        {
            Location[i].Filled = false;
            Location[i].MoveOut();
        }
        for (int i = 0; i < EnemyLocation.Length; i++)
        {
            EnemyLocation[i].Filled = false;
            EnemyLocation[i].MoveOut();
        }
    }
    public void DropInCrew(CharacterManager CrewMate)
    {
        CombatCrewLocation randomUnfilledLocation = GetRandomUnfilledLocation();
        if (randomUnfilledLocation != null)
        {
            randomUnfilledLocation.FillIn(CrewMate);
        }
        else
            Debug.Log("All locations are filled.");
    }
    public void DropInEnemy(EnemyCombatScript EnemyMate)
    {
        CombatEnemyLocation randomUnfilledLocation = GetRandomUnfilledEnemyLocation();
        if (randomUnfilledLocation != null)
        {
            randomUnfilledLocation.FillIn(EnemyMate);
        }
        else
            Debug.Log("All locations are filled.");
    }

    CombatCrewLocation GetRandomUnfilledLocation()
    {
        // Create a list to hold unfilled locations
        List<CombatCrewLocation> unfilledLocations = new List<CombatCrewLocation>();
        // Loop through each location in the array
        for (int i = 0; i < Location.Length; i++)
        {
            // Check if the Filled property is false
            if (Location[i].Filled == false)
            {
                unfilledLocations.Add(Location[i]);
            }
        }
        // Return a random unfilled location if any exist
        if (unfilledLocations.Count > 0)
        {
            int randomIndex = Random.Range(0, unfilledLocations.Count);
            return unfilledLocations[randomIndex];
        }
        // Return null if no unfilled location is found
        return null;
    }
    CombatEnemyLocation GetRandomUnfilledEnemyLocation()
    {
        // Create a list to hold unfilled locations
        List<CombatEnemyLocation> unfilledLocations = new List<CombatEnemyLocation>();
        // Loop through each location in the array
        for (int i = 0; i < EnemyLocation.Length; i++)
        {
            // Check if the Filled property is false
            if (EnemyLocation[i].Filled == false)
            {
                unfilledLocations.Add(EnemyLocation[i]);
            }
        }
        // Return a random unfilled location if any exist
        if (unfilledLocations.Count > 0)
        {
            int randomIndex = Random.Range(0, unfilledLocations.Count);
            return unfilledLocations[randomIndex];
        }
        // Return null if no unfilled location is found
        return null;
    }
    public void MoveToEmpty(CombatCrewLocation CrewHere)
    {
        CombatCrewLocation EmptySpot = null;
        //find our empty spot
        for (int i = 0; i < Location.Length; i++)
            if (Location[i].CrewInLocation == false)
                EmptySpot = Location[i];
        //get empty out the old slot
        EmptySpot.FillInDelayed(CrewHere.CrewInLocation);
        //spawn the moving object and change the picture to who is moving in
        GameObject CrewMover = Instantiate(MoveableObject, CrewHere.transform.localPosition, Quaternion.identity, transform);
        CrewMover.GetComponent<Image>().sprite = CrewHere.CrewInLocation.CharacterPicture;
        //move out the old picture
        CrewHere.MoveOut();
        //start the actual moving section
        StartCoroutine(Move(CrewHere.transform.localPosition, EmptySpot.transform.localPosition, CrewMover.GetComponent<RectTransform>()));
    }
    public GameObject MoveableObject;

    public Sprite Blank;
    IEnumerator Move(Vector3 InitialPosition, Vector3 FinalPosition, RectTransform ToMove)
    {
        float elapsedTime = 0f;
        //slowly smoothly move to the target location
        while (elapsedTime < 0.5f)
        {
            ToMove.localPosition = Vector3.Lerp(InitialPosition - Vector3.up * 41, FinalPosition - Vector3.up * 41, elapsedTime / 0.5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Ensure the final location and clear the sprite
        ToMove.localPosition = FinalPosition;
        ToMove.GetComponent<Image>().sprite = Blank;
        Destroy(ToMove.gameObject);
    }
}