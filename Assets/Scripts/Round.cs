using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;


// all  code in this file is conceptual and does not rely on the implementation of other code yet
public class Round : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] Chair chair;
    [SerializeField] Person person;

    [Header("UI")]
    [SerializeField] TMP_Text disparityText;
    [SerializeField] TMP_Text roundText;

    [Header("Round Settings")]
    [SerializeField] float roundTime = 10f;
    [SerializeField] int maxRounds = 10; 
    //[SerializeField] float outcomeThreshold = 10f;

    private int currentRound;
    private float totalDisparity;
    private float disparity;

    private Vector2 chairOriginalPosition;

    private InputAction spaceBarAction;

    private bool roundFinished;

    // Update is called once per frame

    void Start()
    {
        chairOriginalPosition = chair.transform.position;

        spaceBarAction = InputSystem.actions.FindAction("Jump");
        UpdateRoundText();

    }

    void Update()
    {
        // Round.cs does NOT move the chair.
        // MoveAndCheck still handles that.

        // We only care about the Space RELEASE.
        if (!roundFinished && spaceBarAction.WasReleasedThisFrame())
        {
            roundFinished = true;

            StartCoroutine(FinishRound());
        }
    }

    private void NewRound()
    {
        currentRound++;

        chair.transform.position = chairOriginalPosition;

        person.transfor.position = new Vector2(person.transform.position.x, Random.Range(3f, 10f));

        if (roundText != null)
        {
            roundText.text = "Round " + currentRound + " / " + maxRounds;
        }

        if (disparityText != null)
        {
            disparityText.text = "";
        }

        UpdateRoundText();

        roundFinished = false;
    }


    private IEnumerator FinishRound()
    {
        // NOT IMPLEMENTED YET:
        // Make the person sprite sit down here.


        // Calculate how accurate the chair placement was
        CalculateDisparity();

        // Add this round to the total
        totalDisparity += disparity;


        // Show this round's result
        if (disparityText != null)
        {
            disparityText.text =
                "Disparity: " + disparity.ToString("F2");
        }


        // Keep the score visible for 2 seconds
        yield return new WaitForSeconds(2f);


        if (currentRound < maxRounds)
        {
            NewRound();
        }
        else
        {
            EndGame();
        }
    }

    private void CalculateDisparity()
    {
        disparity = Mathf.Abs(chair.referencePoint.position.y - person.referencePoint.position.y);
    }

    private void UpdateRoundText()
    {
        if (roundText != null)
        {
            roundText.text =
                "Round " + currentRound + " / " + maxRounds;
        }
    }


}
