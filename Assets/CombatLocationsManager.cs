using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatLocationsManager : MonoBehaviour
{
    public CombatCrewLocation[] Location;
    // Update is called once per frame
    public void DropInCrew(CharacterManager CrewMate)
    {
        CombatCrewLocation randomUnfilledLocation = GetRandomUnfilledLocation();
        if (randomUnfilledLocation != null)
        {
            randomUnfilledLocation.FillIn(CrewMate);
        }
        else
        {
            Debug.Log("All locations are filled.");
        }
    }
    CombatCrewLocation GetRandomUnfilledLocation()
    {
        // Create a list to hold unfilled locations
        List<CombatCrewLocation> unfilledLocations = new List<CombatCrewLocation>();

        // Loop through each location in the array
        foreach (CombatCrewLocation ThisSpot in Location)
        {
            // Check if the Filled property is false
            if (!ThisSpot.Filled)
            {
                unfilledLocations.Add(ThisSpot);
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
    public void MoveOutCrew()
    {
        for (int i = 0; i < Location.Length; i++)
        {
            Location[i].MoveOut();
        }
    }
}
