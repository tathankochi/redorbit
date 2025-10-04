using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    private List<IInteractable> _interactablesInRange = new List<IInteractable>();
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
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger entered with " + collision.name);
        var interactable = collision.GetComponent<IInteractable>();
        if (interactable != null && interactable.IsInteractable())
        {
            _interactablesInRange.Add(interactable);
            EKey.Instance.SetEnable(true, collision.transform.position);

        }
    }
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
