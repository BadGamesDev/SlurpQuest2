using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class OverworldUI : MonoBehaviour //just combining the UI scripts might simply everything tbh
{
    public GameState gameState;
    public PlayerStats playerStats;
    public ImageLoader imageLoader;
    public AudioManager audioManager;
    public PartyData playerParty;
    public GameObject dialoguePanel;
    public GameObject partyScreen;
    public GameObject menuScreen;
    public GameObject itemsScreen;
    public GameObject cheatScreen;

    public Button cheatON;

    public Button cheatOFF;

    public TMP_Text catFoodCount;
    public TMP_Text pizzaCount;
    public TMP_Text gamblingChipCount;
    public TMP_Text noLifePoints;

    public TMP_Text catFoodPrice;
    public TMP_Text pizzaPrice;
    public TMP_Text gamblingPrice;

    public Button buyCatFoodButton;
    public Button buyPizzaButton;
    public Button buyGamblingButton;

    public TMP_Text dialogueText;
    public List<string> textQueue;

    public Button menuButton;
    public Button partyButton;
    public Button itemsButton;
    public Button cheatButton;

    public Button continueButton;

    public Button companionsButton0;
    public Button companionsButton1;
    public Button companionsButton2;
    public Button companionsButton3;
    public Button companionsButton4;
    public Button companionsButton5;

    public Image portrait;

    public Image member1;
    public Image member2;
    public Image member3;

    public Button dismissCompanion0;
    public Button dismissCompanion1;

    public Button hankChoice1;
    public Button hankChoice2;

    public Button bluePill;
    public Button redPill;

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

    public Button levelUpIndicator;

    public Button menuScreenDoneButton;
    public Button partyScreenDoneButton;
    public Button itemsScreenDoneBUtton;

    public CompanionData statsCompanion; 
    public CompanionData pickedCompanion; //I guess this is the best place to put this? Oh also, I should probably use standardised variable names right? LMAO CHAOS WILL REIGN SUPREME

    public TMP_Text xpLevelText;
    public TMP_Text xpBarText;
    public Slider xpBar;

    public bool hankChoice;
    public bool pillChoice;
    public bool lesbiansTwo;
    public bool levelAvailable;

    public float cooldown;

    public TMP_InputField passwordField;

    public GameObject chad; //there are ways of doing this with a single fucking line
    public GameObject chad1;
    public GameObject chad2;
    public GameObject chad3;
    public GameObject chad4;
    public GameObject chad5;
    public GameObject chad6;
    public GameObject chad7;
    public GameObject chad8;
    public GameObject chad9;
    public GameObject chad10;
    public GameObject chad11;
    public GameObject chad12;
    public GameObject chad13;
    public GameObject chad14;
    public GameObject chad15;
    public GameObject chad16;
    public GameObject chad17;
    public GameObject chad18;
    public GameObject chad19;
    public GameObject chad20;
    public GameObject chad21;
    public GameObject chad22;
    public GameObject chad23;
    public GameObject chad24;
    public GameObject chad25;
    public GameObject chad26;
    public GameObject chad27;

    public GameObject realChad;
    public GameObject realChad1;
    public GameObject realChad3;
    public GameObject realChad4;
    public GameObject realChad5;
    public GameObject realChad6;
    public GameObject realChad7;
    public GameObject realChad8;
    public GameObject realChad9;
    public GameObject realChad10;
    public GameObject realChad11;

    public GameObject fuckingchest1;
    public GameObject fuckingchest2;
    public GameObject fuckingchest3;
    public GameObject fuckingchest4;
    public GameObject fuckingchest5;
    public GameObject fuckingchest6;
    public GameObject fuckingchest7;
    public GameObject fuckingchest8;
    public GameObject fuckingchest9;
    public GameObject fuckingchest10;
    public GameObject fuckingchest11;

    public GameObject bean;

    public Image morpheus;

    public int waitCount;
    public float waitTime;

    public void Update()
    {
        if (cooldown <= 0)
        {
            dialogueText.gameObject.SetActive(true);
            continueButton.gameObject.SetActive(true);
        }

        if (waitCount == 1)
        {
            waitTime -= Time.deltaTime;
            continueButton.interactable = false;
            continueButton.transform.GetComponentInChildren<TextMeshProUGUI>().text = ((int)waitTime).ToString();
            if(waitTime <= 0)
            {
                continueButton.transform.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
                continueButton.interactable = true;
            }
        }

        else
        {
            cooldown -= Time.deltaTime;
        }
    }

    public void LevelUpCheck() // Doesn't level up more than once if you suddenly get a billion xp. Do I care? No.
    {
        levelAvailable = false;
        foreach (CompanionData companion in playerStats.unlockedCompanions)
        {
            if (companion.level < playerStats.level)
            {
                levelAvailable = true;
            }
        }

        if (levelAvailable)
        { 
            levelUpIndicator.gameObject.SetActive(true);
        }
        else
        {
            levelUpIndicator.gameObject.SetActive(false);
        }
    }

    public void MenuButtonPressed()
    {
        gameState.globalPaused = true;
        menuScreen.SetActive(true);
    }

    public void CloseMenuButtonPressed()
    {
        gameState.globalPaused = false;
        menuScreen.SetActive(false);
    }

    public void PartyButtonPressed()
    {
        gameState.globalPaused = true;
        partyScreen.SetActive(true);
        
        if(playerParty.pos1 != null)
        {
            member1.sprite = playerParty.pos1.GetComponent<CharacterData>().avatar;
        }

        if (playerParty.pos2 != null)
        {
            member2.sprite = playerParty.pos2.GetComponent<CharacterData>().avatar;
        }

        if (playerParty.pos3 != null)
        {
            member3.sprite = playerParty.pos3.GetComponent<CharacterData>().avatar;
        }

        CheckSelectableCompanions();
    }

    public void PartyScreenDoneButtonPressed() //what a terrible name lmao
    {
        gameState.globalPaused = false;
        partyScreen.SetActive(false);
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

        if (statsCompanion.characterName == "Slurp")
        {
            portrait.sprite = imageLoader.slurpHead;
        }
        else if (statsCompanion.characterName == "Honey")
        {
            portrait.sprite = imageLoader.honeyHead;
        }
        else if(statsCompanion.characterName == "Digi63")
        {
            portrait.sprite = imageLoader.digiHead;
        }
        else if(statsCompanion.characterName == "Jaydizz")
        {
            portrait.sprite = imageLoader.jaydizzHead;
        }
        else if(statsCompanion.characterName == "Cndk99")
        {
            portrait.sprite = imageLoader.cndkHead;
        }
        else if(statsCompanion.characterName == "OneViolence")
        {
            portrait.sprite = imageLoader.oneviolenceHead;
        }

        nameText.text = statsCompanion.characterName;
        levelText.text = "Level: " + (statsCompanion.level + 1).ToString();
        healthText.text = "Health: " + statsCompanion.health.ToString();
        defenceText.text = "Defence: " + statsCompanion.defence.ToString();
        dodgeText.text = "Dodge: " + statsCompanion.dodge.ToString();
        damageText.text = "Damage: " + statsCompanion.damage.ToString();
        accuracyText.text = "Accuracy: " + statsCompanion.accuracy.ToString();
        speedText.text = "Speed: " + statsCompanion.speed.ToString();

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

        skill0Panel.SetActive(false);
        skill1Panel.SetActive(false);
        skill2Panel.SetActive(false);
        skill3Panel.SetActive(false);
        skill4Panel.SetActive(false);
        
        if (statsCompanion.skills.Count > 1)
        {
            skill0Panel.SetActive(true);
            skill0Name.text = statsCompanion.skills[0].skillName;
            skill0Desc.text = statsCompanion.skills[0].skillDesc;
            skill0Icon.sprite = statsCompanion.skills[0].skillIcon;

            skill1Panel.SetActive(true);
            skill1Name.text = statsCompanion.skills[1].skillName;
            skill1Desc.text = statsCompanion.skills[1].skillDesc;
            skill1Icon.sprite = statsCompanion.skills[1].skillIcon;
        }

        if (statsCompanion.skills.Count > 2)
        {
            skill2Panel.SetActive(true);
            skill2Name.text = statsCompanion.skills[2].skillName;
            skill2Desc.text = statsCompanion.skills[2].skillDesc;
            skill2Icon.sprite = statsCompanion.skills[2].skillIcon;
        }

        if (statsCompanion.skills.Count > 3)
        {
            skill3Panel.SetActive(true);
            skill3Name.text = statsCompanion.skills[3].skillName;
            skill3Desc.text = statsCompanion.skills[3].skillDesc;
            skill3Icon.sprite = statsCompanion.skills[3].skillIcon;
        }

        if (statsCompanion.skills.Count > 4)
        {
            skill4Panel.SetActive(true);
            skill4Name.text = statsCompanion.skills[4].skillName;
            skill4Desc.text = statsCompanion.skills[4].skillDesc;
            skill4Icon.sprite = statsCompanion.skills[4].skillIcon;
        }
    }

    public void OpenInventoryScreen() //same as above
    {
        gameState.globalPaused = true;
        itemsScreen.SetActive(true);
        CheckMoney();

        catFoodCount.text = "Cat Food: " + playerStats.catFood.ToString();
        pizzaCount.text = "5/5 Pizza: " + playerStats.pizza.ToString();
        gamblingChipCount.text = "Gambling Chip: " + playerStats.gamblingChip.ToString();
        noLifePoints.text = "Nolifepoints: " + playerStats.noLifePoints.ToString();

        catFoodPrice.text = "Buy(" + (100 + (50 * gameState.progress)).ToString() + " NLP)";
        pizzaPrice.text = "Buy(" + (200 + (100 * gameState.progress)).ToString() + " NLP)";
        gamblingPrice.text = "Buy(" + (200 + (100 * gameState.progress)).ToString() + " NLP)";
    }

    public void BuyCatFoodPressed()
    {
        playerStats.catFood += 1;
        playerStats.noLifePoints -= 100 + (gameState.progress * 50); //HELL YEAH! HARD CODE THAT BITCH! THIS IS HOW REAL MEN DO IT! NO GAY ASS VARIABLES!
        CheckMoney();
    }

    public void BuyPizzaPressed()
    {
        playerStats.pizza += 1;
        playerStats.noLifePoints -= 200 + (gameState.progress * 100);
        CheckMoney();
    }

    public void BuyChipPressed()
    {
        playerStats.gamblingChip += 1;
        playerStats.noLifePoints -= 200 + (gameState.progress * 100);
        CheckMoney();
    }

    public void CheckMoney()
    {
        catFoodCount.text = "Cat Food: " + playerStats.catFood.ToString();
        pizzaCount.text = "5/5 Pizza: " + playerStats.pizza.ToString();
        gamblingChipCount.text = "Gambling Chip: " + playerStats.gamblingChip.ToString();
        noLifePoints.text = "Nolifepoints: " + playerStats.noLifePoints.ToString();

        buyCatFoodButton.interactable = true;
        buyPizzaButton.interactable = true;
        buyGamblingButton.interactable = true;

        catFoodPrice.text = "Buy(" + (100 + (50 * gameState.progress)).ToString() + " NLP)";
        pizzaPrice.text = "Buy(" + (200 + (100 * gameState.progress)).ToString() + " NLP)";
        gamblingPrice.text = "Buy(" + (200 + (100 * gameState.progress)).ToString() + " NLP)";

        if (playerStats.noLifePoints < 100 + (gameState.progress * 50))
        {
            buyCatFoodButton.interactable = false;
            buyPizzaButton.interactable = false;
            buyGamblingButton.interactable = false;
        }
        else if(playerStats.noLifePoints < 200 + (gameState.progress * 100))
        {
            buyPizzaButton.interactable = false;
            buyGamblingButton.interactable = false;
        }
    }

    public void CloseInventoryScreen()
    {
        gameState.globalPaused = false;
        itemsScreen.SetActive(false);
    }

    public void LevelUpButtonPressed()
    {
        statsCompanion.LevelUp();
        LevelUpCheck();

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

        if (statsCompanion.level < playerStats.level) // I am kinda ashamed of this shit but it does no harm other than just taking up a lot of space
        {
            levelUpButton.gameObject.SetActive(true);
        }

        if (statsCompanion.skills.Count > 1)
        {
            skill0Panel.SetActive(true);
            skill0Name.text = statsCompanion.skills[0].skillName;
            skill0Desc.text = statsCompanion.skills[0].skillDesc;
            skill0Icon.sprite = statsCompanion.skills[0].skillIcon;

            skill1Panel.SetActive(true);
            skill1Name.text = statsCompanion.skills[1].skillName;
            skill1Desc.text = statsCompanion.skills[1].skillDesc;
            skill1Icon.sprite = statsCompanion.skills[1].skillIcon;
        }

        if (statsCompanion.skills.Count > 2)
        {
            skill2Panel.SetActive(true);
            skill2Name.text = statsCompanion.skills[2].skillName;
            skill2Desc.text = statsCompanion.skills[2].skillDesc;
            skill2Icon.sprite = statsCompanion.skills[2].skillIcon;
        }

        if (statsCompanion.skills.Count > 3)
        {
            skill3Panel.SetActive(true);
            skill3Name.text = statsCompanion.skills[3].skillName;
            skill3Desc.text = statsCompanion.skills[3].skillDesc;
            skill3Icon.sprite = statsCompanion.skills[3].skillIcon;
        }

        if (statsCompanion.skills.Count > 4)
        {
            skill4Panel.SetActive(true);
            skill4Name.text = statsCompanion.skills[4].skillName;
            skill4Desc.text = statsCompanion.skills[4].skillDesc;
            skill4Icon.sprite = statsCompanion.skills[4].skillIcon;
        }
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

        member2.sprite = null;
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

        member3.sprite = null;
    }

    public void HankChoice1Pressed()
    {
        dialogueText.text = null;
        hankChoice = false;
        hankChoice1.gameObject.SetActive(false);
        hankChoice2.gameObject.SetActive(false);
        cooldown = 10;
        dialogueText.text = "You can see that Hank has already made up his mind. He simply starts walking away without saying anything.";
        continueButton.gameObject.SetActive(false);
        dialogueText.gameObject.SetActive(false);

        audioManager.slurpLesbiansOne.Play();
        lesbiansTwo = true;
    }

    public void HankChoice2Pressed()
    {
        dialogueText.text = "I am sorry Slurp but the other choice is the funny one so I'm gonna need you to go back and pick the correct option.";
        hankChoice1.gameObject.SetActive(false);
        hankChoice2.gameObject.SetActive(false);
    }

    public void BluePillPressed()
    {
        pillChoice = false;
        dialogueText.text = "You have picked the blue pill!";
        bluePill.gameObject.SetActive(false);
        redPill.gameObject.SetActive(false);

        morpheus.gameObject.SetActive(false);

        fuckingchest1.gameObject.SetActive(false);
        fuckingchest2.gameObject.SetActive(false);
        fuckingchest3.gameObject.SetActive(false);
        fuckingchest4.gameObject.SetActive(false);
        fuckingchest5.gameObject.SetActive(false);
        fuckingchest6.gameObject.SetActive(false);
        fuckingchest7.gameObject.SetActive(false);
        fuckingchest8.gameObject.SetActive(false);
        fuckingchest9.gameObject.SetActive(false);
        fuckingchest10.gameObject.SetActive(false);
        fuckingchest11.gameObject.SetActive(false);

    }

    public void RedPillPressed()
    {
        pillChoice = false;
        dialogueText.text = "You have picked the red pill!";

        chad.gameObject.SetActive(false);
        chad1.gameObject.SetActive(false);
        chad2.gameObject.SetActive(false);
        chad3.gameObject.SetActive(false);
        chad4.gameObject.SetActive(false);
        chad5.gameObject.SetActive(false);
        chad6.gameObject.SetActive(false);
        chad7.gameObject.SetActive(false);
        chad8.gameObject.SetActive(false);
        chad9.gameObject.SetActive(false);
        chad10.gameObject.SetActive(false);
        chad11.gameObject.SetActive(false);
        chad12.gameObject.SetActive(false);
        chad13.gameObject.SetActive(false);
        chad14.gameObject.SetActive(false);
        chad15.gameObject.SetActive(false);
        chad16.gameObject.SetActive(false);
        chad17.gameObject.SetActive(false);
        chad18.gameObject.SetActive(false);
        chad19.gameObject.SetActive(false);
        chad20.gameObject.SetActive(false);
        chad21.gameObject.SetActive(false);
        chad22.gameObject.SetActive(false);
        chad23.gameObject.SetActive(false);
        chad24.gameObject.SetActive(false);
        chad25.gameObject.SetActive(false);
        chad26.gameObject.SetActive(false);
        chad27.gameObject.SetActive(false);

        realChad.gameObject.SetActive(true);
        realChad1.gameObject.SetActive(true);
        realChad3.gameObject.SetActive(true);
        realChad4.gameObject.SetActive(true);
        realChad5.gameObject.SetActive(true);
        realChad6.gameObject.SetActive(true);
        realChad7.gameObject.SetActive(true);
        realChad8.gameObject.SetActive(true);
        realChad9.gameObject.SetActive(true);
        realChad10.gameObject.SetActive(true);
        realChad11.gameObject.SetActive(true);

        bluePill.gameObject.SetActive(false);
        redPill.gameObject.SetActive(false);

        morpheus.gameObject.SetActive(false);
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
            companionsButton0.image.sprite = imageLoader.honeyHead;
        }

        if (FindUnlockedCompanionByName("Digi63") != null && FindActiveCompanionByName("Digi63") == null)
        {
            companionsButton1.interactable = true;
            companionsButton1.image.sprite = imageLoader.digiHead;
        }

        if (FindUnlockedCompanionByName("Jaydizz") != null && FindActiveCompanionByName("Jaydizz") == null)
        {
            companionsButton2.interactable = true;
            companionsButton2.image.sprite = imageLoader.jaydizzHead;
        }

        if (FindUnlockedCompanionByName("Cndk99") != null && FindActiveCompanionByName("Cndk99") == null)
        {
            companionsButton3.interactable = true;
            companionsButton3.image.sprite = imageLoader.cndkHead;
        }

        if (FindUnlockedCompanionByName("OneViolence") != null && FindActiveCompanionByName("OneViolence") == null)
        {
            companionsButton4.interactable = true;
            companionsButton4.image.sprite = imageLoader.oneviolenceHead;
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

        xpBar.gameObject.SetActive(false);
        xpBarText.gameObject.SetActive(false);
        xpLevelText.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);
        itemsButton.gameObject.SetActive(false);
        partyButton.gameObject.SetActive(false);
        cheatButton.gameObject.SetActive(false);
    }

    public void CloseMessageButtonPressed()
    {
        waitCount -= 1;

        if (textQueue.Count <= 0)
        {
            if (passwordField.gameObject.activeSelf)
            {
                if (passwordField.text == "cndk99" || passwordField.text == "CNDK99" || passwordField.text == "cndk" || passwordField.text == "CNDK")
                {
                    AddMessage("CORRECT ANSWER! You have been granted nine hundred ninety-nine million, nine hundred ninety-nine thousand, nine hundred ninety-nine nolifepoints. These points only work as currency and do not increase your level because it would be incredibly broken if you suddenly became level 100. (Sponsored by StopSuicideDon)");
                    playerStats.noLifePoints += 999999999;
                }

                else
                {
                    AddMessage("Really? " + passwordField.text + "?" + " You disappoint me Slurp.");
                }
                passwordField.gameObject.SetActive(false);
                DisplayMessages(textQueue[0]);
                textQueue.Remove(textQueue[0]);
            }

            else if (!hankChoice && !pillChoice)
            {
                if (lesbiansTwo)
                {
                    audioManager.slurpLesbiansTwo.Play();
                    lesbiansTwo = false;
                    GameObject hank = GameObject.Find("HANKJWIMBLETON");
                    Destroy(hank);
                }

                gameState.globalPaused = false;
                dialoguePanel.SetActive(false);
                dialogueText.text = null;

                xpBar.gameObject.SetActive(true);
                xpBarText.gameObject.SetActive(true);
                xpLevelText.gameObject.SetActive(true);
                menuButton.gameObject.SetActive(true);
                itemsButton.gameObject.SetActive(true);
                partyButton.gameObject.SetActive(true);
                cheatButton.gameObject.SetActive(true);

                if (gameState.waitingCombat == true)
                {
                    FindAnyObjectByType<CombatManager>().StartCombat(gameState.partiesWaitingCombat[0], gameState.partiesWaitingCombat[1]);
                    gameState.waitingCombat = false;
                    gameState.partiesWaitingCombat.Clear();
                }
            }

            else if (hankChoice)
            {
                dialogueText.text = null;

                hankChoice1.gameObject.SetActive(true);
                hankChoice2.gameObject.SetActive(true);
            }

            else if (pillChoice)
            {
                dialogueText.text = null;

                bluePill.gameObject.SetActive(true);
                redPill.gameObject.SetActive(true);
            }
        }
        else 
        {
            DisplayMessages(textQueue[0]);
            textQueue.Remove(textQueue[0]); //is this the right way of doing it? Check it once you have internet connection !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }
    }

    public CompanionData FindUnlockedCompanionByName(string companionName) //Having a method like this here feels bad, honestly this whole thing feels retarded.
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
        AddMessage("A powerful shaman from Daneland, OneViolence and his magical powers will surely be of great help against the lord of decay!");
    }

    public void ENDTHEFUCKINGGAMEBUTTONPRESSED()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=PNFjLzVKVdk");
    }

    public void OpenCheat()
    {
        gameState.globalPaused = true;
        cheatScreen.SetActive(true);
    }

    public void GoBackCheat()
    {
        gameState.globalPaused = false;
        cheatScreen.SetActive(false);
    }

    public void CheatON()
    {
        CompanionData slurp = playerStats.unlockedCompanions[0];
        slurp.maxHealth += 9999;
        slurp.health += 9999;
        slurp.damage += 9999;
        slurp.speed += 80;
        cheatOFF.gameObject.SetActive(true);
        cheatON.gameObject.SetActive(false);
    }

    public void CheatOFF()
    {
        CompanionData slurp = playerStats.unlockedCompanions[0];
        slurp.maxHealth -= 9999;
        slurp.health -= 9999;
        slurp.damage -= 9999;
        slurp.speed -= 80;

        cheatON.gameObject.SetActive(true);
        cheatOFF.gameObject.SetActive(false);
    }
}