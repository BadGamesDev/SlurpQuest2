using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    public string characterName;
    public int maxHealth;
    public int health;
    public int defence;
    public int accuracy;
    public int damage;
    public int speed;
    public int turnCoolDown;
    public int team;
    public int xpReward;
    public string winEvent; //I will probably need to make a separate "deathEvent" in the future as I might need to fire some shit at the death of the enemy rather than end of combat
    public string loseEvent;
    public List<Skill> skills;
    public Dictionary<string, int> skillCooldowns;
    public List<StatusEffect> selfStatusEffects;
    public List<StatusEffect> globalStatusEffects;

    public int bossNumber; //This is for boss death specific events;

    private void Start()
    {
        skills.Add(SkillDatabase.swipe);
    }
}
