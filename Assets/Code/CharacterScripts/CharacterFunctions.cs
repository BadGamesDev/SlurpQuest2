using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class CharacterFunctions : MonoBehaviour
{
    public CharacterData ownData;
    public CharacterUI ownUI;
    public CombatManager combatManager;
    public CombatUI combatUI;

    private void Start()
    {
        ownUI.healthBar.maxValue = ownData.maxHealth;
        ownUI.healthBar.value = ownData.health;
        ownUI.cooldownBar.maxValue = ownData.turnCoolDown;
        ownUI.healthText.text = ownData.health.ToString();
        combatManager = FindObjectOfType<CombatManager>();
        combatUI = FindObjectOfType<CombatUI>(); 
    }

    public void ChangeMaxHealth(int amount)
    {
        ownData.maxHealth = amount;
        ownData.health = amount;
        ownUI.healthBar.maxValue = amount;
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

        Transform combatSlot = transform.parent;
        TMP_Text damageText = combatSlot.Find("damageText").GetComponent<TMP_Text>();
        damageText.transform.SetAsLastSibling();
        damageText.text = healAmount.ToString();
        damageText.color = Color.green;

        ownUI.UpdateHealthBar(ownData.health);
    }

    public void TakeDamage(int damageAmount, bool trueDamage) //The fact that I don't substract the defence here causes a FUCKTON of problems, I don't even remember why I did it like this but god do I fucking hate that I did it like this.
    {
        if (!ownData.invulnerable)
        {
            foreach (StatusEffect status in ownData.globalStatusEffects)
            {
                if (status.statusName == "raid target")
                {
                    damageAmount *= 2;
                }
            }

            if (!trueDamage)
            {
                damageAmount -= ownData.defence;

                foreach (StatusEffect status in ownData.globalStatusEffects)
                {
                    if (status.statusName == "ted audience")
                    {
                        damageAmount += Convert.ToInt32(ownData.defence / 2);
                    }
                }
            }

            if (damageAmount < 0)
            {
                damageAmount = 0;
            }

            ownData.health -= damageAmount;
            ownUI.UpdateHealthBar(ownData.health);

            Transform combatSlot = transform.parent;
            TMP_Text damageText = combatSlot.Find("damageText").GetComponent<TMP_Text>();
            damageText.transform.SetAsLastSibling();
            damageText.text = damageAmount.ToString();
            damageText.color = Color.red;

            if (ownData.health <= 0)
            {
                Die();
            }
        }
        else
        {
            ownData.health -= 0;
        }
    }

    public void GetInflicted(string status, int duration) //You know what? I'm actually kinda proud of this system. Even thought the disgusting trend of a billion if checks continues here too.
    {
        if (ownData.characterName != "Digi63")
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

            else if (status == "ted audience")
            {
                existingStatus = CheckStatusGlobal(status);
                if (existingStatus == null)
                {
                    StatusEffect newEffect = new StatusEffect
                    {
                        statusName = StatusEffectDatabase.tedAudience.statusName,
                        tickCooldown = StatusEffectDatabase.tedAudience.tickCooldown,
                        tickCount = duration
                    };

                    ownData.globalStatusEffects.Add(newEffect);
                }
                else
                {
                    existingStatus.tickCount = duration;
                }
            }

            else if (status == "raid target")
            {
                existingStatus = CheckStatusGlobal(status);
                if (existingStatus == null)
                {
                    StatusEffect newEffect = new StatusEffect
                    {
                        statusName = StatusEffectDatabase.raidTarget.statusName,
                        tickCooldown = StatusEffectDatabase.raidTarget.tickCooldown,
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

            else if (status == "thottery")
            {
                existingStatus = CheckStatusSelf(status);
                if (existingStatus == null)
                {
                    StatusEffect newEffect = new StatusEffect
                    {
                        statusName = StatusEffectDatabase.thottery.statusName,
                        tickCount = duration
                    };

                    ownData.selfStatusEffects.Add(newEffect);
                }
                else
                {
                    existingStatus.tickCount = duration;
                }
            }

            else if (status == "clownmaxxing")
            {
                existingStatus = CheckStatusSelf(status);
                if (existingStatus == null)
                {
                    StatusEffect newEffect = new StatusEffect
                    {
                        statusName = StatusEffectDatabase.clownmaxxing.statusName,
                        tickCount = duration
                    };

                    ownData.selfStatusEffects.Add(newEffect);
                }
                else
                {
                    existingStatus.tickCount = duration;
                }
            }

            else if (status == "return to trad")
            {
                existingStatus = CheckStatusSelf(status);
                if (existingStatus == null)
                {
                    StatusEffect newEffect = new StatusEffect
                    {
                        statusName = StatusEffectDatabase.tradwis.statusName,
                        tickCount = duration
                    };

                    ownData.selfStatusEffects.Add(newEffect);
                }
                else
                {
                    existingStatus.tickCount = duration;
                }
            }

            else if (status == "corpse paint")
            {
                existingStatus = CheckStatusGlobal(status); //It reallt doesn't matter whether this one is global or self
                if (existingStatus == null)
                {
                    StatusEffect newEffect = new StatusEffect
                    {
                        statusName = StatusEffectDatabase.corpsePaint.statusName,
                        tickCount = duration
                    };

                    ownData.globalStatusEffects.Add(newEffect);
                }
                else
                {
                    existingStatus.tickCount = duration;
                }
            }

            else if (status == "one peace")
            {
                existingStatus = CheckStatusSelf(status);
                if (existingStatus == null)
                {
                    StatusEffect newEffect = new StatusEffect
                    {
                        statusName = StatusEffectDatabase.onePeace.statusName,
                        tickCount = duration
                    };

                    ownData.selfStatusEffects.Add(newEffect);
                }
                else
                {
                    existingStatus.tickCount = duration;
                }
            }

            else if (status == "permacloud")
            {
                existingStatus = CheckStatusSelf(status);
                if (existingStatus == null)
                {
                    StatusEffect newEffect = new StatusEffect
                    {
                        statusName = StatusEffectDatabase.permacloud.statusName,
                        tickCount = duration
                    };

                    ownData.selfStatusEffects.Add(newEffect);
                }
                else
                {
                    existingStatus.tickCount = duration;
                }
            }

            else if (status == "cute")
            {
                existingStatus = CheckStatusGlobal(status);
                if (existingStatus == null)
                {
                    StatusEffect newEffect = new StatusEffect
                    {
                        statusName = StatusEffectDatabase.cute.statusName,
                        tickCount = duration
                    };

                    ownData.globalStatusEffects.Add(newEffect);
                }
                else
                {
                    existingStatus.tickCount = duration;
                }
            }

            else if (status == "ultra instinct")
            {
                existingStatus = CheckStatusGlobal(status);
                if (existingStatus == null)
                {
                    StatusEffect newEffect = new StatusEffect
                    {
                        statusName = StatusEffectDatabase.ultraInstinct.statusName,
                        tickCount = duration
                    };

                    ownData.globalStatusEffects.Add(newEffect);
                }
                else
                {
                    existingStatus.tickCount = duration;
                }
            }

            else if (status == "simp")
            {
                existingStatus = CheckStatusSelf(status);
                
                if (existingStatus == null)
                {
                    StatusEffect newEffect = new StatusEffect
                    {
                        statusName = StatusEffectDatabase.simp.statusName,
                        tickCount = duration
                    };

                    ownData.selfStatusEffects.Add(newEffect);
                }
                else
                {
                    existingStatus.tickCount = duration;
                }
            }

            else if (status == "silence")
            {
                existingStatus = CheckStatusGlobal(status);
                if (existingStatus == null)
                {
                    StatusEffect newEffect = new StatusEffect
                    {
                        statusName = StatusEffectDatabase.silence.statusName,
                        tickCount = duration
                    };

                    ownData.globalStatusEffects.Add(newEffect);
                }
                else
                {
                    existingStatus.tickCount = duration;
                }
            }

            else if (status == "rapped")
            {
                existingStatus = CheckStatusGlobal(status);
                if (existingStatus == null)
                {
                    StatusEffect newEffect = new StatusEffect
                    {
                        statusName = StatusEffectDatabase.rapped.statusName,
                        tickCount = duration
                    };

                    ownData.globalStatusEffects.Add(newEffect);
                }
                else
                {
                    existingStatus.tickCount = duration;
                }
            }

            ownUI.UpdateStatusIcons();
        }
    }

    public void StatusTick(string status)
    {
        if (status == "stun")
        {
            combatManager.turnHaver = null;
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
        ownData.turnCoolDown = 3000; //magic number but it's ok
        ownUI.UpdateCooldownBar(ownData.turnCoolDown);
    }

    public void Die()
    {
        if (ownData.characterName == "Asmongold" && ownData.secondPhase == false)
        {
            ownData.secondPhase = true;

            GetHealed(99999);
            GetInflicted("permacloud", 999);

            combatManager.combatPauseCooldown = 6;
            combatUI.combatText.text = "Asmongold: Did you really think it would be so easy? Did you really think that cndk wouldn't add a second phase for the final boss fight? *Asmongold activated permanent cloud of decay*";
            combatUI.asmonCloud.SetActive(true);
        }

        else
        {
            if (ownData.characterName == "OneViolence")
            {
                combatManager.peace = false; //Just incase OV manages to die while there is peace. Because if he is dead peace will never run out and combat will never end. 
            }
            combatManager.xpReward += ownData.xpReward;
            combatManager.combatants.Remove(ownData);

            if (ownData.team == 0)
            {
                combatManager.teamOne.Remove(ownData);
            }
            
            if (ownData.team == 1)
            {
                combatManager.teamTwo.Remove(ownData);
            }

            if (ownData.characterName == "Big Foot")
            {
                combatManager.combatPauseCooldown += 2.5f;
                combatUI.combatText.text = "Bigfoot got defeated. But " + combatManager.bench[0].GetComponent<CharacterData>().characterName + " is here to continue the fight!";
                combatManager.PullFromBench(combatManager.bench[0]); //STEP 1: over engineer a system. STEP 2: make a retarded function like this to completely make the over engineering obsolete.
            }

            if (ownData.characterName != "Big Foot")
            {
                if (combatManager.teamOne.Count == 0)
                {
                    combatManager.LoseCombat();
                }

                if (combatManager.teamTwo.Count == 0)
                {
                    combatManager.WinCombat();
                }
            }

            foreach(CharacterData combatant in combatManager.combatants)
            {
                if (combatant.characterName == "OneViolence")
                {
                    combatant.GetComponent<CharacterFunctions>().GetHealed(combatant.maxHealth);
                }
            }

            Destroy(gameObject);
        }
    }
}
