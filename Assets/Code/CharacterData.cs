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
    public List<Skill> skills;
    public Dictionary<string, int> skillCooldowns;


    private void Start()
    {
        skills.Add(SkillDatabase.swipe);
    }
}
