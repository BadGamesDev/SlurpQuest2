using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectDatabase : MonoBehaviour
{
    public static StatusEffect bleed = new StatusEffect { statusName = "bleed", tickCount = 4, tickCooldown = 5000 };
    public static StatusEffect stun = new StatusEffect { statusName = "stun", tickCount = 1, tickCooldown = 5000 }; //stun can't work like this, I'll think of something.
}

