using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [Header("References")]
    public GameObject doorParent; //Parent object for doors
    [Header("Room Status")]
    public bool isCleared = false; //Flag to check if the room is cleared
    public bool isStartRoom = false; //Flag to prevent start room from closing doors
    private bool playerInside = false; //Flag to check if the player is inside the room
    private bool isCombatActive = false; //Flag to check if combat is active in the room
    public DungeonGenerator.RoomType roomType; //Type of the room (e.g., enemy, boss, treasure)

    void Awake()
    {
        isCleared = false;
        playerInside = false;
        OpenDoors();
    }

    [Header("Wall/Door Visuals")]
    //public GameObject northDoor, southDoor, eastDoor, westDoor;
    public GameObject northWall, southWall, eastWall, westWall;

    public void SetupRoom(Vector2Int gridPos, List<Vector2Int> allRoomPositions) {
        // Check neighbors and enable/disable doors and walls
        //northDoor.SetActive(allRoomPositions.Contains(gridPos + Vector2Int.up));
        northWall.SetActive(!allRoomPositions.Contains(gridPos + Vector2Int.up));

        //southDoor.SetActive(allRoomPositions.Contains(gridPos + Vector2Int.down));
        southWall.SetActive(!allRoomPositions.Contains(gridPos + Vector2Int.down));

        //eastDoor.SetActive(allRoomPositions.Contains(gridPos + Vector2Int.right));
        eastWall.SetActive(!allRoomPositions.Contains(gridPos + Vector2Int.right));

        //westDoor.SetActive(allRoomPositions.Contains(gridPos + Vector2Int.left));
        westWall.SetActive(!allRoomPositions.Contains(gridPos + Vector2Int.left));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("--- Trigger Contact ---");
        Debug.Log("I am " + gameObject.name);
        Debug.Log("I was touched by " + other.name);
        Debug.Log("That object has tag: [" + other.tag + "]");
        Debug.Log("An object entered the room trigger: " + other.name); // Log the name of the object that entered the trigger

        if (isStartRoom)
        {
            Debug.Log("Start room entered; no doors will be triggered.");
            return;
        }

        // Check if the player entered the room
        // Check to see if the room is already cleared or if the player is already inside
        if (other.CompareTag("Player") && !isCleared && !playerInside)
        {
            Debug.Log("Player entered the room. Starting encounter..."); // Log that the player has entered the room
            OnPlayerEnter();
        }
        else
        {
            Debug.Log("The object was not tagged as Player or the room is already active.");
        }
    }

    //Called when player enters the room trigger
    public void OnPlayerEnter()
    {
        if (isStartRoom)
        {
            Debug.Log("Start room trigger ignored in OnPlayerEnter.");
            return;
        }

        if (!isCleared && !playerInside)
        {
            playerInside = true;
            
            //Trigger combat encounter based on room type
            if (roomType == DungeonGenerator.RoomType.Enemy)
            {
                CloseDoors();
                SpawnEnemies();
                isCombatActive = true;    
            }
            else if (roomType == DungeonGenerator.RoomType.Boss)
            {
                CloseDoors();
                SpawnBoss(); //You can replace this with a different function to spawn a boss instead of regular enemies
                isCombatActive = true;
            }
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
    
    [Header("Enemy Settings")]
    public GameObject[] enemyPrefab; //Reference to the enemy prefab
    public int minEnemies = 1; //Minimum number of enemies to spawn
    public int maxEnemies = 3; //Maximum number of enemies to spawn
    private List<GameObject> activeEnemies = new List<GameObject>(); //List to keep track of active enemies

    //Function to spawn enemies in the room
    void SpawnEnemies()
    {
        Debug.Log("Spawning enemies in the room...");
        
        int amount = Random.Range(minEnemies, maxEnemies + 1);

        for (int i = 0; i < amount; i++)
        {
            //Generate a random position inside the room while staying away from walls
            Vector3 spawnOffset = new Vector3(Random.Range(-3.5f, 3.5f), Random.Range(-3.5f, 3.5f), 0);

            //Pick a random enemy type from the array
            GameObject randomEnemyPrefab = enemyPrefab[Random.Range(0, enemyPrefab.Length)];

            GameObject enemy = Instantiate(randomEnemyPrefab, transform.position + spawnOffset, Quaternion.identity);
            //Set the room as the parent of the enemy for organization
            enemy.transform.parent = this.transform;
            activeEnemies.Add(enemy);
        }
    }

    void Update()
    {
        // Check if the room is active and if all enemies are defeated
        if (isCombatActive && playerInside && !isCleared && activeEnemies.Count > 0)
        {
            //Remove any null (killed) enemies form list
            activeEnemies.RemoveAll(item => item == null);

            if (activeEnemies.Count == 0)
            {
                isCombatActive = false;
                RoomCleared();
            }
        }
    }
    [Header("Boss and portal prefabs")]
    public GameObject bossPrefab;
    public GameObject portalPrefab;

    void SpawnBoss()
    {
        Debug.Log("Boss spawned");

        //Spawn the boss in the centre of the room
        GameObject boss = Instantiate(bossPrefab, transform.position, Quaternion.identity);
        boss.transform.parent = this.transform;
        activeEnemies.Add(boss);
    }

    //Function called when all enemies in the room are defeated
    public void RoomCleared()
    {
        isCleared = true;
        playerInside = false;
        OpenDoors();
        Debug.Log("Room cleared. Doors are now open.");

        if (roomType == DungeonGenerator.RoomType.Boss)
        {
            Debug.Log("Boss defeated");
            //Spawn the portal prefab in the center
            Instantiate(portalPrefab, transform.position, Quaternion.identity);
        }
    }
}
