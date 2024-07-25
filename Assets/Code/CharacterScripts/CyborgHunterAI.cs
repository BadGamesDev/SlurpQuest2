using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyborgHunterAI : MonoBehaviour
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

        moves.Add("attack");
        moves.Add("emp grenade");
        moves.Add("attack");
        moves.Add("attack");
        moves.Add("summon aliens");
        moves.Add("find bigfoot");
        moves.Add("attack");
        moves.Add("attack");
        moves.Add("emp grenade");
        moves.Add("attack");
        moves.Add("attack");
        moves.Add("emp grenade");
        moves.Add("attack");
        moves.Add("attack");
        moves.Add("emp grenade");
        moves.Add("attack");
        moves.Add("attack");
        moves.Add("emp grenade");

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
            else if (moves[turnNumber] == "emp grenade")
            {
                CharacterData target = combatManager.teamOne[Random.Range(0, combatManager.teamOne.Count)];
                combatFunctions.UseSkill(combatManager.teamTwo, ownData, combatManager.teamOne, target, SkillDatabase.empGrenade);
            }
            else if (moves[turnNumber] == "summon aliens")
            {
                GameObject combatant5 = Instantiate(alien, combatManager.spawnSlots[4].position, Quaternion.identity);
                combatant5.transform.SetParent(combatManager.spawnSlots[4]);
                CharacterData combatant5Data = combatant5.GetComponent<CharacterData>();
                combatManager.combatants.Add(combatant5Data);
                combatant5Data.team = 1;
                combatManager.teamTwo.Add(combatant5Data);
                combatant5Data.turnCoolDown += Random.Range(100, 1001);


                GameObject combatant6 = Instantiate(alien, combatManager.spawnSlots[5].position, Quaternion.identity);
                combatant6.transform.SetParent(combatManager.spawnSlots[5]);
                CharacterData combatant6Data = combatant6.GetComponent<CharacterData>();
                combatManager.combatants.Add(combatant6Data);
                combatant6Data.team = 1;
                combatManager.teamTwo.Add(combatant6Data);
                combatant6Data.turnCoolDown += Random.Range(100, 1001);
            }
            else if (moves[turnNumber] == "find bigfoot")
            {
                CharacterData target = combatManager.teamOne[Random.Range(0, combatManager.teamOne.Count)];
                combatFunctions.UseSkill(combatManager.teamTwo, ownData, combatManager.teamOne, target, SkillDatabase.findBigfoot);
            }

            combatManager.turnHaver = null;
            turnNumber += 1;
        }
    }
}