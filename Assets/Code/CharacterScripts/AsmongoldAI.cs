using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsmongoldAI : MonoBehaviour
{
    public GameObject roach;
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

        moves.Add("own goal");
        moves.Add("summon roaches");
        moves.Add("cloud of decay");
        moves.Add("attack");
        moves.Add("attack");


        roach = prefabLoader.RoachPrefab;
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

                else if (moves[turnNumber] == "cloud of decay")
                {
                    List<CharacterData> charactersToKill = new();

                    foreach (CharacterData character in combatManager.teamOne)
                    {

                        if (character.health <= 30)
                        {
                            charactersToKill.Add(character);
                        }
                        else
                        {
                            character.GetComponent<CharacterFunctions>().TakeDamage(30, true);
                        }
                    }
                }

                else if (moves[turnNumber] == "summon roaches")
                {
                    GameObject combatant5 = Instantiate(roach, combatManager.spawnSlots[4].position, Quaternion.identity);
                    combatant5.transform.SetParent(combatManager.spawnSlots[4]);
                    CharacterData combatant5Data = combatant5.GetComponent<CharacterData>();
                    combatManager.combatants.Add(combatant5Data);
                    combatant5Data.team = 1;
                    combatManager.teamTwo.Add(combatant5Data);
                    combatant5Data.turnCoolDown += Random.Range(100, 1001);


                    GameObject combatant6 = Instantiate(roach, combatManager.spawnSlots[5].position, Quaternion.identity);
                    combatant6.transform.SetParent(combatManager.spawnSlots[5]);
                    CharacterData combatant6Data = combatant6.GetComponent<CharacterData>();
                    combatManager.combatants.Add(combatant6Data);
                    combatant6Data.team = 1;
                    combatManager.teamTwo.Add(combatant6Data);
                    combatant6Data.turnCoolDown += Random.Range(100, 1001);
                }

                else if (moves[turnNumber] == "own goal")
                {
                    if (combatManager.spawnSlots[0].childCount != 0)
                    {
                        CharacterFunctions slurp;
                        Debug.Log("slurp is alive");
                        slurp = combatManager.spawnSlots[0].GetChild(0).GetComponent<CharacterFunctions>();
                        slurp.TakeDamage(slurp.ownData.health - 1, true);
                    }
                    else
                    {
                        CharacterData target = combatManager.teamOne[Random.Range(0, combatManager.teamOne.Count)];
                        combatFunctions.Attack(ownData, target);
                    }
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