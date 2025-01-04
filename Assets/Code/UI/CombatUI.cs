using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class CombatUI : MonoBehaviour
{
    public ImageLoader imageLoader;
    public GameState gameState;
    public CombatManager combatManager;
    public CharacterData target;
    public PlayerStats playerStats;

    public GameObject combatUI; //parent of all the UI stuff for combat
    public GameObject asmonCloud;

    public GameObject attackButton;
    public GameObject skillButton;
    public GameObject itemButton;
    public GameObject fleeButton;
    public GameObject endTurnButton;
    public GameObject backButton;
    public GameObject skillBar;
    public GameObject itemBar;

    public GameObject continueButton;

    public TMP_Text combatText;
    public string combatFinishMessage;

    public Button slurp0;
    public Button slurp1;
    public Button slurp2;
    
    public Button dizz0;
    public Button dizz1;
    public Button dizz2;

    public int dizz0effect; //this fucking skill was the finishing move for my codebase, it is fucking over, it is beyond spaghetti at this point
    public int dizz1effect;
    public int dizz2effect;

    public string[] dizzEffectDescriptions; // it gets even worse here lmao, I have hit rock bottom and now I'm finding ways to dig deeper

    public int dizzClicked;

    public List<CharacterData> ownTeam;
    public List<CharacterData> enemyTeam;

    public Button skillButton0;
    public Button skillButton1;
    public Button skillButton2;
    public Button skillButton3;
    public Button skillButton4;
    public Button skillButton5;
    public Button skillButton6;
    public Button skillButton7;
    public Button skillButton8;
    public Button skillButton9;

    public TMP_Text skillCooldownText1;
    public TMP_Text skillCooldownText2;
    public TMP_Text skillCooldownText3;
    public TMP_Text skillCooldownText4;

    public Button itemButton0;
    public Button itemButton1;
    public Button itemButton2;
    public Button itemButton3;
    public Button itemButton4;
    public Button itemButton5;
    public Button itemButton6;
    public Button itemButton7;
    public Button itemButton8;
    public Button itemButton9;

    public enum State
    {
        MainUI,
        AttackTargetUI,
        SkillChoiceUI,
        SkillTargetUI,
        ItemChoiceUI,
        ItemTargetUI,
        FleeUI,
        CombatFinishedUI,
    }

    public State currentState = State.MainUI;

    void Update() //there is no reason to constantly update but the performance impact shouldn't be too much for now
    {
        switch (currentState)
        {
            case State.MainUI:
                attackButton.SetActive(true);
                skillButton.SetActive(true);
                itemButton.SetActive(true);
                fleeButton.SetActive(true);
                endTurnButton.SetActive(true);
                skillBar.SetActive(false);
                itemBar.SetActive(false);

                skillButton0.interactable = false;
                skillButton1.interactable = false;
                skillButton2.interactable = false;
                skillButton3.interactable = false;
                skillButton4.interactable = false;
                skillButton5.interactable = false;
                skillButton6.interactable = false;
                skillButton7.interactable = false;
                skillButton8.interactable = false;
                skillButton9.interactable = false;

                backButton.SetActive(false);
                continueButton.SetActive(false);

                if (combatManager.combatPauseCooldown <= 0 && !gameState.combatPaused) //this check is just another example of me trying to pay the technical debt
                {
                    combatText.text = null;
                }

                break;
            case State.AttackTargetUI:
                attackButton.SetActive(false);
                skillButton.SetActive(false);
                itemButton.SetActive(false);
                fleeButton.SetActive(false);
                endTurnButton.SetActive(false);
                skillBar.SetActive(false);
                itemBar.SetActive(false);

                backButton.SetActive(true);
                continueButton.SetActive(false);
                
                combatText.text = "Choose your Target!";
                break;
            case State.SkillChoiceUI:
                attackButton.SetActive(false);
                skillButton.SetActive(false);
                itemButton.SetActive(false);
                fleeButton.SetActive(false);
                endTurnButton.SetActive(false);
                skillBar.SetActive(true);
                itemBar.SetActive(false);

                backButton.SetActive(true);
                continueButton.SetActive(false);
                
                combatText.text = "Pick the skill you want to use!";
                break;
            case State.SkillTargetUI:
                
                combatText.text = "Choose your Target!";
                break;
            case State.ItemChoiceUI:
                attackButton.SetActive(false);
                skillButton.SetActive(false);
                itemButton.SetActive(false);
                fleeButton.SetActive(false);
                endTurnButton.SetActive(false);
                skillBar.SetActive(false);
                itemBar.SetActive(true);

                backButton.SetActive(true);
                continueButton.SetActive(false);
                
                combatText.text = "Pick the item you want to use!";
                break;
            case State.ItemTargetUI:
                
                combatText.text = "Choose your Target!";
                break;
            
            case State.FleeUI:
                attackButton.SetActive(false);
                skillButton.SetActive(false);
                itemButton.SetActive(false);
                fleeButton.SetActive(false);
                endTurnButton.SetActive(false);
                skillBar.SetActive(false);
                itemBar.SetActive(false);

                backButton.SetActive(true);
                continueButton.SetActive(false);
                
                combatText.text = "Hey Slurp! Unfortunately this button doesn't do anything. There are two reasons for this, " +
                                  "first one is the fact that a disengage mechanic created a fuck ton of bugs and problems that I was too lazy to solve. " +
                                  "Second (and the most important one) is the fact that fleeing combat is blue pilled cuck behavior, so now please click that back button and GET BACK INTO THE RING! RAAAAAAAARGG!";
                break;

            case State.CombatFinishedUI:
                attackButton.SetActive(false);
                skillButton.SetActive(false);
                itemButton.SetActive(false);
                fleeButton.SetActive(false);
                endTurnButton.SetActive(false);
                skillBar.SetActive(false);
                itemBar.SetActive(false);

                backButton.SetActive(false);
                continueButton.SetActive(true);
                combatText.text = combatFinishMessage;
                break;
            default:
                break;
        }

        if(gameState.combatFinished == true) //will add deatils like victory loss messages etc. even images maybe?
        {
            ChangeState(State.CombatFinishedUI);
        }
    }

    public void ChangeState(State newState) //WOW WHAT A FUCKING METHOD! IT WILL SAVE ME SO MUCH TIME
    {
        currentState = newState;
    }

    public void FixedUpdate() //???? this part feels kinda weird and I don't remember why I did it like this. Why do I have separate update functions?
    {
        if (combatManager.turnHaver != null && combatManager.turnHaver.team == 0) 
        {
            attackButton.GetComponent<Button>().interactable = true;
            skillButton.GetComponent<Button>().interactable = true;
            itemButton.GetComponent<Button>().interactable = true;
            fleeButton.GetComponent<Button>().interactable = true;
            endTurnButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            attackButton.GetComponent<Button>().interactable = false;
            skillButton.GetComponent<Button>().interactable = false;
            itemButton.GetComponent<Button>().interactable = false;
            fleeButton.GetComponent<Button>().interactable = false;
            endTurnButton.GetComponent<Button>().interactable = false;
        }
    }

    public void AttackButtonPressed()
    {
        ChangeState(State.AttackTargetUI);
    }

    public void SkillButtonPressed()
    {
        CheckSkills();
        ChangeState(State.SkillChoiceUI);
    }

    public void ItemButtonPressed()
    {
        CheckItems();
        ChangeState(State.ItemChoiceUI);
    }

    public void SkillSlot0Pressed() //this can be done with just one method instead of a separate method for each slot but at this point I really don't care
    {
        ChangeState(State.SkillTargetUI);
        combatManager.selectedSkill = combatManager.turnHaver.skills[1];
    }

    public void SkillSlot1Pressed()
    {
        ChangeState(State.SkillTargetUI);
        combatManager.selectedSkill = combatManager.turnHaver.skills[2];
    }

    public void SkillSlot2Pressed()
    {
        ChangeState(State.SkillTargetUI);
        combatManager.selectedSkill = combatManager.turnHaver.skills[3];
    }

    public void SkillSlot3Pressed()
    {
        ChangeState(State.SkillTargetUI);
        combatManager.selectedSkill = combatManager.turnHaver.skills[4];
    }

    public void ThotwisButtonPressed()
    {
        combatManager.turnHaver.GetComponent<CharacterFunctions>().GetInflicted("thottery",999); //there is a fucking bug here, I don't know why there is a fucking bug here, I want to kill myself
        slurp0.gameObject.SetActive(false);
        slurp1.gameObject.SetActive(false);
        slurp2.gameObject.SetActive(false);

        combatManager.turnHaver = null;
        combatManager.gameState.combatPaused = false;
        combatText.text = "Is that Thotwis? Oh god... I'm gonna... I'M GONNA COOOOOOOOOM!";
    }

    public void ClownwisButtonPressed()
    {
        slurp0.gameObject.SetActive(false);
        slurp1.gameObject.SetActive(false);
        slurp2.gameObject.SetActive(false);

        combatManager.turnHaver = null;
        combatManager.gameState.combatPaused = false;
        combatManager.combatPauseCooldown = 4;
        combatText.text = "ERROR: CLOWN FORM CAN NOT BE ALLOWED. TURN TERMINATED. (Oh wow! Clown form doesn't work? Why is that? How mysterious?)";
    }

    public void TradwisButtonPressed()
    {
        combatManager.turnHaver.GetComponent<CharacterFunctions>().GetInflicted("return to trad", 999);
        slurp0.gameObject.SetActive(false);
        slurp1.gameObject.SetActive(false);
        slurp2.gameObject.SetActive(false);

        combatManager.turnHaver = null;
        combatManager.gameState.combatPaused = false;
        combatText.text = "Slurp finally learned her place and started making some fucking sandwiches!";
    }

    public void Dizz0ButtonPressed()
    {
        dizzClicked += 1;
        
        if(dizzClicked == 3)
        {
            dizzClicked = 0;
            FireDizzEffect(dizz0effect);

            combatText.text = "Congratulations! Your reward is: " + dizzEffectDescriptions[dizz0effect - 1];
        }
        else
        {
            combatText.text = "This box had: " + dizzEffectDescriptions[dizz0effect - 1];
        }

        dizz0.gameObject.SetActive(false);
    }

    public void Dizz1ButtonPressed()
    {
        dizzClicked += 1;

        if (dizzClicked == 3)
        {
            dizzClicked = 0;
            FireDizzEffect(dizz1effect);

            combatText.text = "Congratulations! Your reward is: " + dizzEffectDescriptions[dizz1effect - 1];
        }
        else
        {
            combatText.text = "This box had: " + dizzEffectDescriptions[dizz1effect - 1];
        }

        dizz1.gameObject.SetActive(false);
    }

    public void Dizz2ButtonPressed()
    {
        dizzClicked += 1;

        if (dizzClicked == 3)
        {
            dizzClicked = 0;
            FireDizzEffect(dizz2effect);

            combatText.text = "Congratulations! Your reward is: " + dizzEffectDescriptions[dizz2effect - 1];
        }
        else
        {
            combatText.text = "This box had: " + dizzEffectDescriptions[dizz2effect - 1];
        }

        dizz2.gameObject.SetActive(false);
    }

    public void FireDizzEffect(int effect)
    {
        if (effect == 1)
        {
            CharacterFunctions dizzTarget = enemyTeam[Random.Range(0, enemyTeam.Count)].GetComponent<CharacterFunctions>();
            dizzTarget.TakeDamage(80, true);
        }

        else if (effect == 2)
        {
            CharacterFunctions dizzTarget = enemyTeam[Random.Range(0, enemyTeam.Count)].GetComponent<CharacterFunctions>();
            dizzTarget.TakeDamage(160, true);
        }

        else if (effect == 3)
        {
            CharacterFunctions dizzTarget = enemyTeam[Random.Range(0, enemyTeam.Count)].GetComponent<CharacterFunctions>();
            dizzTarget.TakeDamage(320, true);
        }

        else if (effect == 4)
        {
            List<CharacterData> charactersToKill = new();
            foreach (CharacterData character in enemyTeam)
            {
                if (character.health <= 40)
                {
                    charactersToKill.Add(character);
                }

                else
                {
                    character.GetComponent<CharacterFunctions>().TakeDamage(40, true);
                }
            }
            if (charactersToKill.Count > 0)
            {
                foreach (CharacterData yeetCandidate in charactersToKill)
                {
                    yeetCandidate.GetComponent<CharacterFunctions>().Die();
                }
            }
        }

        else if (effect == 5)
        {
            List<CharacterData> charactersToKill = new();
            foreach (CharacterData character in enemyTeam)
            {
                if (character.health <= 80)
                {
                    charactersToKill.Add(character);
                }

                else
                {
                    character.GetComponent<CharacterFunctions>().TakeDamage(80, true);
                }
            }
            if (charactersToKill.Count > 0)
            {
                foreach (CharacterData yeetCandidate in charactersToKill)
                {
                    yeetCandidate.GetComponent<CharacterFunctions>().Die();
                }
            }
        }

        else if (effect == 6)
        {
            List<CharacterData> charactersToKill = new();
            foreach (CharacterData character in enemyTeam)
            {
                if (character.health <= 160)
                {
                    charactersToKill.Add(character);
                }

                else
                {
                    character.GetComponent<CharacterFunctions>().TakeDamage(160, true);
                }
            }
            if (charactersToKill.Count > 0)
            {
                foreach (CharacterData yeetCandidate in charactersToKill)
                {
                    yeetCandidate.GetComponent<CharacterFunctions>().Die();
                }
            }
        }

        else if (effect == 7)
        {
            CharacterFunctions dizzTarget = enemyTeam[Random.Range(0, enemyTeam.Count)].GetComponent<CharacterFunctions>();
            dizzTarget.GetInflicted("stun", 4);
        }

        else if (effect == 8)
        {
            foreach (CharacterData character in enemyTeam)
            {
                character.GetComponent<CharacterFunctions>().GetInflicted("stun", 2);
            }
        }

        else if (effect == 9)
        {
            
        }

        gameState.combatPaused = false;
    }

    public void CheckSkills()
    {
        skillButton0.interactable = false;
        skillButton1.interactable = false;
        skillButton2.interactable = false;
        skillButton3.interactable = false;
        skillButton0.image.sprite = null;
        skillButton1.image.sprite = null;
        skillButton2.image.sprite = null;
        skillButton3.image.sprite = null;
        skillButton4.gameObject.SetActive(false); //retarded problems require retarded solutions
        skillButton5.gameObject.SetActive(false);
        skillButton6.gameObject.SetActive(false);
        skillButton7.gameObject.SetActive(false);
        skillButton8.gameObject.SetActive(false);
        skillButton9.gameObject.SetActive(false);

        skillCooldownText1.text = combatManager.turnHaver.skill1Cooldown.ToString();
        skillCooldownText2.text = combatManager.turnHaver.skill2Cooldown.ToString();
        skillCooldownText3.text = combatManager.turnHaver.skill3Cooldown.ToString();
        skillCooldownText4.text = combatManager.turnHaver.skill4Cooldown.ToString();

        if (combatManager.turnHaver.skills.Count == 2)
        {
            skillButton0.image.sprite = combatManager.turnHaver.skills[1].skillIcon;
            skillCooldownText1.text = combatManager.turnHaver.skill1Cooldown.ToString();
            
            if (combatManager.turnHaver.skill1Cooldown == 0)
            {
                skillButton0.interactable = true;
                skillCooldownText1.text = "";
            }
            
            skillCooldownText2.text = "";
            skillCooldownText3.text = "";
            skillCooldownText4.text = "";
        }
        
        else if (combatManager.turnHaver.skills.Count == 3)
        {
            skillButton0.image.sprite = combatManager.turnHaver.skills[1].skillIcon;
            skillCooldownText1.text = combatManager.turnHaver.skill1Cooldown.ToString();

            skillButton1.image.sprite = combatManager.turnHaver.skills[2].skillIcon;
            skillCooldownText2.text = combatManager.turnHaver.skill2Cooldown.ToString();

            if (combatManager.turnHaver.skill1Cooldown == 0)
            {
                skillButton0.interactable = true;
                skillCooldownText1.text = "";
            }
            if (combatManager.turnHaver.skill2Cooldown == 0)
            {
                skillButton1.interactable = true;
                skillCooldownText2.text = "";
            }
            
            skillCooldownText3.text = "";
            skillCooldownText4.text = "";
        }

        else if (combatManager.turnHaver.skills.Count == 4)
        {
            skillButton0.image.sprite = combatManager.turnHaver.skills[1].skillIcon;
            skillCooldownText1.text = combatManager.turnHaver.skill1Cooldown.ToString();

            skillButton1.image.sprite = combatManager.turnHaver.skills[2].skillIcon;
            skillCooldownText2.text = combatManager.turnHaver.skill2Cooldown.ToString();

            skillButton2.image.sprite = combatManager.turnHaver.skills[3].skillIcon;
            skillCooldownText3.text = combatManager.turnHaver.skill3Cooldown.ToString();

            if (combatManager.turnHaver.skill1Cooldown == 0)
            {
                skillButton0.interactable = true;
                skillCooldownText1.text = "";
            }
            if (combatManager.turnHaver.skill2Cooldown == 0)
            {
                skillButton1.interactable = true;
                skillCooldownText2.text = "";
            }
            if (combatManager.turnHaver.skill3Cooldown == 0)
            {
                skillButton2.interactable = true;
                skillCooldownText3.text = "";
            }
            
            skillCooldownText4.text = "";
        }

        else if (combatManager.turnHaver.skills.Count == 5)
        {
            skillButton0.image.sprite = combatManager.turnHaver.skills[1].skillIcon;
            skillCooldownText1.text = combatManager.turnHaver.skill1Cooldown.ToString();

            skillButton1.image.sprite = combatManager.turnHaver.skills[2].skillIcon;
            skillCooldownText2.text = combatManager.turnHaver.skill2Cooldown.ToString();

            skillButton2.image.sprite = combatManager.turnHaver.skills[3].skillIcon;
            skillCooldownText3.text = combatManager.turnHaver.skill3Cooldown.ToString();

            skillButton3.image.sprite = combatManager.turnHaver.skills[4].skillIcon;
            skillCooldownText4.text = combatManager.turnHaver.skill4Cooldown.ToString();

            if (combatManager.turnHaver.skill1Cooldown == 0)
            {
                skillButton0.interactable = true;
                skillCooldownText1.text = "";
            }
            if (combatManager.turnHaver.skill2Cooldown == 0)
            {
                skillButton1.interactable = true;
                skillCooldownText2.text = "";
            }
            if (combatManager.turnHaver.skill3Cooldown == 0)
            {
                skillButton2.interactable = true;
                skillCooldownText3.text = "";
            }
            if (combatManager.turnHaver.skill4Cooldown == 0)
            {
                skillButton3.interactable = true;
                skillCooldownText4.text = "";
            }
            if (combatManager.turnHaver.skill4Cooldown >= 100)
            {
                skillCooldownText4.text = "X";
            }
        }
    }

    public void CheckItems()
    {
        itemButton0.interactable = false;
        itemButton1.interactable = false;
        itemButton2.interactable = false;


        if (playerStats.catFood != 0)
        {
            itemButton0.interactable = true;
        }

        if (playerStats.pizza != 0)
        {
            itemButton1.interactable = true;
        }

        if (playerStats.gamblingChip != 0)
        {
            itemButton2.interactable = true;
        }
    }

    public void ItemSlot0Pressed() //HELL YEAH! LET'S DO THE SAME FUCKING THING FOR THE ITEMS. TRULY A 10X CODER RIGHT HERE
    {
        ChangeState(State.ItemTargetUI);
        combatManager.selectedItem = "catFood";
    }

    public void ItemSlot1Pressed()
    {
        ChangeState(State.ItemTargetUI);
        combatManager.selectedItem = "pizza";
    }

    public void ItemSlot2Pressed()
    {
        ChangeState(State.ItemTargetUI);
        combatManager.selectedItem = "gamblingChip";
    }

    public void FleeButtonPressed()
    {
        ChangeState(State.FleeUI);
    }

    public void EndTurnButtonPressed()
    {
        combatManager.turnHaver = null;
    }

    public void ContinueButtonPressed()
    {
        ChangeState(State.MainUI); //Should probably do this in a cleaner way but no problem for now
        gameState.combatFinished = false;
        gameState.overworldPaused = false;
        combatFinishMessage = null;
        combatUI.SetActive(false);
        combatManager.ClearCombat();

        if (gameState.trickyPlaying)
        {
            FindObjectOfType<AudioManager>().trickyTheme.Stop();
            FindObjectOfType<AudioManager>().corruptionTheme.Play();

            gameState.trickyPlaying = false;
        }
    }

    public void BackButtonPressed()
    {
        switch (currentState)
        {
            case State.AttackTargetUI:
                ChangeState(State.MainUI);
                break;
            case State.SkillChoiceUI:
                ChangeState(State.MainUI);
                break;
            case State.SkillTargetUI:
                ChangeState(State.SkillChoiceUI);
                break;
            case State.ItemChoiceUI:
                ChangeState(State.MainUI);
                break;
            case State.ItemTargetUI:
                ChangeState(State.ItemChoiceUI);
                break;
            case State.FleeUI:
                ChangeState(State.MainUI);
                break;
            default:
                break;
        }
    }
}
