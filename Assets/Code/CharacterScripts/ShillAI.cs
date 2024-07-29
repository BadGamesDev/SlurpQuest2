using UnityEngine;

public class ShillAI : MonoBehaviour
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
                combatFunctions.Attack(ownData, target);
                turnSinceSucc += 1;
            }
            else
            {
                combatFunctions.UseSkill(combatManager.teamTwo, ownData, combatManager.teamOne, target, SkillDatabase.empGrenade);
                turnSinceSucc = 0;
            }
            combatManager.turnHaver = null;
        }
    }
}
