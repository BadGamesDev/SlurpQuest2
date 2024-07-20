using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class OverworldUI : MonoBehaviour //just combining the UI scripts might simply everything tbh
{
    public PlayerStats playerStats;
    public GameObject dialoguePanel;
    public GameObject partyScreen;

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
    }

    public void PartyScreenDoneButtonPressed() //what a terrible name lmao
    {
        partyScreen.SetActive(false);
    }

    public void CompanionsButton0Pressed()
    {
        pickedCompanion = FindCompanionByName("Honey"); //there are no checks to see if the companion exist because the button is uninteractable if the companion does not exist
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

    private CompanionData FindCompanionByName(string companionName) //Having a method like this here feels bad, honestly this whole thing feels retarded.
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
}