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
    private Vector2 mousePosition;
    public event Action OnClicked, OnExit;
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
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

