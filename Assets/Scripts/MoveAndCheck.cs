using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; // Added for scene loading

public class MoveAndCheck : MonoBehaviour
{
    [Header("Target References")]
    [Tooltip("Drag your visible Chair GameObject here.")]
    [SerializeField] Chair chair;
    [SerializeField] Person person;

    [Header("Movement Settings")]
    public float speed = 5f; 

    private bool isMovingUp;
    private float diff;
    private Rigidbody2D chairRb;
    private bool hasFinished; // Prevents triggering scene change multiple times

    void Start()
    {
        if (chair == null || person == null)
        {
            Debug.LogError("Chair or Person reference is not assigned in the inspector.");
            return;
        }
        chairRb = chair.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Skip input handling once the release check has completed
        if (hasFinished) return;

        var keyboard = Keyboard.current;
        if (keyboard == null)
        {
            Debug.LogError("Keyboard input is not available.");
            return;
        }

        // 1. Set movement state while held
        isMovingUp = keyboard.spaceKey.isPressed;

        // 2. ONLY run on the single frame Space is RELEASED
        if (keyboard.spaceKey.wasReleasedThisFrame)
        {
            hasFinished = true; // Lock execution so it runs only once
            isMovingUp = false;

            diff = chair.referencePoint.position.y - person.referencePoint.position.y;
            Debug.Log($"Difference in Y position: {diff}"); //make pretty and print with a layer over top to hide prep for next play, make score percentage based on diff and print to screen

            // Call your scene change or pass the score here:
            // SceneManager.LoadScene("PostGameplayScene");
        }
    }

    void FixedUpdate()
    {
        MoveChair();
    }

    void MoveChair()
    {
        if (chairRb == null) return;

        if (isMovingUp)
        {
            chairRb.linearVelocity = new Vector2(0f, speed);
        }
        else
        {
            chairRb.linearVelocity = Vector2.zero;
        }
    }
}