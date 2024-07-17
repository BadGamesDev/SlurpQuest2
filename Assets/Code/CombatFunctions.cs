using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatFunctions : MonoBehaviour
{
    public void Attack(CharacterData attacker, CharacterData target) //might be a good idea to directly get functions instead of data but I might also need the data so this is fine for now
    {
        target.GetComponent<CharacterFunctions>().TakeDamage(attacker.damage);
    }

    public void UseSkill(List<CharacterData> userTeam, CharacterData skillUser, List<CharacterData> enemyTeam, CharacterData target, Skill skill) //I will use if checks for each skill, this is quite bad and it also requires me to write hundreds of ugly lines but it is also quite simple. I want to make a finished game not a pretty codebase.
    {
        if (skill.skillName == "swipe")
        {
            foreach(CharacterData character in enemyTeam)
            {
                character.GetComponent<CharacterFunctions>().TakeDamage(skillUser.damage);
            }
        }
        //else if (skill == "EMPBlast")
        //{

        //}
        //else if (skill == "oneViolence" )
        //{

        //}
    }

    public void EndTurn(CharacterData turnSpender) //truly amazing name
    {

    }
}
