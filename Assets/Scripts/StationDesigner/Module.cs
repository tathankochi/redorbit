using UnityEngine;
using UnityEngine.Tilemaps;

public class Module : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool isDragged = false;
    public bool isKinematic = false;
    private Vector3 offset;
    public Tilemap moduleTilemap;
    void Start()
    {
        if (moduleTilemap == null)
        {
            Debug.LogError("Module Tilemap is not assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDragged) return;
        if (isKinematic) return;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector3 oldPos = transform.position;
        transform.position = new Vector3(mousePos.x - offset.x, mousePos.y - offset.y, 0);
        Vector3 moveAmount = transform.position - oldPos;
        moduleTilemap.transform.position += moveAmount;
        offset = mousePos - transform.position;
    }
    public void moveTilemap(Vector3 moveAmount)
    {
        moduleTilemap.transform.position += moveAmount;
    }
    void OnMouseDown()
    {
        isDragged = true;
        offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }
    void OnMouseUp()
    {
        isDragged = false;
    }
}
