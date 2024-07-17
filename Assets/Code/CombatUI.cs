using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatUI : MonoBehaviour
{
    public CombatManager combatManager;
    public CharacterData target;

    public GameObject attackButton;
    public GameObject skillButton;
    public GameObject itemButton;
    public GameObject fleeButton;
    public GameObject endTurnButton;
    public GameObject backButton;
    public GameObject skillBar;

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

    public enum State
    {
        MainUI,
        AttackTargetUI,
        SkillChoiceUI,
        SkillTargetUI,
        ItemChoiceUI,
        ItemTargetUI,
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
                backButton.SetActive(false);
                skillBar.SetActive(false);
                break;
            case State.AttackTargetUI:
                attackButton.SetActive(false);
                skillButton.SetActive(false);
                itemButton.SetActive(false);
                fleeButton.SetActive(false);
                endTurnButton.SetActive(false);
                backButton.SetActive(true);
                skillBar.SetActive(false);
                break;
            case State.SkillChoiceUI:
                attackButton.SetActive(false);
                skillButton.SetActive(false);
                itemButton.SetActive(false);
                fleeButton.SetActive(false);
                endTurnButton.SetActive(false);
                backButton.SetActive(true);
                skillBar.SetActive(true);

                skillButton0.GetComponentInChildren<TMP_Text>().text = combatManager.turnHaver.skills[0].skillName; //this is just for testing !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                break;
            case State.SkillTargetUI:
                break;
            case State.ItemChoiceUI:
                break;
            case State.ItemTargetUI:
                break;
            default:
                break;
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
            default:
                break;
        }
    }
}
