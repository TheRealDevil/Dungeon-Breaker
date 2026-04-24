using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public enum RoomType {Start, Enemy, Boss, Treasure}
    
    [System.Serializable]
    public class RoomData {
        public Vector2Int gridPos;
        public RoomType type;

        public RoomData(Vector2Int pos, RoomType t) {
            gridPos = pos;
            type = t;
        }
    }

    [Header("Generator Settings")]
    public int numberOfRooms = 5;
    public int roomSize = 25; //size of the rooms in world units

    [Header("Room Prefabs")]
    public GameObject startRoomPrefab;
    public GameObject enemyRoomPrefab;
    public GameObject bossRoomPrefab;
    public GameObject treasureRoomPrefab;

    //Saves the whole layout of the dungeon
    private List<RoomData> dungeonLayout = new List<RoomData>();

    void Start()
    {
        GenerateBlueprint();
        SpawnRooms();
    }

    void GenerateBlueprint()
    {
        dungeonLayout.Clear();
        Vector2Int currentPos = Vector2Int.zero;

        //Start room is always at (0,0)
        dungeonLayout.Add(new RoomData(currentPos, RoomType.Start));

        //Random room generation with a random walk algorithm
        int failedAttempts = 0;
        const int maxFailedAttempts = 100;
        while (dungeonLayout.Count < numberOfRooms && failedAttempts < maxFailedAttempts)
        {
            //Pick a random room that we already spawned
            RoomData randomRoom = dungeonLayout[Random.Range(0, dungeonLayout.Count)];
            Vector2Int potentialPos = randomRoom.gridPos + GetRandomDirection();

            //Check if the position is already occupied
            if (!IsPositionOccupied(potentialPos))
            {
                dungeonLayout.Add(new RoomData(potentialPos, RoomType.Enemy));
                failedAttempts = 0;
            }
            else
            {
                failedAttempts++;
            }
        }

        //Boss room generation
        SetBossRoom();

        //Debug log of the generated layout
        foreach (var room in dungeonLayout)
        {
            Debug.Log($"Místnost na {room.gridPos} je {room.type}");
        }
    }

    //Function for spawning rooms based on the generated blueprint
    void SpawnRooms() {
        
        //List of room coordinates to check for neighbors
        List<Vector2Int> occupiedPositions = new List<Vector2Int>();
        foreach (var room in dungeonLayout) occupiedPositions.Add(room.gridPos);

        foreach (RoomData room in dungeonLayout) {
            //Calculation of world position based on grid position and room size
            Vector3 spawnPos = new Vector3(room.gridPos.x * roomSize, room.gridPos.y * roomSize, 0);

            GameObject prefabToSpawn = GetPrefabByType(room.type);
            if (prefabToSpawn != null) {
                GameObject newRoom = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
                newRoom.name = $"{room.type} Room ({room.gridPos.x}, {room.gridPos.y})";
                newRoom.transform.parent = this.transform; //Organization in the hierarchy

                RoomController controller = newRoom.GetComponent<RoomController>();
                if (controller != null)
                {
                    controller.SetupRoom(room.gridPos, occupiedPositions);
                    controller.isStartRoom = room.type == RoomType.Start;
                }
            }
            else
            {
                Debug.LogError($"Missing prefab for room type {room.type}");
            }
        }
    }

    GameObject GetPrefabByType(RoomType type) {
        switch (type) {
            case RoomType.Start: return startRoomPrefab;
            case RoomType.Boss: return bossRoomPrefab;
            case RoomType.Treasure: return treasureRoomPrefab;
            default: return enemyRoomPrefab; //Default is enemy room
        }
    }

    Vector2Int GetRandomDirection()
    {
        int choice = Random.Range(0, 4);
        return choice switch
        {
            0 => Vector2Int.up,
            1 => Vector2Int.down,
            2 => Vector2Int.left,
            _ => Vector2Int.right,
        };
    }

    bool IsPositionOccupied(Vector2Int pos)
    {
        return dungeonLayout.Exists(r => r.gridPos == pos);
    }

    void SetBossRoom()
    {
        RoomData furthestRoom = null;
        float maxDistance = 0;

        //The boss room is set the furthest from the start room
        foreach (var room in dungeonLayout)
        {
            if (room.type == RoomType.Start) continue; //Skip start room
            
            float distance = Vector2.Distance(Vector2.zero, (Vector2)room.gridPos);
            if (distance > maxDistance)
            {
                maxDistance = distance;
                furthestRoom = room;
            }
        }
        
        if (furthestRoom != null)
        {
            furthestRoom.type = RoomType.Boss;
        }
    }
}
