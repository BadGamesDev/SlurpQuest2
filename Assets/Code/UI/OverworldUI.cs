using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class OverworldUI : MonoBehaviour //just combining the UI scripts might simply everything tbh
{
    public GameState gameState;
    public PlayerStats playerStats;
    public PartyData playerParty;
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

    public Button dismissCompanion0;
    public Button dismissCompanion1;

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

    public void DismissButton0Pressed()
    {
        CompanionData companionToRemove = FindActiveCompanionByName(playerParty.pos2.GetComponent<CharacterData>().characterName);
        playerStats.activeCompanions.Remove(companionToRemove);
        playerParty.pos2 = null; //I had to add a reference to player party just for this shit
        pickedCompanion = null; //there is no need for this but lets keep it for now
        CheckSelectableCompanions();
        dismissCompanion0.gameObject.SetActive(false);
    }

    public void DissmissButton1Pressed()
    {
        CompanionData companionToRemove = FindActiveCompanionByName(playerParty.pos3.GetComponent<CharacterData>().characterName);
        playerStats.activeCompanions.Remove(companionToRemove);
        playerParty.pos3 = null;
        pickedCompanion = null;
        CheckSelectableCompanions();
        dismissCompanion1.gameObject.SetActive(false);
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

    public void FirstDeathMessage()
    {
        DisplayMessage("Hello Slurp! It seems you have died! This is to be expected considering your atrocious performance in the last game, and normally dying would have been a big problem. " +
                       "But this game is set in the SlurpQuest™ universe and you actually canonically have almost a hundred trillion lives left over from SlurpQuest 1 thanks to Don. " +
                       "So go ahead and die as much as you want!");
    }

    public void HoneyUnlockedMessage()
    {
        DisplayMessage("You have unlocked Honey as a companion! You can click the party button and assign her to one of your empty companion slots." +
                       "You will keep unlocking more companions as you play the game and save people from corruption.");
    }
}