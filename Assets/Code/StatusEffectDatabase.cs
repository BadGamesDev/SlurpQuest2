using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectDatabase : MonoBehaviour
{
    public static StatusEffect bleed = new () { statusName = "bleed", tickCount = 4, tickCooldown = 5000 };
    public static StatusEffect stun = new () { statusName = "stun" };
    public static StatusEffect engineStarted = new() { statusName = "engine started" };
    public static StatusEffect burnoutSmoke = new() { statusName = "burnout smoke", tickCount = 2, tickCooldown = 5000 };
}

