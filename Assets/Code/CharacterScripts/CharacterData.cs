using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    public PlayerStats playerStats;
    public string characterName;
    public int level;
    public int maxHealth;
    public int health;
    public int defence;
    public int accuracy;
    public int dodge;
    public int damage;
    public int speed;
    public int turnCoolDown;
    public int team;
    public int xpReward;
    public string winEvent; //I will probably need to make a separate "deathEvent" in the future as I might need to fire some shit at the death of the enemy rather than end of combat
    public string loseEvent;
    public List<Skill> skills;

    public int skill1Cooldown;
    public int skill2Cooldown;
    public int skill3Cooldown;
    public int skill4Cooldown;

    public List<StatusEffect> selfStatusEffects;
    public List<StatusEffect> globalStatusEffects;
    public int bigFootTurns; //Having such a specific variable on every character really saddens me. It is inefficient and it is ugly. But it doesn't hurt too much and I really want to be done with coding.
    public Transform bigFootSlot; //same as above

    public bool isBoss;

    private void Start()
    {
        playerStats = FindAnyObjectByType<PlayerStats>();
        foreach (CompanionData companion in playerStats.unlockedCompanions)
        {
            if (companion.characterName == characterName)
            {
                for (int i = 0; i < companion.skills.Count; i++)
                {
                    skills.Add(companion.skills[i]);
                }

                level = companion.level;
                maxHealth = companion.maxHealth;
                health = companion.health;
                defence = companion.defence;
                dodge = companion.dodge;
                damage = companion.damage;
                accuracy = companion.accuracy;
                speed = companion.speed;
            }
        }
    }
}
