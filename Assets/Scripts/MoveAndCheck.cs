using UnityEngine;
using UnityEngine.InputSystem;


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
    private bool hasFinished; 

    private InputAction spaceBarAction;

    void Start()
    {
        if (chair == null || person == null)
        {
            Debug.LogError("Chair or Person reference is not assigned in the inspector.");
            return;
        }
        spaceBarAction = InputSystem.actions.FindAction("Jump");
    }

    void Update()
    {
        // Skip input handling once the release check has completed
        if (hasFinished) return;

        if (spaceBarAction.IsPressed())
        {
            chair.transform.position += new Vector3(0, speed*Time.deltaTime, 0);
            Debug.Log(transform.position.y);
        }

        if (spaceBarAction.WasReleasedThisFrame())
        {
            hasFinished = true; // Lock execution so it runs only once

            diff = chair.referencePoint.position.y - person.referencePoint.position.y;
            Debug.Log($"Difference in Y position: {diff}"); //make pretty and print with a layer over top to hide prep for next play, make score percentage based on diff and print to screen

            // Call your scene change or pass the score here:
            // SceneManager.LoadScene("PostGameplayScene");
        }
    }

    // void FixedUpdate()
    // {
    //     MoveChair();
    // }

    // void MoveChair()
    // {
    //     if (chairRb == null) return;

    //     if (isMovingUp)
    //     {
    //         chairRb.linearVelocity = new Vector2(0f, speed);
    //     }
    //     else
    //     {
    //         chairRb.linearVelocity = Vector2.zero;
    //     }
    // }
}