using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterUI : MonoBehaviour
{
    public TMP_Text healthBar;
    public TMP_Text cooldownBar;

    public void UpdateHealthBar(int newHealthValue) //this is quite flexible though there is almost no case where I would need to manually enter a valuer, I'll let this stay for now.
    {
        healthBar.text = newHealthValue.ToString();
    }

    public void UpdateCooldownBar(int newCooldownValue)
    {
        cooldownBar.text = newCooldownValue.ToString();    
    }
}
