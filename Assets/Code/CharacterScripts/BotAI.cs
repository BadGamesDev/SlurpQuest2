using UnityEngine;

public class BotAI : MonoBehaviour
{
    public CombatManager combatManager;
    public CombatFunctions combatFunctions;
    public CharacterData ownData;

    public int turnSinceSucc;

    void Start()
    {
        combatManager = FindObjectOfType<CombatManager>();
        combatFunctions = FindObjectOfType<CombatFunctions>();
    }

    public void FixedUpdate()
    {
        if (combatManager.turnHaver == ownData && !ownData.selfStatusEffects.Contains(StatusEffectDatabase.stun) && ownData.team == 1 && combatManager.teamOne.Count != 0)
        {
            CharacterData target = combatManager.teamOne[Random.Range(0, combatManager.teamOne.Count)];
            if (turnSinceSucc < 3)
            {
                combatFunctions.UseSkill(combatManager.teamTwo, ownData, combatManager.teamOne, target, SkillDatabase.empGrenade);
            }
            else
            {
                combatFunctions.Attack(ownData, target);
            }
            combatManager.turnHaver = null;
        }
    }
}
