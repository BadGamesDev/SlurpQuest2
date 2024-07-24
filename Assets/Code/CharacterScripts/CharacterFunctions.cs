using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFunctions : MonoBehaviour
{
    public CharacterData ownData;
    public CharacterUI ownUI;
    public CombatManager combatManager;

    private void Start()
    {
        ownUI.healthBar.text = ownData.health.ToString();
        ownUI.cooldownBar.text = ownData.turnCoolDown.ToString();
        combatManager = FindObjectOfType<CombatManager>();
    }

    public void ChangeMaxHealth(int amount)
    {
        if(ownData.health > amount)
        {
            ownData.health = amount;
        }
        ownUI.UpdateHealthBar(ownData.health);
    }

    public void GetHealed(int healAmount)
    {
        if(ownData.health + healAmount > ownData.maxHealth) 
        { 
            ownData.health = ownData.maxHealth;
        }
        else 
        {
            ownData.health += healAmount;
        }
        ownUI.UpdateHealthBar(ownData.health);
    }

    public void TakeDamage(int damageAmount)
    {
        ownData.health -= damageAmount;
        ownUI.UpdateHealthBar(ownData.health);
        
        if (ownData.health <= 0)
        {
            Die();
        }
    }

    public void GetInflicted(string status, int duration) //You know what? I'm actually kinda proud of this system. Even thought the disgusting trend of a billion if checks continues here too.
    {
        StatusEffect existingStatus;
        if (status == "stun")
        {
            existingStatus = CheckStatusSelf(status); //I can improve the method to remove the need for putting it separately for each skill but why fix something that is not broken?
            if (existingStatus == null)
            {
                StatusEffect newEffect = StatusEffectDatabase.stun;
                newEffect.tickCount = duration;

                ownData.selfStatusEffects.Add(newEffect);
            }
            else
            {
                existingStatus.tickCount = duration;
            }
        }
        
        else if (status == "bleed")
        {
            existingStatus = CheckStatusGlobal(status);
            if (existingStatus == null)
            {
                StatusEffect newEffect = StatusEffectDatabase.bleed;
                newEffect.tickCount = duration;

                ownData.globalStatusEffects.Add(newEffect);
            }
            else
            {
                existingStatus.tickCount = duration;
            }
        }
    }

    public void StatusTick(string status)
    {
        if (status == "stun")
        {
            combatManager.turnHaver = null;
        }
        else if (status == "bleed")
        {
            TakeDamage(7);   
        }
    }

    public StatusEffect CheckStatusGlobal(string statusName)
    {
        foreach (StatusEffect status in ownData.globalStatusEffects)
        {
            if (statusName == status.statusName)
            {
                return status;
            }
        }
        return null;
    }
    public StatusEffect CheckStatusSelf(string statusName)
    {
        foreach (StatusEffect status in ownData.selfStatusEffects)
        {
            if (statusName == status.statusName)
            {
                return status;
            }
        }
        return null;
    }

    public void ReduceTurnCooldown(int amount)
    {
        ownData.turnCoolDown -= amount;
        ownUI.UpdateCooldownBar(ownData.turnCoolDown);
    }

    public void ResetTurnCooldown()
    {
        ownData.turnCoolDown = 5000; //magic number but it's ok
        ownUI.UpdateCooldownBar(ownData.turnCoolDown);
    }

    public void Die()
    {
        combatManager.xpReward += ownData.xpReward;
        combatManager.combatants.Remove(ownData);

        if (ownData.team == 0)
        {
            combatManager.teamOne.Remove(ownData);
        }
        else if(ownData.team == 1)
        {
            combatManager.teamTwo.Remove(ownData);
        }

        if(combatManager.teamOne.Count == 0)
        {
            combatManager.LoseCombat();
        }

        if(combatManager.teamTwo.Count == 0)
        {
            combatManager.WinCombat();
        }

        if(ownData.characterName == "Big Foot")
        {
            combatManager.PullFromBench(combatManager.bench[0]); //STEP 1: over engineer a system. STEP 2: make a retarded function like this to completely make the over engineering useless.
        }

        Destroy(gameObject);
    }
}
