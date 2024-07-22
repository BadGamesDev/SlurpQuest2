using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAI : MonoBehaviour
{
    public CombatManager combatManager;
    public CombatFunctions combatFunctions;
    public CharacterData ownData;

    void Start()
    {
        combatManager = FindObjectOfType<CombatManager>();
        combatFunctions = FindObjectOfType<CombatFunctions>();
    }

    public void FixedUpdate()//horrible for performance
    {
        if (combatManager.turnHaver == ownData && ownData.team == 1 && combatManager.teamOne.Count != 0) //checking team like this is really dumb ------Edit: checking if the team is empty to prevent some bugs, some real vodoo shit tbh 
        {
            CharacterData target = combatManager.teamOne[Random.Range(0, combatManager.teamOne.Count)];
            combatFunctions.Attack(ownData, target);
            combatManager.turnHaver = null; //doing this manually everywhere is a bad idea, it should be in a proper function
            Debug.Log(target.maxHealth + target.health);
        }
    }
}
