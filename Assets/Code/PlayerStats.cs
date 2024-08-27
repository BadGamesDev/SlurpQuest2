using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour //I feel like "PlayerData" would have been a more appropriate name but I'll keep this just out of sentimentality 
{
    public int level;
    public int xpTreshold;
    public int xp;
    public int noLifePoints;

    public List<CompanionData> unlockedCompanions;
    public List<CompanionData> activeCompanions;

    public int pizza; //I would have made an inventory script instead but considering I only need one inventory and there aren't any complicated mechanics needed I have just decided to make every item a variable
    public int catFood;
    public int gamblingChip;

    private void Start()
    {
        xpTreshold = 500;

        CompanionData slurp = new() //Yes... Slurp is Slurp's companion...
        {
            characterName = "Slurp",
            level = 0,
            maxHealth = 100,
            health = 100,
            defence = 0,
            accuracy = 90,
            damage = 10,
            speed = 10,
            turnCoolDown = 3000,
        };
        unlockedCompanions.Add(slurp);
        unlockedCompanions[0].skills.Add(SkillDatabase.sevenYears);
        unlockedCompanions[0].skills.Add(SkillDatabase.dancingMaster);
    }

    public void GainXP(int amount)
    {
        xp += amount;
        if (xp >= xpTreshold)
        {
            xp -= xpTreshold;
            xpTreshold *= 2;
            level += 1;
        }
    }
}
