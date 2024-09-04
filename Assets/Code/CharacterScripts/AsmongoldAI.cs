using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsmongoldAI : MonoBehaviour
{
    public GameObject roach;
    public PrefabLoader prefabLoader;
    public CombatManager combatManager;
    public CombatFunctions combatFunctions;
    public CombatUI combatUI;
    public CharacterData ownData;
    public List<string> moves;
    public int turnNumber;

    void Start()
    {
        combatManager = FindObjectOfType<CombatManager>();
        combatFunctions = FindObjectOfType<CombatFunctions>();
        combatUI = FindObjectOfType<CombatUI>();
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
                        if (character.health <= 50)
                        {
                            charactersToKill.Add(character);
                        }
                        else
                        {
                            character.GetComponent<CharacterFunctions>().TakeDamage(50, true);
                        }
                    }

                    foreach (CharacterData character in combatManager.teamTwo)
                    {
                        character.GetComponent<CharacterFunctions>().GetHealed(50);
                    }

                    if (charactersToKill.Count > 0)
                    {
                        foreach(CharacterData character in charactersToKill)
                        {
                            character.GetComponent<CharacterFunctions>().Die();
                        }
                    }

                    combatManager.combatPauseCooldown = 3;
                    combatUI.combatText.text = "Asmongold used cloud of decay. The toxic cloud dealt damage to every enemy while healing his team.";
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

                    combatManager.combatPauseCooldown = 3;
                    combatUI.combatText.text = "Asmongold summoned creatures from his room to fight for him!";
                }

                else if (moves[turnNumber] == "own goal")
                {
                    if (combatManager.spawnSlots[0].childCount > 1)
                    {
                        CharacterData slurp = null;
                        Debug.Log("slurp is alive");
                        foreach (CharacterData person in combatManager.combatants)
                        {
                            if (person.characterName == "Slurp")
                            {
                                slurp = person; 
                            }
                        }
                        slurp.GetComponent<CharacterFunctions>().TakeDamage(slurp.health - 1, true);

                        combatManager.combatPauseCooldown += 3.5f;
                        combatUI.combatText.text = "Asmongold told Slurp that this whole streaming thing is a waste of time.";
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