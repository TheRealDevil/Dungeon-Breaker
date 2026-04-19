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
    public int numberOfRooms = 10;

    //ukláda kompletní blueprint mapy
    private List<RoomData> dungeonLayout = new List<RoomData>();

    void Start()
    {
        GenerateBlueprint();
    }

    void GenerateBlueprint()
    {
        dungeonLayout.Clear();
        Vector2Int currentPos = Vector2Int.zero;

        //startovní roomka na 0.0
        dungeonLayout.Add(new RoomData(currentPos, RoomType.Start));

        //random tvorba místností
        int failedAttempts = 0;
        const int maxFailedAttempts = 100;
        while (dungeonLayout.Count < numberOfRooms && failedAttempts < maxFailedAttempts)
        {
            currentPos += GetRandomDirection();

            //kontrola jestli tam už něco je
            if (!IsPositionOccupied(currentPos))
            {
                dungeonLayout.Add(new RoomData(currentPos, RoomType.Enemy));
                failedAttempts = 0;
            }
            else
            {
                failedAttempts++;
            }
        }

        //generace boss roomky
        SetBossRoom();

        //debug vizulizace
        foreach (var room in dungeonLayout)
        {
            Debug.Log($"Místnost na {room.gridPos} je {room.type}");
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

        //boss místnost má být nejdál (ale ne Start roomka)
        foreach (var room in dungeonLayout)
        {
            if (room.type == RoomType.Start) continue; // Skip Start room
            
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
