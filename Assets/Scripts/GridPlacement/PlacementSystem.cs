using System;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private GameObject cellIndicator, mouseIndicator;
    [SerializeField]
    private Grid grid;
    [SerializeField]
    private ObjectDatabaseSO objectDatabase;
    private int selectedObjectIndex = -1;
    [SerializeField]
    private GameObject gridVisualization;
    [SerializeField]
    private GridData funitureData;
    private Renderer previewRenderer;
    private List<GameObject> placedObjects = new();
    [SerializeField]
    private PreviewSystem preview;

    void Start()
    {
        StopPlacement();
        previewRenderer = cellIndicator.GetComponentInChildren<Renderer>();
    }

    private void StopPlacement()
    {
        // gridVisualization.SetActive(false);
        preview.StopShowingPlacementPreview();
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
        selectedObjectIndex = -1;
    }
    public void StartPlacement(int objectID)
    {
        selectedObjectIndex = objectDatabase.objectsData.FindIndex(data => data.ID == objectID);
        if (selectedObjectIndex < 0)
        {
            Debug.LogError("Object with ID " + objectID + " not found in database.");
            return;
        }
        else
        {
            Debug.Log("Starting placement for object: " + objectDatabase.objectsData[selectedObjectIndex].Name);
        }
        gridVisualization.SetActive(true);
        preview.StartShowingPlacementPreview(objectDatabase.objectsData[selectedObjectIndex].Prefab,
            objectDatabase.objectsData[selectedObjectIndex].Size);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    private void PlaceStructure()
    {
        if (inputManager.IsPointerOverUI())
        {
            // Debug.Log("Pointer is over UI, not placing structure.");
            return;
        }
        Vector2 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        bool PlacementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if (!PlacementValidity) return;

        GameObject newObject = Instantiate(objectDatabase.objectsData[selectedObjectIndex].Prefab);
        Debug.Log("Placing object at " + gridPosition);
        newObject.transform.position = grid.GetCellCenterWorld(gridPosition);
        placedObjects.Add(newObject);
        funitureData.AddObjectAt(gridPosition, objectDatabase.objectsData[selectedObjectIndex].Size,
            objectDatabase.objectsData[selectedObjectIndex].ID, placedObjects.Count - 1);
        preview.UpdatePosition(grid.GetCellCenterWorld(gridPosition), false);

    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        GridData gridData = funitureData;
        return !gridData.IsPositionOccupied(gridPosition, objectDatabase.objectsData[selectedObjectIndex].Size);
    }

    void Update()
    {
        if (selectedObjectIndex < 0) return;
        Vector2 mousePosition = inputManager.GetSelectedMapPosition();
        mouseIndicator.transform.position = (Vector3)mousePosition;
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        bool PlacementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        preview.UpdatePosition(grid.GetCellCenterWorld(gridPosition), PlacementValidity);

        cellIndicator.transform.position = grid.GetCellCenterWorld(gridPosition);
        cellIndicator.SetActive(true);

    }
}
