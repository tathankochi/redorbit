using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.EventSystems;
public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private LayerMask placementLayerMask;
    private Vector2 mousePosition;
    public event Action OnClicked, OnExit;
    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
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
    public bool IsPointerOverUI()
        => EventSystem.current.IsPointerOverGameObject();
    public Vector2 GetSelectedMapPosition()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Debug.Log("Mouse Position: " + mousePosition);
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, placementLayerMask))
        {
            mousePosition = hitInfo.point;
            return mousePosition;
        }

        return Vector2.zero;
        // return mousePosition;
    }
}