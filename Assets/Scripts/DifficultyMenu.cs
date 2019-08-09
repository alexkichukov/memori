using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyMenu : MonoBehaviour
{
    public void PlayEasy()
    {
        SceneManager.LoadScene("Easy");
    }

    public void PlayMedium()
    {
        SceneManager.LoadScene("Medium");
    }

    public void PlayHard()
    {
        SceneManager.LoadScene("Hard");
    }
}
