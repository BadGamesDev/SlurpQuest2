using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFunctions : MonoBehaviour
{
    public CharacterData ownData;
    public CharacterUI ownUI;
    public CombatManager combatManager;

    private void Start()
    {
        ownUI.healthBar.text = ownData.health.ToString();
        ownUI.cooldownBar.text = ownData.turnCoolDown.ToString();
        combatManager = FindObjectOfType<CombatManager>();
    }

    public void TakeDamage(int amount)
    {
        ownData.health -= amount;
        ownUI.UpdateHealthBar(ownData.health);
        if (ownData.health <= 0)
        {
            Die();
        }
    }

    public void reduceTurnCooldown(int amount)
    {
        ownData.turnCoolDown -= amount;
        ownUI.UpdateCooldownBar(ownData.turnCoolDown);
    }

    public void Die()
    {
        combatManager.combatants.Remove(ownData);

        if (ownData.team == 0)
        {
            combatManager.teamOne.Remove(ownData);
        }
        else if(ownData.team == 1)
        {
            combatManager.teamTwo.Remove(ownData);
        }

        if(combatManager.teamOne.Count == 0)
        {
            combatManager.LoseCombat();
        }

        if(combatManager.teamTwo.Count == 1)
        {
            combatManager.WinCombat();
        }

        Destroy(gameObject);
    }
}
