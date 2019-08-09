using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{   
    // Public
    public Text TimerText;
    public float InitialTimeLeft = 20.0f;
    public float TimeMultiplier = 1.0f;

    // Private
    private float TimeLeft;
    private CardController Controller;

    // Start is called before the first frame update
    void Start()
    {
        TimeLeft = InitialTimeLeft;
        Controller = GameObject.Find("CardController").GetComponent<CardController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeLeft > 0.0f)
        {
            TimerText.text = TimeLeft.ToString("0.00"); // Update the time displayed
            TimeLeft -= Time.deltaTime; // Decrease the time by the time passed since last frame
        }
        else
        {
            TimerText.text = "0.00"; // Null the time
            // Time is over, game should end
            EndGame();
        }
    }

    // Adds additional time
    public void AddTime(float Time)
    {
        TimeLeft += Time * TimeMultiplier;
    }

    public void EndGame()
    {
        StartCoroutine(Controller.EndGame());
    }
}
