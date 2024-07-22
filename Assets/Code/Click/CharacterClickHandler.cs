using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CharacterClickHandler : MonoBehaviour, IPointerClickHandler
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

        switch (combatUI.currentState)
        {
            case CombatUI.State.AttackTargetUI:

                if (combatManager.turnHaver.team != ownData.team) //check if it is an enemy
                {
                    combatFunctions.Attack(combatManager.turnHaver, ownData);
                    combatManager.turnHaver = null;
                    combatUI.ChangeState(CombatUI.State.MainUI);
                }
                break;

            case CombatUI.State.SkillTargetUI:

                if (combatManager.turnHaver.team != ownData.team && combatManager.selectedSkill.hostile) //if enemy and the skill is supposed to be used on enemies
                {
                    combatFunctions.UseSkill(userTeam, combatManager.turnHaver, enemyTeam, ownData, combatManager.selectedSkill);
                    combatManager.turnHaver = null;
                    combatUI.ChangeState(CombatUI.State.MainUI);
                }
                else if (combatManager.turnHaver.team == ownData.team && !combatManager.selectedSkill.hostile) //if ally and skill is supposed to be used on allies
                {
                    combatFunctions.UseSkill(userTeam, combatManager.turnHaver, enemyTeam, ownData, combatManager.selectedSkill);
                    combatManager.turnHaver = null;
                    combatUI.ChangeState(CombatUI.State.MainUI);
                }
                break;
            
            case CombatUI.State.ItemTargetUI:

                combatFunctions.UseItem(userTeam, combatManager.turnHaver, enemyTeam, ownData, combatManager.selectedItem);
                combatManager.turnHaver = null;
                combatUI.ChangeState(CombatUI.State.MainUI);
                break;

            default:
                Debug.Log("Back button pressed in unknown state.");
                break;
        }
    }
}