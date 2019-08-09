using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text ScoreText;
    public int ScoreMultiplier = 1;

    private int GameScore = 0;

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = GameScore.ToString();
    }

    public void AddScore(int AdditionalScore)
    {
        GameScore += AdditionalScore * ScoreMultiplier;
    }

    public int GetScore()
    {
        return GameScore;
    }
}
