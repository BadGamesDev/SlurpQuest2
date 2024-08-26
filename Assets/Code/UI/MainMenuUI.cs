using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject tutorialScreen;

    public void StartButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void TutorialButton()
    {
        tutorialScreen.SetActive(true);
    }

    public void CloseTutorialButton()
    {
        tutorialScreen.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
