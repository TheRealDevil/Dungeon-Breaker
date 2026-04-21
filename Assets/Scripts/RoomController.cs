using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;


public class RoomController : MonoBehaviour
{
    [Header("References")]
    public GameObject doorParent; // Parent object for doors
    [Header("Room Status")]
    public bool isCleared = false; // Flag to check if the room is cleared
    private bool playerInside = false; // Flag to check if the player is inside the room

    void Start()
    {
        OpenDoors();
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

    public void CloseDoors() => doorParent.SetActive(true);
    public void OpenDoors() => doorParent.SetActive(false);
    
    void SpawnEnemies()
    {
        Debug.Log("Spawning enemies in the room...");
        // Add enemy spawning logic here
    }
}
