using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 velocity = Vector2.zero;
    public float acceleration = 3f;
    public float maxSpeed = 5f;
    public float friction = 3f;
    public float rotationSpeed = 60f;
    [SerializeField] private Animator animator;
    private Rigidbody2D _rigidbody2D;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.gravityScale = 0;
        _rigidbody2D.angularDamping = 0;
        _rigidbody2D.linearDamping = 0;
        _rigidbody2D.freezeRotation = true;
    }

    void Update()
    {
        float rotationInput = 0f;
        if (Keyboard.current.leftArrowKey.isPressed)
            rotationInput = 1f;
        if (Keyboard.current.rightArrowKey.isPressed)
            rotationInput = -1f;

        transform.Rotate(Vector3.forward, rotationInput * rotationSpeed * Time.deltaTime);

        Vector2 moveInput = Vector2.zero;
        if (Keyboard.current.upArrowKey.isPressed)
            moveInput.y = 1;
        if (Keyboard.current.downArrowKey.isPressed)
            moveInput.y = -1;

        moveInput = moveInput.normalized;

        Vector2 forward = transform.up;
        velocity = _rigidbody2D.linearVelocity;

        velocity += forward * moveInput.y * acceleration * Time.deltaTime;

        if (moveInput.y == 0)
            velocity = Vector2.MoveTowards(velocity, Vector2.zero, friction * Time.deltaTime);

        velocity = Vector2.ClampMagnitude(velocity, maxSpeed);

        _rigidbody2D.linearVelocity = velocity;

        animator.SetBool("isMoving", moveInput.y != 0);
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
