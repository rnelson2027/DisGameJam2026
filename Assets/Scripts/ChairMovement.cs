using UnityEngine;
using UnityEngine.InputSystem; // Make sure this is present

public class ChairMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f; // Speed of the chair movement

    private Rigidbody2D rb;
    private bool isMovingUp;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. Get the current active keyboard
        Keyboard keyboard = Keyboard.current;

        // 2. Safety check in case no keyboard is connected
        if (keyboard == null) return;

        // 3. Read W or Up Arrow inputs directly
        isMovingUp = keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed;
    }

    void FixedUpdate()
    {
        if (isMovingUp)
        {
            // Move strictly along the object's local upward vector
            rb.linearVelocity = (Vector2)transform.up * speed;
        }
        else
        {
            // Stop movement when key is released
            rb.linearVelocity = Vector2.zero;
        }
    }
}