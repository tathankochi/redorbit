using System;
using TMPro;
using UnityEngine;

public class PreviewSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject cellIndicator;
    private GameObject previewObject;
    
    private Color defaultColor = new Color(1, 1, 1, 0.5f);
    private Color invalidColor = Color.red;
    private Color validColor;
    void Start()
    {
        cellIndicator.SetActive(false);
    }
    public void StartShowingPlacementPreview(GameObject prefab, Vector2Int size)
    {
        Debug.Log("Starting placement preview for prefab: " + prefab.name);
        Debug.Log("Size: " + size);
        if (previewObject != null)
        {
            Destroy(previewObject);
        }
        previewObject = Instantiate(prefab);
        PreparePreview(previewObject);
        PrepareCursor(size);
        cellIndicator.SetActive(true);
    }

    private void PreparePreview(GameObject gameObject)
    {
        // get all sprite renderers in the preview object and change their color
        SpriteRenderer[] spriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
        // Debug.Log("Found " + spriteRenderers.Length + " sprite renderers in preview object.");
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = validColor;
        }
    }

    private void PrepareCursor(Vector2Int size)
    {
        if (size.x > 0 || size.y > 0)
        {
            cellIndicator.transform.localScale = new Vector3(size.x, 1, size.y);
        }
        else
        {
            cellIndicator.transform.localScale = Vector3.one;
        }
    }
    public void StopShowingPlacementPreview()
    {
        if (previewObject != null)
        {
            Destroy(previewObject);
        }
        cellIndicator.SetActive(false);
    }
    public void UpdatePosition(Vector3 position, bool validity)
    {
        // Debug.Log("Updating preview position to: " + position + " with validity: " + validity);
        MovePreview(position);
        MoveCursor(position);
        ApplyFeedback(validity);
    }

    private void ApplyFeedback(bool validity)
    {
        if (validity)
        {
            validColor = defaultColor;
            PreparePreview(previewObject);
        }
        else
        {
            // Debug.Log("Invalid placement position");
            validColor = invalidColor;
            PreparePreview(previewObject);
        }
    }

    private void MoveCursor(Vector3 position)
    {
        cellIndicator.transform.position = position;
    }

    private void MovePreview(Vector3 position)
    {
        previewObject.transform.position = position;
    }

    void Update()
    {
        
    }
}
