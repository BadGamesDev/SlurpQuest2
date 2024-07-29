using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CombatFunctions : MonoBehaviour
{
    public CombatManager combatManager;
    public CombatUI combatUI;

    public void Attack(CharacterData attacker, CharacterData target) //might be a good idea to directly get functions instead of data but I might also need the data so this is fine for now
    {  
        int attackRoll = Random.Range(1, 101);
        int chance = attacker.accuracy - target.dodge;

        if (attackRoll <= chance)
        {
            target.GetComponent<CharacterFunctions>().TakeDamage(attacker.damage - target.defence);
            combatUI.combatText.text = (attacker.characterName + " attacked " + target.characterName + ".");
        }
        else
        {
            combatUI.combatText.text = (attacker.characterName + " attacked " + target.characterName + " but missed.");
        }
        combatManager.combatPauseCooldown = 1.8f;
    }

    public void UseSkill(List<CharacterData> userTeam, CharacterData skillUser, List<CharacterData> enemyTeam, CharacterData target, Skill skill) //I will use if checks for each skill, this is quite bad and it also requires me to write hundreds of ugly lines but it is also quite simple. I want to make a finished game not a pretty codebase.
    {
        if (skill.skillName == "Dancing Master")
        {
            int roll = Random.Range(0, 10);

            if (roll == 0)
            {
                skillUser.GetComponent<CharacterFunctions>().TakeDamage(skillUser.damage * 2 - skillUser.defence);
                combatUI.combatText.text = "Slurp tried doing some dance moves... and hit herself.";
            }

            else
            { 
                target.GetComponent<CharacterFunctions>().TakeDamage(skillUser.damage * 2 - target.defence);
                combatUI.combatText.text = "Slurp did some amazing dance moves and hit the " + target.characterName;
            }

            skillUser.skill1Cooldown = 2;
        }

        else if (skill.skillName == "Ted Talk")
        {
            List<CharacterData> charactersToKill = new();
            
            int roll = Random.Range(0, 8);

            if (roll == 0)
            {
                foreach (CharacterData character in userTeam)
                {

                    if (character.health <= skillUser.damage - target.defence)
                    {
                        charactersToKill.Add(character);
                    }
                    else
                    {
                        character.GetComponent<CharacterFunctions>().TakeDamage(skillUser.damage - target.defence);
                        character.GetComponent<CharacterFunctions>().GetInflicted("tedTalk", 2);
                    }
                }
            }
            
            foreach (CharacterData character in enemyTeam)
            {

                if (character.health <= skillUser.damage - target.defence)
                {
                    charactersToKill.Add(character);
                }
                else
                {
                    character.GetComponent<CharacterFunctions>().TakeDamage(skillUser.damage - target.defence);
                    character.GetComponent<CharacterFunctions>().GetInflicted("tedTalk", 2);
                }
            }
        }
        
        else if(skill.skillName == "Raid")
        {
            target.GetComponent<CharacterFunctions>().GetInflicted("raidTarget", 2);
        }
        
        else if(skill.skillName == "Final Form")
        {

        }

        else if (skill.skillName == "Swipe")
        {
            List<CharacterData> charactersToKill = new ();
            foreach(CharacterData character in enemyTeam)
            {

                if (character.health <= skillUser.damage - target.defence)
                {
                    charactersToKill.Add(character);
                }
                else
                {
                    character.GetComponent<CharacterFunctions>().TakeDamage(skillUser.damage - target.defence);
                    character.GetComponent<CharacterFunctions>().GetInflicted("bleed", 2);
                }
            }
            if (charactersToKill.Count > 0)
            {
                foreach(CharacterData yeetCandidate in charactersToKill)
                {
                    yeetCandidate.GetComponent<CharacterFunctions>().Die();
                }
            }
        }

        else if (skill.skillName == "Just Be Cute")
        {
            skillUser.defence += 5 * (skillUser.level + 1);
            skillUser.GetComponent<CharacterFunctions>().GetInflicted("cute", 3);
        }

        else if (skill.skillName == "Snuggle")
        {
            target.GetComponent<CharacterFunctions>().GetHealed(20 + (10 * (skillUser.level + 1)));
        }

        else if (skill.skillName == "Ultra Instinct")
        {
            skillUser.damage *= 2;
            skillUser.speed += 5 + skillUser.level * 2;
            skillUser.GetComponent<CharacterFunctions>().GetInflicted("Ultra Instinct", 2);
        }
        
        else if (skill.skillName == "EMP Grenade")
        {
            target.GetComponent<CharacterFunctions>().GetInflicted("stun", 2);
        }
        
        else if (skill.skillName == "Find Bigfoot")
        {
            int roll = Random.Range(2, 5);
            skillUser.bigFootTurns = roll;
            combatManager.AddToBench(skillUser.gameObject);
        }

        else if (skill.skillName == "Start Your Engines")
        {
            skillUser.GetComponent<CharacterFunctions>().GetInflicted("engine started", 4);
        }
        
        else if (skill.skillName == "Burnout")
        {
            skillUser.GetComponent<CharacterFunctions>().GetInflicted("burnout smoke", 2);
        }
        
        else if (skill.skillName == "Pit Stop")
        {
            skillUser.GetComponent<CharacterFunctions>().GetHealed(skillUser.maxHealth - skillUser.health); //heal method already fixes any overhealing but I want to calculate it correctly incase I want to show it in UI or something.
        }
        
        else if (skill.skillName == "Dizz Or No Dizz")
        {
            combatUI.ownTeam = userTeam; 
            combatUI.enemyTeam = enemyTeam;

            HashSet<int> uniqueNumbers = new HashSet<int>();

            while (uniqueNumbers.Count < 3)
            {
                uniqueNumbers.Add(Random.Range(1, 10));
            }

            int[] uniqueArray = uniqueNumbers.ToArray();
            combatUI.dizz0effect = uniqueArray[0];
            combatUI.dizz1effect = uniqueArray[1];
            combatUI.dizz2effect = uniqueArray[2];

            combatManager.gameState.combatPaused = true;
            combatUI.dizz0.gameObject.SetActive(true);
            combatUI.dizz1.gameObject.SetActive(true);
            combatUI.dizz2.gameObject.SetActive(true);
        }
        
        else if (skill.skillName == "Suck Life")
        {
            target.GetComponent<CharacterFunctions>().TakeDamage(skillUser.damage - target.defence);
            skillUser.GetComponent<CharacterFunctions>().GetHealed(skillUser.damage - target.defence);
        }

        combatManager.combatPauseCooldown = 1.8f;
    }

    public void UseItem(List<CharacterData> userTeam, CharacterData itemUser, List<CharacterData> enemyTeam, CharacterData target, string item)//Once again the if checks are absolutely disgusting
    {
        if (item == "catFood")
        {
            if(target.characterName == "FeralCat")
            {
                target.characterName = "Honey";
                target.GetComponent<CharacterFunctions>().ChangeMaxHealth(100);

                combatManager.WinCombat();
            }
            
            else if (target.characterName == "Honey")
            {
                combatUI.combatText.text = "Honey loves it! She got healed by the cat food.";
            }

            else
            {
                combatUI.combatText.text = "the cat food had absolutely no effect";
            }
        }
        combatManager.combatPauseCooldown = 1.8f;
    }

    public void EndTurn(CharacterData turnSpender) //truly amazing name
    {
        //what am I supposed to use this method for lmao
    }
}
