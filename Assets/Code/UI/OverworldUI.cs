using System.Collections.Generic;
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
    public GameObject menuScreen;
    public GameObject itemsScreen;

    public TMP_Text dialogueText;
    public List<string> textQueue;

    public Button menuButton;
    public Button partyButton;
    public Button itemsButton;

    public Button companionsButton0;
    public Button companionsButton1;
    public Button companionsButton2;
    public Button companionsButton3;
    public Button companionsButton4;
    public Button companionsButton5;

    public Button dismissCompanion0;
    public Button dismissCompanion1;

    public TMP_Text nameText;
    public TMP_Text levelText;
    public TMP_Text healthText;
    public TMP_Text defenceText;
    public TMP_Text dodgeText;
    public TMP_Text damageText;
    public TMP_Text accuracyText;
    public TMP_Text speedText;

    public TMP_Text newLevelText;
    public TMP_Text newHealthText;
    public TMP_Text newDefenceText;
    public TMP_Text newDodgeText;
    public TMP_Text newDamageText;
    public TMP_Text newAccuracyText;
    public TMP_Text newSpeedText;

    public GameObject skill0Panel;
    public GameObject skill1Panel;
    public GameObject skill2Panel;
    public GameObject skill3Panel;
    public GameObject skill4Panel;

    public Image skill0Icon;
    public Image skill1Icon;
    public Image skill2Icon;
    public Image skill3Icon;
    public Image skill4Icon;

    public TMP_Text skill0Name;
    public TMP_Text skill1Name;
    public TMP_Text skill2Name;
    public TMP_Text skill3Name;
    public TMP_Text skill4Name;

    public TMP_Text skill0Desc;
    public TMP_Text skill1Desc;
    public TMP_Text skill2Desc;
    public TMP_Text skill3Desc;
    public TMP_Text skill4Desc;

    public GameObject statsScreen;
    public Button levelUpButton;
    public Button exitStatScreenButton;
    public Button slurpStatButton;
    public Button statButton0;
    public Button statButton1;

    public Button menuScreenDoneButton;
    public Button partyScreenDoneButton;
    public Button itemsScreenDoneBUtton;

    public CompanionData statsCompanion; 
    public CompanionData pickedCompanion; //I guess this is the best place to put this? Oh also, I should probably use standardised variable names right? LMAO CHAOS WILL REIGN SUPREME

    public void MenuButtonPressed()
    {
        menuScreen.SetActive(true);
    }

    public void CloseMenuButtonPressed()
    {
        menuScreen.SetActive(false);
    }

    public void PartyButtonPressed()
    {
        partyScreen.SetActive(true);
        CheckSelectableCompanions();
    }

    public void PartyScreenDoneButtonPressed() //what a terrible name lmao
    {
        partyScreen.SetActive(false);
    }

    public void ItemsButtonPressed()
    {
        itemsScreen.SetActive(false);
    }

    public void CloseItemsButtonPressed()
    {
        itemsScreen.SetActive(false);
    }

    public void CompanionsButton0Pressed()
    {
        pickedCompanion = FindUnlockedCompanionByName("Honey"); //there are no checks to see if the companion exist because the button is uninteractable if the companion does not exist
    }

    public void CompanionsButton1Pressed()
    {
        pickedCompanion = FindUnlockedCompanionByName("Digi63");
    }

    public void CompanionsButton2Pressed()
    {
        pickedCompanion = FindUnlockedCompanionByName("Jaydizz");
    }

    public void CompanionsButton3Pressed()
    {
        pickedCompanion = FindUnlockedCompanionByName("Cndk99");
    }

    public void CompanionsButton4Pressed()
    {
        pickedCompanion = FindUnlockedCompanionByName("OneViolence");
    }

    public void CompanionsButton5Pressed()
    {

    }

    public void StatButtonSlurpPressed()
    {
        statsCompanion = FindUnlockedCompanionByName("Slurp");
        OpenStatScreen();
    }

    public void StatButton0Pressed()
    {
        statsCompanion = FindActiveCompanionByName(playerParty.pos2.GetComponent<CharacterData>().characterName);
        OpenStatScreen();
    }

    public void StatButton1Pressed()
    {
        statsCompanion = FindActiveCompanionByName(playerParty.pos3.GetComponent<CharacterData>().characterName);
        OpenStatScreen();
    }

    public void OpenStatScreen() //I can just make this method take a variable instead of having a variable on the script. But I'm too tired to change it right now.
    {
        statsScreen.SetActive(true);

        nameText.text = "Name: " + statsCompanion.characterName;
        levelText.text = "Level: " + (statsCompanion.level + 1).ToString();
        healthText.text = "Health: " + statsCompanion.health.ToString();
        defenceText.text = "Defence: " + statsCompanion.defence.ToString();
        dodgeText.text = "Dodge: " + statsCompanion.dodge.ToString();
        damageText.text = "Damage: " + statsCompanion.damage.ToString();
        accuracyText.text = "Accuracy: " + statsCompanion.accuracy.ToString();
        speedText.text = "Health: " + statsCompanion.speed.ToString();

        newLevelText.text = null;
        newHealthText.text = null;
        newDefenceText.text = null;
        newDodgeText.text = null;
        newDamageText.text = null;
        newAccuracyText.text = null;
        newSpeedText.text = null;

        if (statsCompanion.level < playerStats.level)
        {
            levelUpButton.gameObject.SetActive(true);
        }

        if (statsCompanion.skills.Count == 2) //I am actually really ashamed of this part
        {
            skill0Panel.SetActive(true);
            skill0Name.text = statsCompanion.skills[0].skillName;
            skill0Desc.text = statsCompanion.skills[0].skillDesc;
            
            skill1Panel.SetActive(true);
            skill0Name.text = statsCompanion.skills[0].skillName;
            skill0Desc.text = statsCompanion.skills[0].skillDesc;
        }

        else if (statsCompanion.skills.Count == 3)
        {
            skill0Panel.SetActive(true);
            skill1Panel.SetActive(true);
            skill2Panel.SetActive(true);
            skill3Panel.SetActive(true);
        }

        else if (statsCompanion.skills.Count == 4)
        {
            skill0Panel.SetActive(true);
            skill1Panel.SetActive(true);
            skill2Panel.SetActive(true);
            skill3Panel.SetActive(true);
            skill4Panel.SetActive(true);
        }
    }

    public void LevelUpButtonPressed()
    {
        statsCompanion.LevelUp();
        
        if (statsCompanion.level >= playerStats.level)
        {
            levelUpButton.gameObject.SetActive(false);
        }

        newLevelText.text = (statsCompanion.level + 1).ToString();
        newHealthText.text = statsCompanion.health.ToString();
        newDefenceText.text = statsCompanion.defence.ToString();
        newDodgeText.text = statsCompanion.dodge.ToString();
        newDamageText.text = statsCompanion.damage.ToString();
        newAccuracyText.text = statsCompanion.accuracy.ToString();
        newSpeedText.text = statsCompanion.speed.ToString();
    }

    public void CloseStatScreenButtonPressed()
    {
        levelUpButton.gameObject.SetActive(false);
        statsScreen.SetActive(false);
    }

    public void DismissButton0Pressed()
    {
        CompanionData companionToRemove = FindActiveCompanionByName(playerParty.pos2.GetComponent<CharacterData>().characterName);
        playerStats.activeCompanions.Remove(companionToRemove);
        playerParty.pos2 = null; //I had to add a reference to player party just for this shit
        pickedCompanion = null; //there is no need for this but lets keep it for now
        CheckSelectableCompanions();
        statButton0.gameObject.SetActive(false);
        dismissCompanion0.gameObject.SetActive(false);
    }

    public void DissmissButton1Pressed()
    {
        CompanionData companionToRemove = FindActiveCompanionByName(playerParty.pos3.GetComponent<CharacterData>().characterName);
        playerStats.activeCompanions.Remove(companionToRemove);
        playerParty.pos3 = null;
        pickedCompanion = null;
        CheckSelectableCompanions();
        statButton1.gameObject.SetActive(false);
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

        if (FindUnlockedCompanionByName("Digi63") != null && FindActiveCompanionByName("Digi63") == null)
        {
            companionsButton1.interactable = true;
        }

        if (FindUnlockedCompanionByName("Jaydizz") != null && FindActiveCompanionByName("Jaydizz") == null)
        {
            companionsButton2.interactable = true;
        }

        if (FindUnlockedCompanionByName("Cndk99") != null && FindActiveCompanionByName("Cndk99") == null)
        {
            companionsButton3.interactable = true;
        }

        if (FindUnlockedCompanionByName("OneViolence") != null && FindActiveCompanionByName("OneViolence") == null)
        {
            companionsButton4.interactable = true;
        }
    }

    public void DisplayMessages(string message)
    {
        gameState.globalPaused = true;
        dialoguePanel.SetActive(true);
        dialogueText.text = message;
    }

    public void AddMessage(string message)
    {
        if (!dialoguePanel.activeSelf)
        {
            DisplayMessages(message);
        }
        else
        {
            textQueue.Add(message);
        }
    }

    public void CloseMessageButtonPressed()
    {
        if (textQueue.Count <= 0)
        {
            gameState.globalPaused = false;
            dialoguePanel.SetActive(false);
            dialogueText.text = null;

            if (gameState.waitingCombat == true)
            {
                FindAnyObjectByType<CombatManager>().StartCombat(gameState.partiesWaitingCombat[0], gameState.partiesWaitingCombat[1]);
                gameState.waitingCombat = false;
                gameState.partiesWaitingCombat.Clear();
            }
        }
        else 
        {
            DisplayMessages(textQueue[0]);
            textQueue.Remove(textQueue[0]); //is this the right way of doing it? Check it once you have internet connection !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }
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

    private CompanionData FindActiveCompanionByName(string companionName)
    {
        foreach (CompanionData companion in playerStats.activeCompanions)
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
        AddMessage("Hello Slurp! It seems you have died! This is to be expected considering your atrocious performance in the last game, and normally dying would have been a big problem. " +
                   "But this game is set in the SlurpQuest™ universe and you actually canonically have almost a hundred trillion lives left over from SlurpQuest 1 thanks to Don. " +
                   "So go ahead and die as much as you want!");
    }

    public void HoneyUnlockedMessage()
    {
        AddMessage("You have unlocked Honey as a companion! You can click the party button and assign her to one of your empty companion slots." +
                   "You will keep unlocking more companions as you play the game and save people from corruption.");
    }

    public void DigiUnlockedMessage()
    {
        AddMessage("It was about damn time you got a mod on your side! Digi63 is here to moderate the fuck out of your enemies once again!");
    }

    public void JaydizzUnlockedMessage()
    {
        AddMessage("Hell yeah! It is the man, the myth, the legend Jaydizz himself! Here to assist you on your quest!");
    }

    public void OneViolenceUnlockedMessage()
    {
        AddMessage("A powerful shaman from Daneland, OneViolence and his magical powers will surely be of great help against the lord of pestilence!");
    }
}