using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class CompanionData
{
    public string characterName;
    public int level;
    public int maxHealth;
    public int health;
    public int defence;
    public int dodge;
    public int damage;
    public int accuracy;
    public int speed;
    public int turnCoolDown;
    public List<Skill> skills = new List<Skill>();
}
