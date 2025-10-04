using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine.Rendering.UI;

public class GridData : MonoBehaviour
{
    Dictionary<Vector3Int, PlacementData> placedObjects = new();
    [SerializeField]
    private List<Tilemap> moduleTilemaps;
    public void AddObjectAt(Vector3Int gridPosition, Vector2Int objectSize,
        int ID, int placeObjectIndex)
    {
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
        PlacementData newData = new PlacementData(ID, placeObjectIndex, positionToOccupy);
        foreach (var pos in positionToOccupy)
        {
            if (placedObjects.ContainsKey(pos))
            {
                Debug.LogError("Position " + pos + " is already occupied.");
            }
            placedObjects[pos] = newData;
        }
    }

    private List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> positions = new List<Vector3Int>();
        for (int x = 0; x < objectSize.x; ++x)
        {
            for (int y = 0; y < objectSize.y; ++y)
            {
                positions.Add(new Vector3Int(gridPosition.x + x, gridPosition.y + y, gridPosition.z));
            }
        }
        return positions;
    }
    public bool IsPositionOnTilemap(List<Vector3Int> gridPositions)
    {
        foreach (var pos in gridPositions)
        {
            foreach (var tilemap in moduleTilemaps)
            {
                Vector3 world = tilemap.layoutGrid.CellToWorld(pos);
                Vector3Int localCell = tilemap.WorldToCell(world);
                if (tilemap != null && tilemap.HasTile(localCell) == true)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public bool IsPositionOccupied(Vector3Int gridPosition, Vector2Int objectSize)
    {
        // if the position is not on the tilemap, return true
        List<Vector3Int> positionsToCheck = CalculatePositions(gridPosition, objectSize);
        foreach (var pos in positionsToCheck)
        {
            if (placedObjects.ContainsKey(pos))
            {
                return true;
            }
        }
        if (!IsPositionOnTilemap(positionsToCheck)) return true;
        return false;
    }

    void Start()
    {
        // if (moduleTilemaps.Count == 0)
        // {
        //     Debug.LogError("Module Tilemaps are not assigned.");
        // }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
public class PlacementData
{
    public PlacementData(int id, int placeObjectIndex, List<Vector3Int> occupiedPositions)
    {
        ID = id;
        PlaceObjectIndex = placeObjectIndex;
    }
    public List<Vector3Int> occupiedPositions;
    public int ID { get; private set; }
    public int PlaceObjectIndex { get; private set; }
}
