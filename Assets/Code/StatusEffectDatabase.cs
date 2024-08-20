using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectDatabase : MonoBehaviour
{
    public static StatusEffect bleed = new () { statusName = "bleed", tickCount = 4, tickCooldown = 3000 };
    public static StatusEffect stun = new () { statusName = "stun" };
    public static StatusEffect tedAudience = new() { statusName = "ted audience", tickCooldown = 3000 };
    public static StatusEffect raidTarget = new() { statusName = "raid target", tickCooldown = 3000 };
    public static StatusEffect engineStarted = new() { statusName = "engine started" };
    public static StatusEffect burnoutSmoke = new() { statusName = "burnout smoke", tickCount = 2, tickCooldown = 3000 };
    public static StatusEffect thottery = new() { statusName = "thottery" };
    public static StatusEffect clownmaxxing = new() { statusName = "clownmaxxing" };
    public static StatusEffect tradwis = new() { statusName = "return to trad" };
}

