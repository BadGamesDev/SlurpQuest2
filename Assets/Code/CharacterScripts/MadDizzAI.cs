using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadDizzAI : MonoBehaviour
{
    public GameObject alien;
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
        moves.Add("dizzOrNoDizz");
        moves.Add("attack");
        moves.Add("attack");
        moves.Add("attack");
        moves.Add("attack");
        moves.Add("burnout");
        moves.Add("attack");
        moves.Add("attack");
        moves.Add("pitstop");

        alien = prefabLoader.AlienPrefab;
    }

    public void FixedUpdate()//horrible for performance
    {
        if (combatManager.turnHaver == ownData && !ownData.selfStatusEffects.Contains(StatusEffectDatabase.stun) && combatManager.teamOne.Count != 0)
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

            combatManager.turnHaver = null;
            turnNumber += 1;
        }
    }
}