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

    public void LevelUp()
    {
        if (characterName == "Slurp")
        {
            maxHealth += 40;
            health += 40;
            defence += 0;
            dodge += 5;
            damage += 8;
            accuracy += 0;
            speed += 1;

            if (level == 1)
            {
                skills.Add(SkillDatabase.tedTalk); 
            }

            if (level == 2)
            {
                skills.Add(SkillDatabase.raid);
            }

            if (level == 3)
            {
                skills.Add(SkillDatabase.finalForm);
            }

            level += 1;
        }

        else if (characterName == "Honey")
        {
            maxHealth += 30;
            health += 30;
            defence += 0;
            dodge += 5;
            damage += 5;
            accuracy += 5;
            speed += 2;

            if (level == 1)
            {
                skills.Add(SkillDatabase.justBeCute);
            }

            if (level == 2)
            {
                skills.Add(SkillDatabase.Snuggle);
            }

            if (level == 3)
            {
                skills.Add(SkillDatabase.ultraInstinct);
            }
        }

        //else if (characterName == "Digi63")
        //{
        //    maxHealth += 30;
        //    health += 30;
        //    defence += 0;
        //    dodge += 0;
        //    damage += 0;
        //    accuracy += 0;
        //    speed += 0;

        //    if (level == 1)
        //    {
        //        skills.Add();
        //    }

        //    if (level == 2)
        //    {
        //        skills.Add();
        //    }

        //    if (level == 3)
        //    {
        //        skills.Add();
        //    }
        //}

        //else if (characterName == "Jaydizz")
        //{
        //    maxHealth += 30;
        //    health += 30;
        //    defence += 0;
        //    dodge += 0;
        //    damage += 0;
        //    accuracy += 0;
        //    speed += 0;

        //    if (level == 1)
        //    {
        //        skills.Add();
        //    }

        //    if (level == 2)
        //    {
        //        skills.Add();
        //    }

        //    if (level == 3)
        //    {
        //        skills.Add();
        //    }
        //}

        //else if (characterName == "Cndk99")
        //{
        //    maxHealth += 30;
        //    health += 30;
        //    defence += 0;
        //    dodge += 0;
        //    damage += 0;
        //    accuracy += 0;
        //    speed += 0;

        //    if (level == 1)
        //    {
        //        skills.Add();
        //    }

        //    if (level == 2)
        //    {
        //        skills.Add();
        //    }

        //    if (level == 3)
        //    {
        //        skills.Add();
        //    }
        //}

        //else if (characterName == "OneViolence")
        //{
        //    maxHealth += 30;
        //    health += 30;
        //    defence += 0;
        //    dodge += 0;
        //    damage += 0;
        //    accuracy += 0;
        //    speed += 0;

        //    if (level == 1)
        //    {
        //        skills.Add();
        //    }

        //    if (level == 2)
        //    {
        //        skills.Add();
        //    }

        //    if (level == 3)
        //    {
        //        skills.Add();
        //    }

    }
}
