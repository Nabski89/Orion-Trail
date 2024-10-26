using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatLocationsManager : MonoBehaviour
{
    public CombatCrewLocation[] Location;
    public CombatEnemyLocation[] EnemyLocation;
    public Color[] EnemyLocationHighlights;
    public int Movements;
    public TextMeshProUGUI MovementText;
    // Update is called once per frame
    public void ClearOutForNewCombat()
    {
        //update our UI text
        MovementText.text = Movements.ToString();
        //move out all previous crew and enemies
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
    public void DropInEnemy(EnemyCombatScript EnemyMate, int EnemyNum)
    {
        CombatEnemyLocation randomUnfilledLocation = GetRandomUnfilledEnemyLocation();
        if (randomUnfilledLocation != null)
            randomUnfilledLocation.FillIn(EnemyMate, EnemyNum);
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
        //same as above but for enemies, see the comments there
        List<CombatEnemyLocation> unfilledLocations = new List<CombatEnemyLocation>();
        for (int i = 0; i < EnemyLocation.Length; i++)
        {
            if (EnemyLocation[i].Filled == false)
                unfilledLocations.Add(EnemyLocation[i]);
        }
        if (unfilledLocations.Count > 0)
        {
            int randomIndex = Random.Range(0, unfilledLocations.Count);
            return unfilledLocations[randomIndex];
        }
        return null;
    }
    public void CheckIfStoreMove()
    {
        Movements -= 1;
        MovementText.text = Movements.ToString();
    }
    public void MoveToEmpty(CombatCrewLocation CrewHere)
    {
        CheckIfStoreMove();
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
    //currently this is triggered by a button when you click on an enemy
    //same as above except the last line
    public void MoveEnemyToEmpty(CombatEnemyLocation EnemyHere)
    {
        CheckIfStoreMove();
        if (EnemyHere.EnemyInLocation == null)
            Debug.LogWarning("Tried to move an enemy without an enemy actually in the location");
        else
        {
            CombatEnemyLocation EmptySpot = null;
            EmptySpot = GetRandomUnfilledEnemyLocation();
            EmptySpot.FillInDelayed(EnemyHere.EnemyInLocation);
            Debug.Log(EnemyHere.transform.localPosition);
            GameObject CrewMover = Instantiate(MoveableObject, EnemyHere.transform.localPosition, Quaternion.identity, transform);
            CrewMover.GetComponent<Image>().sprite = EnemyHere.EnemyPicture.sprite;
            EnemyHere.MoveOut();
            RectTransform Start = EnemyHere.GetComponent<RectTransform>();
            RectTransform End = EmptySpot.GetComponent<RectTransform>();
            //the parent and plus ten are because there's bases for these that the non enemies don't have
            StartCoroutine(Move(EnemyHere.transform.parent.localPosition + Vector3.up * 10, EmptySpot.transform.parent.localPosition + Vector3.up * 10, CrewMover.GetComponent<RectTransform>()));
        }
    }
    public GameObject MoveableObject;

    public Sprite Blank;
    IEnumerator Move(Vector3 InitialPosition, Vector3 FinalPosition, RectTransform ToMove)
    {
        float elapsedTime = 0f;
        //slowly smoothly move to the target location
        while (elapsedTime < 0.5f)
        {
            ToMove.localPosition = Vector3.Lerp(InitialPosition, FinalPosition, elapsedTime / 0.5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Ensure the final location and clear the sprite
        ToMove.localPosition = FinalPosition;
        ToMove.GetComponent<Image>().sprite = Blank;
        Destroy(ToMove.gameObject);
    }
    public void AttackEnemy(int Rank, int Bonus)
    {

        if (Rank == 0)
        {
            DamageEnemy(0, 1 + Bonus);
            DamageEnemy(1, 1 + Bonus);
        }
        if (Rank == 1)
        {
            DamageEnemy(2, 1 + Bonus);
            DamageEnemy(3, 1 + Bonus);
        }
        if (Rank == 2)
        {
            DamageEnemy(4, 1 + Bonus);
            DamageEnemy(5, 1 + Bonus);
        }
    }
    void DamageEnemy(int location, int DamageAmount)
    {
        Debug.Log("the enemy we are trying to hit is " + EnemyLocation[location].EnemyInLocation + " in slot " + location);
        if (EnemyLocation[location].EnemyInLocation != null)
        {
            StartCoroutine(FlashColor(EnemyLocation[location].transform));
            EnemyLocation[location].EnemyInLocation.GetAttacked(DamageAmount);
            Debug.Log("okay we did it reddit is " + EnemyLocation[location].EnemyInLocation + " in slot " + location);
        }
    }
    IEnumerator FlashColor(Transform Element)
    {
        Image SetMe = Element.GetComponent<Image>();
        SetMe.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        SetMe.color = new Color(1, .25f, .25f, 1);
        yield return new WaitForSeconds(0.05f);
        SetMe.color = new Color(1, .5f, .5f, 1);
        yield return new WaitForSeconds(0.05f);
        SetMe.color = Color.white;
    }
}