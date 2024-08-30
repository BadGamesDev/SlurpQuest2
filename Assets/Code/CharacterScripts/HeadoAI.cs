using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadoAI : MonoBehaviour
{
    public GameObject tricky;
    public CombatUI combatUI;
    public PrefabLoader prefabLoader;
    public CombatManager combatManager;
    public CombatFunctions combatFunctions;
    public CharacterData ownData;
    public List<string> moves;
    public int turnNumber;

    public float dialogueCooldown;
    public int dialogueStage;

    void Start()
    {
        combatManager = FindObjectOfType<CombatManager>();
        combatFunctions = FindObjectOfType<CombatFunctions>();
        prefabLoader = FindObjectOfType<PrefabLoader>();
        combatUI = FindObjectOfType<CombatUI>();

        moves.Add("attack");
        moves.Add("tricky Entrance");
        moves.Add("attack");

        tricky = prefabLoader.TrickyPrefab;
    }

    public void FixedUpdate()//horrible for performance
    {
        if (combatManager.turnHaver == ownData && !ownData.selfStatusEffects.Contains(StatusEffectDatabase.stun) && combatManager.teamOne.Count != 0)
        {
            dialogueCooldown -= Time.deltaTime;

            if (turnNumber < moves.Count)
            {
                if (moves[turnNumber] == "attack")
                {
                    CharacterData target = combatManager.teamOne[Random.Range(0, combatManager.teamOne.Count)];
                    combatFunctions.Attack(ownData, target);
                }

                else if (moves[turnNumber] == "tricky Entrance")
                {
                    if (dialogueCooldown <= 0 && dialogueStage == 0)
                    {
                        combatManager.combatPauseCooldown = 5;
                        dialogueCooldown = 4;
                        combatUI.combatText.text = "<b>WARNING: REALITY BREACH DETECTED</b>";
                        combatUI.combatText.color = Color.red;
                        dialogueStage += 1;
                    }

                    if (dialogueCooldown <= 0 && dialogueStage == 1)
                    {
                        combatManager.combatPauseCooldown = 5;
                        dialogueCooldown = 4;
                        dialogueStage += 1;
                        combatUI.combatText.text = "<b>SYSTEM OVERLOAD: THERE CAN ONLY BE ONE CLOWN</b>";
                    }

                    if (dialogueCooldown <= 0 && dialogueStage == 2)
                    {
                        combatManager.combatPauseCooldown = 5;
                        dialogueCooldown = 4;
                        dialogueStage += 1;

                        combatUI.combatText.text = "<b>TRICKSTER CLASS: DEPRECATED</b>";
                    }

                    if (dialogueCooldown <= 0 && dialogueStage == 3)
                    {
                        GameObject combatant5 = Instantiate(tricky, combatManager.spawnSlots[4].position, Quaternion.identity);
                        CharacterData combatant5Data = combatant5.GetComponent<CharacterData>();

                        combatant5.transform.SetParent(combatManager.spawnSlots[4]);
                        combatManager.combatants.Add(combatant5Data);
                        combatant5Data.team = 1;
                        combatManager.teamTwo.Add(combatant5Data);
                        combatant5Data.turnCoolDown = 5;

                        combatManager.combatPauseCooldown = 4;
                        dialogueCooldown = 4;
                        dialogueStage += 1;

                        combatUI.combatText.text = "HOLY FUCKING SHIT IT IS TRICKY FROM MADNESS COMBAT WITH THE STEEL STOP SIGN!!!!!";
                        combatUI.combatText.color = Color.black;
                    }
                }
            }

            else
            {
                CharacterData target = combatManager.teamOne[Random.Range(0, combatManager.teamOne.Count)];
                combatFunctions.Attack(ownData, target);
                Debug.Log("No moves left");
            }

            if (dialogueCooldown <= 0)
            {
                combatManager.turnHaver = null;
                turnNumber += 1;
            }
        }
    }
}