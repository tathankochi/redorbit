using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private LayerMask placementLayerMask;
    public GameObject mouseIndicator;
    public event Action OnClicked, OnExit;
    void Start()
    {
        mainCamera = Camera.main;
        Debug.Log("Placement Layer Mask: " + placementLayerMask.value);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseScreenPosition = Mouse.current.position.ReadValue();
        Debug.Log("Mouse Position: " + mouseScreenPosition);
        Ray ray = mainCamera.ScreenPointToRay(mouseScreenPosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, placementLayerMask))
        {
            Debug.Log("Hit Point: " + hitInfo.point);
            mouseIndicator.transform.position = hitInfo.point;
        }
        if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                // Debug.Log("Left mouse button clicked");
                OnClicked?.Invoke();
            }
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            // Debug.Log("Right mouse button clicked");
            OnExit?.Invoke();
        }
    }
    public bool IsPointedOverUI() => EventSystem.current.IsPointerOverGameObject();
    public Vector2 GetSelectedMapPosition()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, placementLayerMask))
        {
            mousePosition = hitInfo.point;
            return mousePosition;
        }
        return Vector2.zero;
    }
}

