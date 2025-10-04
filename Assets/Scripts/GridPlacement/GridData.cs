using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class GridData : MonoBehaviour
{
    Dictionary<Vector3Int, PlacementData> placedObjects = new();
    [SerializeField] Tilemap tilemap;
    private List<Tilemap> moduleTilemaps;
    public void AddObjectAt(Vector3Int gridPosition, Vector2Int objectSize,
        int ID, int placeObjectIndex)
    {
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
        PlacementData newData = new PlacementData(ID, placeObjectIndex, positionToOccupy);
    }

    private List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2Int objectSize)
    {
        throw new NotImplementedException();
    }

    void Start()
    {

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
