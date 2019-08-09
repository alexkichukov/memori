using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public Text GameOverText;

    public void ShowMenu()
    {
        int GameScore = GameObject.Find("Score").GetComponent<Score>().GetScore();
        GameOverText.text = $"YOU FINISHED THE GAME WITH A SCORE OF: {GameScore}";
        this.gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
