using UnityEngine;

public class PreviewSystem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject previewObject;
    public GameObject cellIndicator;
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
        // get color of the sprite renderer in children
        SpriteRenderer[] spriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = defaultColor;
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
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
