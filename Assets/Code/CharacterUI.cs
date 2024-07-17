using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterUI : MonoBehaviour
{
    public TMP_Text healthBar;
    public TMP_Text cooldownBar;

    public void UpdateHealthBar(int newHealthValue)
    {
        healthBar.text = newHealthValue.ToString();
    }

    public void UpdateCooldownBar(int newCooldownValue)
    {
        cooldownBar.text = newCooldownValue.ToString();    
    }
}
