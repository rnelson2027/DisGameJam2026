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
    [SerializeField] float outcomeThreshold = 10f;

    private int currentRound;
    private float totalDisparity;
    private float disparity;

    // Update is called once per frame

    void Start()
    {
        NewRound();
    }

    private void NewRound()
    {
        currentRound++;

        if (roundText != null)
        {
            roundText.text = "Round " + currentRound + " / " + maxRounds;
        }

        if (disparityText != null)
        {
            disparityText.text = "";
        }

        StartCoroutine(RoundTimer());
    }

    private IEnumerator RoundTimer()
    {
        yield return new WaitForSeconds(roundTime);
        // sprite sits down
        CalculateDisparity();

        totalDisparity += disparity;

        if (disparityText != null)
        {
        disparityText.text = "Disparity: " + disparity.ToString("F2");
        }

        yield return new WaitForSeconds(2f);

        if (currentRound < maxRounds)
        {
            yield return StartCoroutine(WaitForSpace());
            person.transform.position = new Vector2(person.transform.position.x, Random.Range(3f, 10f));
            NewRound();
        }
        else
        {
            /// add end game options
        }
    }

    private IEnumerator WaitForSpace()
    {
        yield return new WaitUntil(() => Keyboard.current != null && Keyboard.current.spaceKey.IsPressed());
        yield return new WaitUntil(() => Keyboard.current != null && !Keyboard.current.spaceKey.IsPressed());
    }
    
    private void CalculateDisparity()
    {
        disparity = Mathf.Abs(chair.referencePoint.position.y - person.referencePoint.position.y);
    }

}
