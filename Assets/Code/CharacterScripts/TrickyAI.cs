using UnityEngine;

public class TrickyAI : MonoBehaviour
{
    public CombatManager combatManager;
    public CombatFunctions combatFunctions;
    public CharacterData ownData;

    void Start()
    {
        combatManager = FindObjectOfType<CombatManager>();
        combatFunctions = FindObjectOfType<CombatFunctions>();

        CharacterUI ownUI = GetComponent<CharacterUI>();

        ownUI.healthText.text = "CANNOT/DIE";
    }

    public void FixedUpdate()//horrible for performance
    {
        if (combatManager.turnHaver == ownData && !ownData.selfStatusEffects.Contains(StatusEffectDatabase.stun) && ownData.team == 1 && combatManager.teamOne.Count != 0) //checking team like this is really dumb ------Edit: checking if the team is empty to prevent some bugs, some real vodoo shit tbh 
        {
            CharacterData target = combatManager.teamTwo[0];
            combatFunctions.Attack(ownData, target);
            combatManager.turnHaver = null; //doing this manually everywhere is a bad idea, it should be in a proper function
            combatManager.WinCombat();
        }
    }
}
