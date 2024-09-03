using UnityEngine;

public class ShillAI : MonoBehaviour
{
    public CombatManager combatManager;
    public CombatFunctions combatFunctions;
    public CharacterData ownData;

    public int turnSinceSucc;
    public bool silence;

    void Start()
    {
        combatManager = FindObjectOfType<CombatManager>();
        combatFunctions = FindObjectOfType<CombatFunctions>();
        turnSinceSucc = 1;
    }

    public void FixedUpdate()
    {
        if (combatManager.turnHaver == ownData && !ownData.selfStatusEffects.Contains(StatusEffectDatabase.stun) && ownData.team == 1 && combatManager.teamOne.Count != 0)
        {
            silence = false;

            foreach(StatusEffect statusEffect in ownData.globalStatusEffects) //imagine how easy life would be if I just had a check status method.
            {
                if (statusEffect.statusName == "silence")
                {
                    silence = true;
                }
            }

            CharacterData target = combatManager.teamOne[Random.Range(0, combatManager.teamOne.Count)];
            if (turnSinceSucc >= 3 && silence == false)
            {
                combatFunctions.UseSkill(combatManager.teamTwo, ownData, combatManager.teamOne, target, SkillDatabase.suckLifeForce);
                turnSinceSucc = 0;
            }

            else
            {
                combatFunctions.Attack(ownData, target);
                turnSinceSucc += 1;
            }
            combatManager.turnHaver = null;
        }
    }
}
