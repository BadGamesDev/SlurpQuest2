using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
//everything should have a separate pause lenght as sometimes it is hard to read shit in time
public class CombatFunctions : MonoBehaviour
{
    public PlayerStats playerStats;
    public CombatManager combatManager;
    public CombatUI combatUI;

    public void Attack(CharacterData attacker, CharacterData target) //might be a good idea to directly get functions instead of data but I might also need the data so this is fine for now
    {  
        int attackRoll = UnityEngine.Random.Range(1, 101);
        int chance = attacker.accuracy - target.dodge;

        if (attackRoll <= chance)
        {
            target.GetComponent<CharacterFunctions>().TakeDamage(attacker.damage, false);
            combatUI.combatText.text = (attacker.characterName + " attacked " + target.characterName + ".");
            
            if(attacker.characterName == "Honey")
            {
                target.GetComponent<CharacterFunctions>().GetInflicted("bleed", 2);
            }
        }
        else
        {
            combatUI.combatText.text = (attacker.characterName + " attacked " + target.characterName + " but missed.");
        }
        combatManager.combatPauseCooldown = 1.8f;
    }

    public void UseSkill(List<CharacterData> userTeam, CharacterData skillUser, List<CharacterData> enemyTeam, CharacterData target, Skill skill) //I will use if checks for each skill, this is quite bad and it also requires me to write hundreds of ugly lines but it is also quite simple. I want to make a finished game not a pretty codebase.
    {
        if (skill.skillName == "Dancing Master") //DONE
        {
            int roll = UnityEngine.Random.Range(0, 10);

            if (roll == 0)
            {
                skillUser.GetComponent<CharacterFunctions>().TakeDamage(skillUser.damage * 2, false);
                combatUI.combatText.text = "Slurp tried doing some dance moves... and hit herself.";
            }

            else
            { 
                target.GetComponent<CharacterFunctions>().TakeDamage(skillUser.damage * 2, false);
                combatUI.combatText.text = "Slurp did some amazing dance moves and hit the " + target.characterName;
            }

            skillUser.skill1Cooldown = 2;
        }

        else if (skill.skillName == "Ted Talk") //DONE (beware of bugs)
        {
            List<CharacterData> charactersToKill = new();

            foreach (CharacterData character in enemyTeam)
            {

                if (character.health <= skillUser.damage - target.defence)
                {
                    charactersToKill.Add(character);
                }
                else
                {
                    character.GetComponent<CharacterFunctions>().TakeDamage(Convert.ToInt32(skillUser.damage / 2), false);
                    character.GetComponent<CharacterFunctions>().GetInflicted("ted audience", 2);
                }
            }

            int roll = UnityEngine.Random.Range(0, 6); //roll to see if ted talk friendly fire happens

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
                        character.GetComponent<CharacterFunctions>().TakeDamage(Convert.ToInt32(skillUser.damage / 2), false);
                        character.GetComponent<CharacterFunctions>().GetInflicted("ted audience", 2);
                    }
                }

                combatUI.combatText.text = "The speech was so fucking bad that even your team got effected. Great job Slurp!";
            }

            else        
            {
                combatUI.combatText.text = "Slurp gave a long ass Ted speech and demoralised the enemy, lowering their defence.";
            }

            skillUser.skill2Cooldown = 5;
        }
        
        else if(skill.skillName == "Raid") //DONE
        {
            target.GetComponent<CharacterFunctions>().GetInflicted("raid target", 1);
            combatUI.combatText.text = "Slurp told everyone to go and raid " + target.characterName;
            skillUser.skill3Cooldown = 4;
        }
        
        else if(skill.skillName == "Final Form") //dear god give me the strenght to exorcise the bugs from this skill EDIT: Done I guess?
        {
            combatManager.gameState.combatPaused = true;
            combatUI.slurp0.gameObject.SetActive(true);
            combatUI.slurp1.gameObject.SetActive(true);
            combatUI.slurp2.gameObject.SetActive(true);
            skillUser.skill4Cooldown = 999; //bruuuuuh
        }

        else if (skill.skillName == "Swipe") //DONE
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
                    character.GetComponent<CharacterFunctions>().TakeDamage(skillUser.damage, false);
                }
            }
            if (charactersToKill.Count > 0)
            {
                foreach(CharacterData yeetCandidate in charactersToKill)
                {
                    yeetCandidate.GetComponent<CharacterFunctions>().Die();
                }
            }
            combatUI.combatText.text = skillUser.characterName + " used swipe to hit all enemies.";
            skillUser.skill2Cooldown = 2;
        }

        else if (skill.skillName == "Just Be Cute") //DONE
        {
            skillUser.defence += 5 * (skillUser.level + 1);
            skillUser.GetComponent<CharacterFunctions>().GetInflicted("cute", 3);
            combatUI.combatText.text = skillUser.characterName + " is acting like the cute cat that she is. Enemies do not want to hurt her.";
            skillUser.skill2Cooldown = 6;
        }

        else if (skill.skillName == "Snuggle") //DONE
        {
            target.GetComponent<CharacterFunctions>().GetHealed(20 + (10 * (skillUser.level + 1)));
            combatUI.combatText.text = skillUser.characterName + " snuggled " + target.characterName + " helping them heal";
            skillUser.skill3Cooldown = 5;
        }

        else if (skill.skillName == "Ultra Instinct") //DONE
        {
            skillUser.damage += 15 + 8 * skillUser.level; //might be the worst case of hardcoding in this whole game
            skillUser.speed += 5 + skillUser.level * 2;
            skillUser.GetComponent<CharacterFunctions>().GetInflicted("ultra instinct", 2);
            combatUI.combatText.text = skillUser.characterName + " activated Ultra Instinct mode! Gods have mercy on her enemies";
            skillUser.skill4Cooldown = 999;
        }
        
        else if (skill.skillName == "EMP Grenade")
        {
            target.GetComponent<CharacterFunctions>().GetInflicted("stun", 2);
            combatUI.combatText.text = skillUser.characterName + " threw an EMP grenade at " + target.characterName;
            skillUser.skill1Cooldown = 5;
        }

        else if (skill.skillName == "Silence")
        {
            target.GetComponent<CharacterFunctions>().GetInflicted("silence", 3);
            if (!target.isBoss)
            {
                combatUI.combatText.text = skillUser.characterName + " silenced " + target.characterName + " they can not use any skills now!";
            }

            else
            {
                combatUI.combatText.text = "I am sorry Slurp. I know it makes sense to use this skill on bosses but I can not allow it as boss fights can get fucking broken if you use silence on them. That is why it did nothing. You just wasted a turn.";
            }
            
            skillUser.skill1Cooldown = 4;
        }

        else if (skill.skillName == "Ban Hammer")
        {
            if (target.health <= target.maxHealth * 0.3)
            {
                target.GetComponent<CharacterFunctions>().Die();
                combatUI.combatText.text = skillUser.characterName + " has PERMABANNED " + target.characterName;
            }
            else
            {
                combatUI.combatText.text = target.health + " is more than 30% of " + target.maxHealth + " please close the game and take some fucking math lessons.";
            }

            skillUser.skill1Cooldown = 0;
        }

        else if (skill.skillName == "Find Bigfoot")
        {
            int roll = UnityEngine.Random.Range(2, 5);
            skillUser.bigFootTurns = roll;
            combatManager.AddToBench(skillUser.gameObject);
            combatUI.combatText.text = skillUser.characterName + " went searching for something. Who knows when he will be back?";
            skillUser.skill4Cooldown = 999;
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
                uniqueNumbers.Add(UnityEngine.Random.Range(1, 10));
            }

            int[] uniqueArray = uniqueNumbers.ToArray();
            combatUI.dizz0effect = uniqueArray[0];
            combatUI.dizz1effect = uniqueArray[1];
            combatUI.dizz2effect = uniqueArray[2];

            combatManager.gameState.combatPaused = true;
            combatUI.dizz0.gameObject.SetActive(true);
            combatUI.dizz1.gameObject.SetActive(true);
            combatUI.dizz2.gameObject.SetActive(true);

            combatUI.combatText.text = skillUser.characterName + " used dizz or not dizz! Click the boxes one by one. Last one will be your reward!";
        }

        else if (skill.skillName == "Herbal Medicine")
        {
            target.GetComponent<CharacterFunctions>().GetHealed(20 + skillUser.level * 10);
            if (skillUser == target)
            {
                combatUI.combatText.text = skillUser.characterName + " used herbal medicine to heal himself.";
            }
            else
            {
                combatUI.combatText.text = skillUser.characterName + " used herbal medicine to heal " + target.characterName;
            }
        }

        else if (skill.skillName == "Corpse Paint")
        {
            target.GetComponent<CharacterFunctions>().GetInflicted("corpse paint", 999);
            target.damage += 5 + skillUser.level * 2; //When I'm adding the buff I'm looking at the level of skill user, when I'm removing the buff I'm looking at the level of the character with the buff. This is not a problem since everyone in the same party will have the same level BUT it will be a problem if slurps forgets to level up a character. (Oh also, corpse paint will never be removed anyway but I'm probably going to do this for some other skills too)
            target.speed += skillUser.level * 1;

            if (skillUser == target)
            {
                combatUI.combatText.text = skillUser.characterName + " applied corpse paint on himself. It looks fucking sick!";
            }
            else
            {
                combatUI.combatText.text = skillUser.characterName + " applied corpse paint on " + target.characterName + ". It looks fucking sick!";
            }
        }

        else if (skill.skillName == "One Peace")
        {
            skillUser.GetComponent<CharacterFunctions>().GetInflicted("one peace", 2);
            combatManager.peace = true;

            combatUI.combatText.text = skillUser.characterName + " used ONE PEACE! No one can attack or use any harmful abilities for 2 turns!";
        }

        else if (skill.skillName == "One Violence")
        {
            int roll = UnityEngine.Random.Range(0, 4);
            string damageText = 0.ToString();

            if (skillUser.characterName == "The Warlock")
            {
                roll = 3;
            }

            if (roll == 0)
            {
                target.GetComponent<CharacterFunctions>().TakeDamage(1, true);
                damageText = 1.ToString();
            }

            else if (roll == 1)
            {
                target.GetComponent<CharacterFunctions>().TakeDamage(11, true);
                damageText = 11.ToString();
            }

            else if (roll == 2)
            {
                target.GetComponent<CharacterFunctions>().TakeDamage(111, true);
                damageText = 111.ToString();
            }

            else if (roll == 3)
            {
                target.GetComponent<CharacterFunctions>().TakeDamage(1111, true);
                damageText = 1111.ToString();
            }

            combatUI.combatText.text = skillUser.characterName + " used ONE VIOLENCE to deal " + damageText + " damage!";
        }

        else if (skill.skillName == "Rapper")
        {
            foreach (CharacterData combatant in combatManager.combatants)
            {
                combatant.defence = 0;
            }

            combatUI.combatText.text = skillUser.characterName + " started rapping! It is so fucking bad that it reduced everyones defence to 0. Because they all want this shit to be over asap.";
        }

        else if (skill.skillName == "clownmaxxing")
        {
            foreach (CharacterData combatant in combatManager.combatants)
            {
                combatant.GetComponent<CharacterFunctions>().TakeDamage(30 + skillUser.level * 5, true);

                combatUI.combatText.text = skillUser.characterName + " is trying too hard to be funny. Everyone took damage because of the sheer amount of cringe they were exposed to.";
            }
        }

        else if (skill.skillName == "God Complex")
        {
            combatUI.combatText.text = skillUser.characterName + " started talking about how fucking great he is. This did absolutely nothing.";
        }

        else if (skill.skillName == "Extreme Laziness")
        {
            combatUI.combatText.text = skillUser.characterName + " was too lazy to implement a cool ultimate ability so he just normal attacked " + target.characterName + " instead.";
        }

        else if (skill.skillName == "Suck Life")
        {
            target.GetComponent<CharacterFunctions>().TakeDamage(skillUser.damage, true);
            skillUser.GetComponent<CharacterFunctions>().GetHealed(skillUser.damage);
            combatUI.combatText.text = "Shill sucked the life force of " + target.characterName;
        }

        else if (skill.skillName == "Summon Bot")
        {

        }

        combatManager.combatPauseCooldown = 2f;
    }

    public void UseItem(List<CharacterData> userTeam, CharacterData itemUser, List<CharacterData> enemyTeam, CharacterData target, string item)//Once again the if checks are absolutely disgusting
    {
        if (item == "catFood")
        {
            if(target.characterName == "Feral Cat")
            {
                target.characterName = "Honey";
                target.GetComponent<CharacterFunctions>().ChangeMaxHealth(100);

                combatManager.WinCombat();
            }
            
            else if (target.characterName == "Honey")
            {
                combatUI.combatText.text = "Honey loves it! She got healed by the cat food.";
                target.GetComponent<CharacterFunctions>().GetHealed(Convert.ToInt32(target.maxHealth / 2)); //cat food does the same thing as pizza despite being a much more specific item. Is this a problem? Maybe if this was a more serious game lmao. I guess I can make it cheaper or something.
            }

            else
            {
                combatUI.combatText.text = "the cat food had absolutely no effect";
            }

            playerStats.catFood -= 1;
        }

        else if (item == "pizza")
        {
            target.GetComponent<CharacterFunctions>().GetHealed(Convert.ToInt32(target.maxHealth/2));
            playerStats.pizza -= 1;
            combatUI.combatText.text = "Eating a delicious slice of 5/5 pizza healed " + target.characterName;
        }

        else if (item == "gamblingChip") //gambling chip was supposed to be a much more complicated (and hopefully fun) item but I really didn't want to complicate things further.
        {
            int roll = UnityEngine.Random.Range(0, 4);
            if (roll == 0)
            {
                itemUser.GetComponent<CharacterFunctions>().TakeDamage(itemUser.damage, false);
                combatUI.combatText.text = "Bad luck! You damaged yourself.";
            }
            else if (roll == 1)
            {
                target.GetComponent<CharacterFunctions>().TakeDamage(itemUser.damage, false);
                combatUI.combatText.text = "You deal normal damage. Not too good but it could have been worse.";
            }
            else if (roll == 2)
            {
                target.GetComponent<CharacterFunctions>().TakeDamage(itemUser.damage * 2, false);
                combatUI.combatText.text = "Nice! You did 2x damage.";
            }
            else if (roll == 3)
            {
                target.GetComponent<CharacterFunctions>().TakeDamage(itemUser.damage * 4, false);
                combatUI.combatText.text = "Jackpot! You did 4x damage.";
            }
            playerStats.gamblingChip -= 1;
        }

        combatManager.combatPauseCooldown = 1.8f;
    }

    public void EndTurn(CharacterData turnSpender) //truly amazing name
    {
        //what am I supposed to use this method for lmao
    }
}
