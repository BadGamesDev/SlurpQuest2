using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadoAI : MonoBehaviour
{
    public GameObject tricky;
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
        moves.Add("tricky Entrance");

        tricky = prefabLoader.TrickyPrefab;
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
                else if (moves[turnNumber] == "tricky Entrance")
                {
                    GameObject combatant5 = Instantiate(tricky, combatManager.spawnSlots[4].position, Quaternion.identity);
                    CharacterData combatant5Data = combatant5.GetComponent<CharacterData>();

                    combatant5.transform.SetParent(combatManager.spawnSlots[4]);
                    combatManager.combatants.Add(combatant5Data);
                    combatant5Data.team = 1;
                    combatManager.teamTwo.Add(combatant5Data);
                    combatant5Data.turnCoolDown = 5;

                    combatManager.combatPauseCooldown = 10;
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