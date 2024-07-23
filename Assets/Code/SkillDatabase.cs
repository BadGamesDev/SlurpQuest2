using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDatabase : MonoBehaviour
{
    public static Skill swipe = new Skill { skillName = "Swipe", hostile = true };
    public static Skill empGrenade = new Skill { skillName = "EMP Grenade", hostile = true };
    public static Skill findBigfoot = new Skill { skillName = "Find Bigfoot", hostile = false };

}
