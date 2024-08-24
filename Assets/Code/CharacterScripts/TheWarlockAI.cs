using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWarlockAI : MonoBehaviour
{
    public CombatManager combatManager;
    public CombatFunctions combatFunctions;
    public CharacterData ownData;
    public List<string> moves;
    public int turnNumber;

    void Start()
    {
        combatManager = FindObjectOfType<CombatManager>();
        combatFunctions = FindObjectOfType<CombatFunctions>();

        moves.Add("attack");
        moves.Add("corpse paint");
        moves.Add("attack");
        moves.Add("attack");
        moves.Add("attack");
        moves.Add("one peace");
        moves.Add("herbal medicine");
        moves.Add("attack");
        moves.Add("one violence");
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

                else if (moves[turnNumber] == "herbal medicine")
                {
                    CharacterData target = ownData;
                    combatFunctions.UseSkill(combatManager.teamTwo, ownData, combatManager.teamOne, target, SkillDatabase.herbalMedicine);
                }

                else if (moves[turnNumber] == "corpse paint")
                {
                    CharacterData target = ownData;
                    combatFunctions.UseSkill(combatManager.teamTwo, ownData, combatManager.teamOne, target, SkillDatabase.corpsePaint);
                }

                else if (moves[turnNumber] == "one peace")
                {
                    CharacterData target = ownData;
                    combatFunctions.UseSkill(combatManager.teamTwo, ownData, combatManager.teamOne, target, SkillDatabase.onePeace);
                }

                else if (moves[turnNumber] == "one violence")
                {
                    CharacterData target = combatManager.teamOne[Random.Range(0, combatManager.teamOne.Count)];
                    combatFunctions.UseSkill(combatManager.teamTwo, ownData, combatManager.teamOne, target, SkillDatabase.oneViolence);
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