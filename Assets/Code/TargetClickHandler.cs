using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ClickHandler : MonoBehaviour, IPointerClickHandler
{
    public CombatManager combatManager;
    public CombatFunctions combatFunctions; //should probably move most of this stuff over to UI script or something or maybe make a controls script
    public CombatUI combatUI;
    public CharacterData ownData;

    void Start()
    {
        combatManager = FindObjectOfType<CombatManager>();
        combatUI = FindObjectOfType<CombatUI>();
        combatFunctions = FindObjectOfType<CombatFunctions>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Image Clicked!");
        switch (combatUI.currentState)
        {
            case CombatUI.State.AttackTargetUI:
                combatFunctions.Attack(combatManager.turnHaver, ownData);
                combatManager.turnHaver = null;
                combatUI.ChangeState(CombatUI.State.MainUI);
                break;

            case CombatUI.State.SkillTargetUI:
                List<CharacterData> userTeam = null;
                List<CharacterData> enemyTeam = null;
                
                if (combatManager.turnHaver.team == 0)
                {
                    userTeam = combatManager.teamOne;
                    enemyTeam = combatManager.teamTwo;
                }
                else if (combatManager.turnHaver.team == 1)
                {
                    userTeam = combatManager.teamTwo;
                    enemyTeam = combatManager.teamOne;
                }

                combatFunctions.UseSkill(userTeam, combatManager.turnHaver, enemyTeam, ownData, combatManager.selectedSkill);
                combatManager.turnHaver = null;
                combatUI.ChangeState(CombatUI.State.MainUI);
                break;
            
            case CombatUI.State.ItemTargetUI:
                Debug.Log("Back button pressed in Item Target UI. Returning to Item Choice UI.");
                combatUI.ChangeState(CombatUI.State.ItemChoiceUI);
                break;
            
            default:
                Debug.Log("Back button pressed in unknown state.");
                break;
        }
    }
}