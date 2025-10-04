using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Quản lý việc phát hiện và xử lý tương tác với object trong game
/// Sử dụng trigger collider để phát hiện object có thể tương tác
/// Hiển thị icon E khi có object trong phạm vi
/// </summary>
public class InteractionManager : MonoBehaviour
{
    /// <summary>
    /// Danh sách các object có thể tương tác trong phạm vi
    /// Chỉ tương tác với object đầu tiên trong danh sách
    /// </summary>
    private List<IInteractable> _interactablesInRange = new List<IInteractable>();
    
    /// <summary>
    /// Xử lý input phím E để tương tác với object
    /// Chỉ tương tác với object đầu tiên trong danh sách
    /// Tự động loại bỏ object không thể tương tác
    /// </summary>
    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame
            && _interactablesInRange.Count > 0)
            {
                var interactable = _interactablesInRange[0];
                interactable.Interact();
                if (!interactable.IsInteractable())
                {
                    _interactablesInRange.Remove(interactable);
                }

            }
    }
    
    void Start()
    {

    }
    
    /// <summary>
    /// Phát hiện object có IInteractable khi player vào phạm vi
    /// Thêm vào danh sách và hiển thị icon E
    /// </summary>
    /// <param name="collision">Collider của object được phát hiện</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        var interactable = collision.GetComponent<IInteractable>();
        if (interactable != null && interactable.IsInteractable())
        {
            _interactablesInRange.Add(interactable);
            EKey.Instance.SetEnable(true, collision.transform.position);

        }
    }
    
    /// <summary>
    /// Loại bỏ object khỏi danh sách khi player rời khỏi phạm vi
    /// Ẩn icon E
    /// </summary>
    /// <param name="collision">Collider của object rời khỏi phạm vi</param>
    void OnTriggerExit2D(Collider2D collision)
    {
        var interactable = collision.GetComponent<IInteractable>();
        if (interactable != null)
        {
            _interactablesInRange.Remove(interactable);
            EKey.Instance.SetEnable(false, Vector2.zero);
        }
    }
}
