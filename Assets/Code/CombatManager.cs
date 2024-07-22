using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatManager : MonoBehaviour
{ 
    public GameState gameState;
    public PlayerStats playerStats;
    public Camera mainCamera;

    public OverworldUI overworldUI; //I hope referencing this shit here doesn't cause the world to end
    public GameObject combatUI;
    public Transform[] spawnSlots;
    public GameObject partyOne;
    public GameObject partyTwo;
    public List<CharacterData> combatants;
    public List<CharacterData> teamOne;
    public List<CharacterData> teamTwo;
    public CharacterData turnHaver;
    public Skill selectedSkill; //I guess this is the right place for this variable
    public string selectedItem;
    public int xpReward;//whole xp system can be MUCH simpler, but I guess this is fine, it gives me more control at least.
    public List<string> winEvents;
    public List<string> loseEvents; //dumb names but I want consistency with the win and lose methods

    public void FixedUpdate()
    {
        if (gameState.inCombat == true && turnHaver == null)
        {
            UpdateCombat(); //updating combat based on fixedupdate speed is kinda dumb, using deltatime or something might be better but this works for now
        }
    }

    public void StartCombat(GameObject sideOne, GameObject sideTwo) //this method can be simplified by directly taking in the data script instead of the GameObject
    {
        xpReward = 0;
        gameState.overworldPaused = true;
        combatUI.SetActive(true);

        partyOne = sideOne;
        partyTwo = sideTwo;

        PartyData sideOneData = partyOne.GetComponent<PartyData>();
        PartyData sideTwoData = partyTwo.GetComponent<PartyData>();

        GameObject combatant1 = Instantiate(sideOneData.pos1, spawnSlots[0].position, Quaternion.identity);
        combatant1.transform.SetParent(spawnSlots[0]);
        CharacterData combatant1Data = combatant1.GetComponent<CharacterData>();
        combatants.Add(combatant1Data);
        combatant1Data.team = 0;
        teamOne.Add(combatant1Data);
        
        if (sideOneData.pos2 != null) //I'm sure checking if a party position exists can be automated but there is no need to complicate things for now
        {
            GameObject combatant2 = Instantiate(sideOneData.pos2, spawnSlots[1].position, Quaternion.identity);
            combatant2.transform.SetParent(spawnSlots[1]);
            CharacterData combatant2Data = combatant2.GetComponent<CharacterData>();
            combatants.Add(combatant2Data);
            combatant2Data.team = 0;
            teamOne.Add(combatant2Data);
        }
        
        if (sideOneData.pos3 != null)
        {
            GameObject combatant3 = Instantiate(sideOneData.pos3, spawnSlots[2].position, Quaternion.identity);
            combatant3.transform.SetParent(spawnSlots[2]);
            CharacterData combatant3Data = combatant3.GetComponent<CharacterData>();
            combatants.Add(combatant3Data);
            combatant3Data.team = 0;
            teamOne.Add(combatant3Data);
        }

        GameObject combatant4 = Instantiate(sideTwoData.pos1, spawnSlots[3].position, Quaternion.identity);
        combatant4.transform.SetParent(spawnSlots[3]);
        CharacterData combatant4Data = combatant4.GetComponent<CharacterData>();
        combatants.Add(combatant4Data);
        combatant4Data.team = 1;
        teamTwo.Add(combatant4Data);

        if (sideTwoData.pos2 != null)
        {
            GameObject combatant5 = Instantiate(sideTwoData.pos2, spawnSlots[4].position, Quaternion.identity);
            combatant5.transform.SetParent(spawnSlots[4]);
            CharacterData combatant5Data = combatant5.GetComponent<CharacterData>();
            combatants.Add(combatant5Data);
            combatant5Data.team = 1;
            teamTwo.Add(combatant5Data);
        }
        
        if(sideTwoData.pos3 != null)
        {
            GameObject combatant6 = Instantiate(sideTwoData.pos3, spawnSlots[5].position, Quaternion.identity);
            combatant6.transform.SetParent(spawnSlots[5]);
            CharacterData combatant6Data = combatant6.GetComponent<CharacterData>();
            combatants.Add(combatant6Data);
            combatant6Data.team = 1;
            teamTwo.Add(combatant6Data);
        }

        gameState.inCombat = true;
        CollectEvents();
        RollInitiative();
    }

    public void CollectEvents()
    {
        foreach (CharacterData combatant in combatants)
        {
            if (!string.IsNullOrEmpty(combatant.winEvent))
            {
                winEvents.Add(combatant.winEvent);
            }
            if (!string.IsNullOrEmpty(combatant.loseEvent))
            {
                loseEvents.Add(combatant.loseEvent);
            }
        }
    }
    public void RollInitiative() // I will probably add an initiative stat if I have the time
    {
        foreach (CharacterData combatant in combatants)
        {
            int rollResult = Random.Range(0, 2501);
            combatant.GetComponent<CharacterFunctions>().ReduceTurnCooldown(rollResult);
        }
    }

    public void UpdateCombat()
    {
        foreach (CharacterData combatant in combatants)
        {
            if (turnHaver == null) //band-aid to prevent a bug that skips turns if two characters have really close cooldowns, it seems checking before the function isn't enough
            {
                if (combatant.turnCoolDown <= 0)
                {
                    turnHaver = combatant;
                    combatant.GetComponent<CharacterFunctions>().ResetTurnCooldown();
                }
                else
                {
                    combatant.GetComponent<CharacterFunctions>().ReduceTurnCooldown(combatant.speed);
                }
            }
        }
    }

    public void WinCombat() 
    {
        partyTwo.GetComponent<PartyFunctions>().Die();
        playerStats.xp += xpReward;
        gameState.combatFinished = true;
        gameState.inCombat = false;
        WinEventCheck();
    }

    public void LoseCombat()
    {  
        gameState.combatFinished = true;
        gameState.inCombat = false;
        LoseEventCheck();
        gameState.deathCount += 1;
        if (gameState.deathCount == 1)
        {
            overworldUI.FirstDeathMessage(); //God forgive me for checking each death to see if it is the first death please. I'll never do something like this again I swear.
        }
    }

    public void ClearCombat()
    {
        teamOne.Clear();
        teamTwo.Clear();
        foreach (CharacterData combatant in combatants)
        {
            Destroy(combatant.gameObject);
        }
        combatants.Clear();
    }
    
    public void WinEventCheck()
    {
        if (winEvents.Contains("honey win event"))
        { 
            HoneyWinEvent(); 
        }
    }

    public void LoseEventCheck()
    {
        if (loseEvents.Contains("honey lose event"))
        {
            HoneyLoseEvent();
        }
    }

    public void HoneyWinEvent()
    {
        CompanionData honey = new CompanionData
        {
            characterName = "Honey",
            maxHealth = 100,
            health = 100,
            defence = 0,
            accuracy = 100,
            damage = 10,
            speed = 10,
            turnCoolDown = 5000,
        };

        FindAnyObjectByType<CombatUI>().combatFinishMessage = "The feral cat calms down after eating the cat food and you see a familiar face... IT IS HONEY OH MY FUCKING GOD IT IS HONEY! " +
                                                              "It is clear that honey doesn't want to fight you anymore. Congratulations you have won!";
        overworldUI.HoneyUnlockedMessage();
        playerStats.unlockedCompanions.Add(honey);
    }

    public void HoneyLoseEvent()
    {
        Debug.Log("IT NEEDS TO WORK WHAT THE FUCK");
        FindAnyObjectByType<CombatUI>().combatFinishMessage = "You just got one shot lmao! Maybe you could look around to see if there is anything that can help you?"; // I am referencing the combatUI like this because I am fucking afraid of circular references.
    }                                                                                                                                                                   // Of course referencing it like this doesn't change much but it makes me feel safe
}
