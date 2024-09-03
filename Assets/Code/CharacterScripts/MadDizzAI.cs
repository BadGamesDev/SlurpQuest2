using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadDizzAI : MonoBehaviour
{
    public PrefabLoader prefabLoader;
    public CombatManager combatManager;
    public CombatFunctions combatFunctions;
    public CharacterData ownData;
    public List<string> moves;
    public int turnNumber;

    void Start()
    {
        combatManager = FindObjectOfType<CombatManager>();
        combatFunctions = FindObjectOfType<CombatFunctions>();
        prefabLoader = FindObjectOfType<PrefabLoader>();

        moves.Add("startEngines");
        moves.Add("attack");
        moves.Add("attack");
        moves.Add("attack");
        moves.Add("attack");
        moves.Add("attack");
        moves.Add("attack");
        moves.Add("attack");
        moves.Add("attack");
        moves.Add("dizzOrNoDizz");
        moves.Add("burnout");
        moves.Add("attack");
        moves.Add("attack");
        moves.Add("attack");
        moves.Add("pitstop");
        moves.Add("attack");
    }

    public void FixedUpdate()//horrible for performance
    {
        if (combatManager.turnHaver == ownData && !ownData.selfStatusEffects.Contains(StatusEffectDatabase.stun) && combatManager.teamOne.Count != 0)
        {
            if (turnNumber < moves.Count)
            {
                if (moves[turnNumber] == "attack")
                {
                    CharacterData target = combatManager.teamOne[Random.Range(0, combatManager.teamOne.Count)];
                    combatFunctions.Attack(ownData, target);
                }
                else if (moves[turnNumber] == "startEngines")
                {
                    CharacterData target = ownData;
                    combatFunctions.UseSkill(combatManager.teamTwo, ownData, combatManager.teamOne, target, SkillDatabase.startYourEngines);
                    combatManager.combatPauseCooldown += 2;
                }
                else if (moves[turnNumber] == "burnout")
                {
                    CharacterData target = ownData;
                    combatFunctions.UseSkill(combatManager.teamTwo, ownData, combatManager.teamOne, target, SkillDatabase.burnout);
                }
                else if (moves[turnNumber] == "pitstop")
                {
                    CharacterData target = ownData;
                    combatFunctions.UseSkill(combatManager.teamTwo, ownData, combatManager.teamOne, target, SkillDatabase.pitStop);
                }
                else if (moves[turnNumber] == "dizzOrNoDizz")
                {
                    CharacterData target = combatManager.teamOne[Random.Range(0, combatManager.teamOne.Count)];
                    combatFunctions.UseSkill(combatManager.teamTwo, ownData, combatManager.teamOne, target, SkillDatabase.dizzOrNoDizz);
                }
            }

            else
            {
                CharacterData target = combatManager.teamOne[Random.Range(0, combatManager.teamOne.Count)];
                combatFunctions.Attack(ownData, target);
                Debug.Log("No moves left");
            }

            combatManager.turnHaver = null;
            turnNumber += 1;
        }
    }
}