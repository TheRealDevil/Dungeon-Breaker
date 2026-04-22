using UnityEngine;

public class RoomController : MonoBehaviour
{
    [Header("References")]
    public GameObject doorParent; // Parent object for doors
    [Header("Room Status")]
    public bool isCleared = false; // Flag to check if the room is cleared
    private bool playerInside = false; // Flag to check if the player is inside the room

    void Awake()
    {
        isCleared = false;
        playerInside = false;
        OpenDoors();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("--- Trigger Contact ---");
        Debug.Log("I am " + gameObject.name);
        Debug.Log("I was touched by " + other.name);
        Debug.Log("That object has tag: [" + other.tag + "]");
        Debug.Log("An object entered the room trigger: " + other.name); // Log the name of the object that entered the trigger
        // Check if the player entered the room
        // Check to see if the room is already cleared or if the player is already inside
        if (other.CompareTag("Player") && !isCleared && !playerInside)
        {
            Debug.Log("Player entered the room. Starting encounter..."); // Log that the player has entered the room
            OnPlayerEnter();
        }
        else
        {
            Debug.Log("The object was not tagged as Player. It was tagged as: " + other.tag);
        }
    }
    //Called when player enters the room trigger
    public void OnPlayerEnter()
    {
        if (!isCleared && !playerInside)
        {
            playerInside = true;
            CloseDoors();
            SpawnEnemies();
        }
    }

    public void CloseDoors()
    {
        if (doorParent != null)
        {
            doorParent.SetActive(true);
            Debug.Log("Doors have been set to active " + doorParent.name);
        }
        else
        {
            Debug.LogError("Door parent is missing on " + gameObject.name);
        }
    }
    public void OpenDoors() => doorParent.SetActive(false);
    
    void SpawnEnemies()
    {
        Debug.Log("Spawning enemies in the room...");
        // Add enemy spawning logic here
    }
}
