using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    public GameObject tutorialScreen;
    public Button exitButton;

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
        TextMeshProUGUI textObject = exitButton.GetComponentInChildren<TextMeshProUGUI>();
        textObject.text = "SIKE!";
    }
}
