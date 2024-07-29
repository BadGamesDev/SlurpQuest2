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
                StatusEffect newEffect = new StatusEffect //REMINDER TO ALWAYS MAKE SURE YOU ARE CREATING A NEW INSTANCE.... HOLY FUCKING SHIT I HAVE NEVER WANTED TO KILL MYSELF SO FUCKING MUCH, LITERALLY ALMOST GAVE UP ON MAKING THE GAME BECAUSE OF SUCH A SMALL ERROR, Ideally I also need to to a consturctor or copy method or some shit like this but this solution works
                {
                    statusName = StatusEffectDatabase.stun.statusName,
                    tickCount = duration
                }; 

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
                StatusEffect newEffect = new StatusEffect
                {
                    statusName = StatusEffectDatabase.bleed.statusName, //oh also, yes, I am writing this shit instead of just writing the name as a string which would be way shorter. Why? because fuck you that's why.
                    tickCooldown = StatusEffectDatabase.bleed.tickCooldown,
                    tickCount = duration
                };

                ownData.globalStatusEffects.Add(newEffect);
            }
            else
            {
                existingStatus.tickCount = duration;
            }
        }

        else if (status == "engine started")
        {
            existingStatus = CheckStatusGlobal(status);
            if (existingStatus == null)
            {
                StatusEffect newEffect = new StatusEffect
                {
                    statusName = StatusEffectDatabase.engineStarted.statusName,
                    tickCount = duration
                };

                ownData.globalStatusEffects.Add(newEffect); //not sure if this buff should be self or global tbh, I guess global makes more sense in terms of realism but self feels more balanced (decided on global, I think it is cool that you get even more turns from the buff as your character gets faster by leveling up) 
                ownData.speed *= 2;
            }
            else
            {
                existingStatus.tickCount = duration;
            }
        }

        else if (status == "burnout smoke")
        {
            existingStatus = CheckStatusGlobal(status);
            if (existingStatus == null)
            {
                StatusEffect newEffect = new StatusEffect
                {
                    statusName = StatusEffectDatabase.burnoutSmoke.statusName,
                    tickCount = duration
                };

                ownData.globalStatusEffects.Add(newEffect);
                ownData.dodge += 60;
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
        ownData.turnCoolDown = 4000; //magic number but it's ok
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
            combatManager.PullFromBench(combatManager.bench[0]); //STEP 1: over engineer a system. STEP 2: make a retarded function like this to completely make the over engineering obsolete.
        }

        Destroy(gameObject);
    }
}
