using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatUI : MonoBehaviour
{
    public GameState gameState;
    public CombatManager combatManager;
    public CharacterData target;
    public PlayerStats playerStats;

    public GameObject combatUI; //parent of all the UI stuff for combat

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

                backButton.SetActive(false);
                continueButton.SetActive(false);
                
                combatText.text = null;
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
                skillButton0.GetComponentInChildren<TMP_Text>().text = combatManager.turnHaver.skills[0].skillName; //this is just for testing !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
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

    public void FixedUpdate()
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
        ChangeState(State.SkillChoiceUI);
    }

    public void SkillSlot0Pressed() //this can be done with just one method instead of a separate method for each slot but at this point I really don't care
    {
        ChangeState(State.SkillTargetUI);
        combatManager.selectedSkill = combatManager.turnHaver.skills[0];
    }

    public void SkillSlot1Pressed()
    {

    }

    public void SkillSlot2Pressed()
    {

    }

    public void SkillSlot3Pressed()
    {

    }
    public void SkillSlot4Pressed()
    {

    }
    public void SkillSlot5Pressed()
    {

    }
    public void SkillSlot6Pressed()
    {

    }
    public void SkillSlot7Pressed()
    {

    }

    public void SkillSlot8Pressed()
    {

    }

    public void SkillSlot9Pressed()
    {

    }

    public void ItemButtonPressed()
    {
        CheckItems();
        ChangeState(State.ItemChoiceUI);
    }

    public void CheckItems()
    {
        itemButton0.interactable = false;
        itemButton1.interactable = false;
        itemButton2.interactable = false;
        itemButton3.interactable = false;
        itemButton4.interactable = false;
        itemButton5.interactable = false;
        itemButton6.interactable = false;
        itemButton7.interactable = false;
        itemButton8.interactable = false;
        itemButton9.interactable = false;

        if (playerStats.catFood != 0)
        {
            itemButton0.interactable = true;
        }
       
    }

    public void ItemSlot0Pressed() //HELL YEAH! LET'S DO THE SAME FUCKING THING FOR THE ITEMS. TRULY A 10X CODER RIGHT HERE
    {
        ChangeState(State.ItemTargetUI);
        combatManager.selectedItem = "catFood";
    }

    public void ItemSlot1Pressed()
    {

    }

    public void ItemSlot2Pressed()
    {

    }

    public void ItemSlot3Pressed()
    {

    }
    public void ItemSlot4Pressed()
    {

    }
    public void ItemSlot5Pressed()
    {

    }
    public void ItemSlot6Pressed()
    {

    }
    public void ItemSlot7Pressed()
    {

    }

    public void ItemSlot8Pressed()
    {

    }

    public void ItemSlot9Pressed()
    {

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
        combatUI.SetActive(false);
        combatManager.ClearCombat();
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
