using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class OverworldUI : MonoBehaviour //just combining the UI scripts might simply everything tbh
{
    public GameState gameState;
    public PlayerStats playerStats;
    public GameObject dialoguePanel;
    public GameObject partyScreen;

    public TMP_Text dialogueText;

    public Button partyButton;

    public Button companionsButton0;
    public Button companionsButton1;
    public Button companionsButton2;
    public Button companionsButton3;
    public Button companionsButton4;
    public Button companionsButton5;

    public Button partyScreenDoneButton;

    public CompanionData pickedCompanion; //I guess this is the best place to put this? Oh also, I should probably use standardised variable names right? LMAO CHAOS WILL REIGN SUPREME

    public void PartyButtonPressed()
    {
        partyScreen.SetActive(true);
        CheckSelectableCompanions();
    }

    public void PartyScreenDoneButtonPressed() //what a terrible name lmao
    {
        partyScreen.SetActive(false);
    }

    public void CompanionsButton0Pressed()
    {
        pickedCompanion = FindUnlockedCompanionByName("Honey"); //there are no checks to see if the companion exist because the button is uninteractable if the companion does not exist
    }

    public void CompanionsButton1Pressed()
    {

    }

    public void CompanionsButton2Pressed()
    {

    }

    public void CompanionsButton3Pressed()
    {

    }

    public void CompanionsButton4Pressed()
    {

    }

    public void CompanionsButton5Pressed()
    {

    }

    public void CheckSelectableCompanions()
    {
        companionsButton0.interactable = false;
        companionsButton1.interactable = false;
        companionsButton2.interactable = false;
        companionsButton3.interactable = false;
        companionsButton4.interactable = false;
        companionsButton5.interactable = false;

        if(FindUnlockedCompanionByName("Honey") != null && FindActiveCompanionByName("Honey") == null)
        {
            companionsButton0.interactable = true;
        }
    }

    public void DisplayMessage(string message)
    {
        gameState.globalPaused = true;
        dialoguePanel.SetActive(true);
        dialogueText.text = message;
    }

    public void CloseMessageButtonPressed()
    {
        gameState.globalPaused = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = null;
    }

    private CompanionData FindUnlockedCompanionByName(string companionName) //Having a method like this here feels bad, honestly this whole thing feels retarded.
    {                                                       //I should probably do the whole "adding and dismissing companions from your party" thing from scratch but at this point I really can't be bothered.
        foreach (CompanionData companion in playerStats.unlockedCompanions) //It has been revealed to me in a dream that this method should be moved to the PlayerStats script.
        {
            if (companion.characterName == companionName)
            {
                return companion;
            }
        }
        return null;
    }

    private CompanionData FindActiveCompanionByName(string companionName) //Having a method like this here feels bad, honestly this whole thing feels retarded.
    {                                                       //I should probably do the whole "adding and dismissing companions from your party" thing from scratch but at this point I really can't be bothered.
        foreach (CompanionData companion in playerStats.activeCompanions) //It has been revealed to me in a dream that this method should be moved to the PlayerStats script.
        {
            if (companion.characterName == companionName)
            {
                return companion;
            }
        }
        return null;
    }
}