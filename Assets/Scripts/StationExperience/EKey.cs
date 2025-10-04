using UnityEngine;

public class EKey : MonoBehaviour
{
    private bool isEnabled = false;
    private SpriteRenderer _spriteRenderer;
    public static EKey Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        _spriteRenderer = GetComponent<SpriteRenderer>();
        Color color = _spriteRenderer.color;
        color.a = isEnabled ? 1f : 0f;
        _spriteRenderer.color = color;
    }
    public void SetEnable(bool enable, Vector2 position)
    {
        if (_spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer is null. Cannot change color or position.");
            return;
        }
        isEnabled = enable;
        Color color = _spriteRenderer.color;
        color.a = enable ? 1f : 0f;
        _spriteRenderer.color = color;
        if (enable)
        {

            transform.position = position;
        }
        else
        {

        }
    }
}