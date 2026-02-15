using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardManager : MonoBehaviour
{
    private Tilemap m_Tilemap;

    public int Width;
    public int Height;
    public Tile[] GroundFiles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Tilemap = GetComponentInChildren<Tilemap>();

        if (m_Tilemap == null)
        {
            Debug.LogError("Tilemap component not found in children.");
            return;
        }

        if (GroundFiles == null || GroundFiles.Length == 0)
        {
            Debug.LogError("GroundFiles not assigned or empty.");
            return;
        }

        for (int y = 0; y < Height; ++y)
        {
            for (int x = 0; x < Width; ++x)
            {
                int tileNumber = Random.Range(0, GroundFiles.Length);
                m_Tilemap.SetTile(new Vector3Int(x, y, 0), GroundFiles[tileNumber]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
