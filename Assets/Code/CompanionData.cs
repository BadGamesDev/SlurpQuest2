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
            dodge += 1;
            damage += 6;
            accuracy += 5;
            speed += 1;

            if (level == 0)
            {
                skills.Add(SkillDatabase.tedTalk);
            }

            if (level == 1)
            {
                skills.Add(SkillDatabase.raid);
            }

            if (level == 2)
            {
                skills.Add(SkillDatabase.finalForm);
            }

            level += 1;
        }

        else if (characterName == "Honey")
        {
            maxHealth += 25;
            health += 25;
            defence += 0;
            dodge += 2;
            damage += 8;
            accuracy += 5;
            speed += 2;

            if (level == 0)
            {
                skills.Add(SkillDatabase.justBeCute);
            }

            if (level == 1)
            {
                skills.Add(SkillDatabase.snuggle);
            }

            if (level == 2)
            {
                skills.Add(SkillDatabase.ultraInstinct);
            }

            level += 1;
        }

        else if (characterName == "Digi63")
        {
            maxHealth += 45;
            health += 45;
            defence += 2;
            dodge += 0;
            damage += 5;
            accuracy += 5;
            speed += 0;

            if (level == 0)
            {
                skills.Add(SkillDatabase.silence);
            }

            if (level == 1)
            {
                skills.Add(SkillDatabase.banHammer);
            }

            if (level == 2)
            {
                skills.Add(SkillDatabase.findBigfoot);
            }

            level += 1;
        }

        else if (characterName == "Jaydizz")
        {
            maxHealth += 30;
            health += 30;
            defence += 0;
            dodge += 4;
            damage += 6;
            accuracy += 4;
            speed += 3;

            if (level == 0)
            {
                skills.Add(SkillDatabase.burnout);
            }

            if (level == 1)
            {
                skills.Add(SkillDatabase.pitStop);
            }

            if (level == 2)
            {
                skills.Add(SkillDatabase.dizzOrNoDizz);
            }

            level += 1;
        }

        else if (characterName == "Cndk99")
        {
            maxHealth += 35;
            health += 35;
            defence += 1;
            dodge += 1;
            damage += 5;
            accuracy += 5;
            speed += 1;

            if (level == 0)
            {
                skills.Add(SkillDatabase.clownmaxxing);
            }

            if (level == 1)
            {
                skills.Add(SkillDatabase.godComplex);
            }

            if (level == 2)
            {
                skills.Add(SkillDatabase.extremeLaziness);
            }

            level += 1;
        }

        else if (characterName == "OneViolence")
        {
            maxHealth += 40;
            health += 40;
            defence += 1;
            dodge += 2;
            damage += 4;
            accuracy += 5;
            speed += 0;

            if (level == 0)
            {
                skills.Add(SkillDatabase.corpsePaint);
            }

            if (level == 1)
            {
                skills.Add(SkillDatabase.onePeace);
            }

            if (level == 2)
            {
                skills.Add(SkillDatabase.oneViolence);
            }

            level += 1;
        }
    }
}
