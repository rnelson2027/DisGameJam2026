using UnityEngine;
using TMPro;
using System.Collections;


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

    private Vector2 chairOriginalPosition;
    private Vector2 personOriginalPosition;
    // Update is called once per frame

    void Start()
    {
        chairOriginalPosition = chair.transform.position;
        personOriginalPosition = person.transform.position;

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
        chair.transform.position = chairOriginalPosition;
        person.transform.position = new Vector2(person.transform.position.x, Random.Range(3f, 10f));

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
            NewRound();
        }
        else
        {
            /// add end game options
        }
    }
    
    private void CalculateDisparity()
    {
        disparity = Mathf.Abs(chair.referencePoint.position.y - person.referencePoint.position.y);
    }

}
