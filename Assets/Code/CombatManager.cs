using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatManager : MonoBehaviour
{
    public GameState gameState;
    public PlayerStats playerStats;
    public TilemapManager tilemapManager; //all this tilemap and entitytracker stuff really makes the spaghetti worse. The best solution is simply moving the respawn function somewhere else I guess.
    public EntityTracker entityTracker;
    public Camera mainCamera;
    public GameObject playerParty;

    public OverworldUI overworldUI; //I hope referencing this shit here doesn't cause the world to end
    public GameObject combatUI;
    public GameObject bigfoot; //disgusting reference
    public Transform[] spawnSlots;
    public GameObject partyOne;
    public GameObject partyTwo;
    public List<CharacterData> combatants;
    public List<CharacterData> teamOne;
    public List<CharacterData> teamTwo;
    public List<GameObject> bench;
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

    public void IntroduceCombat(GameObject enemy)
    {
        PartyData enemyData = enemy.GetComponent<PartyData>();
        if (enemyData.pos1.name == "Husk")
        {
            overworldUI.AddMessage("Husk: Pew... die...pie... Boxx... yyyy... Nn... ew... grouuuun... dddsss...");
            if (gameState.metHusk == false)
            {
                overworldUI.AddMessage("This is a husk! An abandoned account left to rot and decay, both alive and dead at the same time. Husks are mostly too weak and slow to become a problem but they might be dangerous in large groups");
                gameState.metHusk = true;
            }
        }

        else if (enemyData.pos1.name == "FeralCat")
        {
            overworldUI.AddMessage("FeralCat: MEEEOOWWWWWWW RAAAEERGGGGGGG REEEEEEEEEEEEEEEEEEEE!!!!!");
            if (gameState.metFeralCat == false)
            {
                overworldUI.AddMessage("A feral cat, driven mad by the corrupting curse of the dark lord like so many other beings in this realm. For a moment you sense something familiar in it's eyes, but there is no time to think! Get ready for a fight!");
                gameState.metFeralCat = true;
            }
        }

        else if (enemyData.pos1.name == "CyborgHunter")
        {
            overworldUI.AddMessage("CyborgHunter: Seek and destroy!"); // making this binary could be really fun
            if (gameState.metCyborgHunter == false)
            {
                overworldUI.AddMessage("A half man half machine monstrosity!");
                gameState.metCyborgHunter = true;
            }
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

        if (sideTwoData.pos3 != null)
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

    public void AddToBench(GameObject combatant)
    {
        CharacterData combatantData = combatant.GetComponent<CharacterData>();
        combatantData.bigFootSlot = combatant.transform.parent;
        combatants.Remove(combatantData);

        if (combatantData.team == 0)
        {
            teamOne.Remove(combatantData);
        }
        else if (combatantData.team == 1)
        {
            teamTwo.Remove(combatantData);
        }

        if (teamOne.Count == 0)
        {
            LoseCombat();
        }

        if (teamTwo.Count == 0)
        {
            WinCombat();
        }

        bench.Add(combatant);

        Vector2 currentPos = combatant.transform.position;
        currentPos.y += 10000; //LMAO FUCK YOU! I DON'T NEED A SOPHISTICATED SOLUTION, JUST YEET THE FUCKER AWAY!
        combatant.transform.position = currentPos;
    }

    public void PullFromBench(GameObject combatant)
    {
        CharacterData combatantData = combatant.GetComponent<CharacterData>();
        combatants.Add(combatantData);

        if (combatantData.team == 0)
        {
            teamOne.Add(combatantData);
        }
        else if (combatantData.team == 1)
        {
            teamTwo.Add(combatantData);
        }

        Vector2 currentPos = combatant.transform.position;
        currentPos.y -= 10000;
        combatant.transform.position = currentPos;
        bench.Remove(combatant); //This is the clear way to write this. Visual studio can suck my dick.
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
                    foreach (StatusEffect status in combatant.selfStatusEffects)
                    {
                        combatant.GetComponent<CharacterFunctions>().StatusTick(status.statusName);
                        status.tickCount -= 1;
                        if (status.tickCount <= 0)
                        {
                            combatant.selfStatusEffects.Remove(status); //I might actually kill myself if I mix up global and self statuses one more time am I retarded? Am I not supposed to do this kind of work?
                        }
                    }
                }
                else
                {
                    combatant.GetComponent<CharacterFunctions>().ReduceTurnCooldown(combatant.speed);
                    foreach (StatusEffect status in combatant.globalStatusEffects)
                    {
                        status.tickCooldown -= 10;
                        if (status.tickCooldown <= 0)
                        {
                            combatant.GetComponent<CharacterFunctions>().StatusTick(status.statusName);
                            status.tickCount -= 1;
                            if (status.tickCount <= 0)
                            {
                                status.tickCooldown = 5000; //There is normally no need for this as the status is removed anyways, but if I don't do this the status will start with 0 cooldown to the next tick so uhh... I need this
                                combatant.globalStatusEffects.Remove(status);
                            }
                            else
                            {
                                status.tickCooldown = 5000;
                            }
                        }
                    }
                }
            }
        }

        foreach (GameObject combatant in bench)
        {
            CharacterData data = combatant.GetComponent<CharacterData>();

            if (turnHaver == null)
            {
                if (data.turnCoolDown <= 0)
                {
                    data.bigFootTurns -= 1;
                    combatant.GetComponent<CharacterFunctions>().ResetTurnCooldown();
                    foreach (StatusEffect status in data.selfStatusEffects)
                    {
                        combatant.GetComponent<CharacterFunctions>().StatusTick(status.statusName);
                        status.tickCount -= 1;
                        if (status.tickCount <= 0)
                        {
                            data.selfStatusEffects.Remove(status); //I might actually kill myself if I mix up global and self statuses one more time am I retarded? Am I not supposed to do this kind of work?
                        }
                    }

                    if (data.bigFootTurns <= 0)
                    {
                        GameObject newBigfoot = Instantiate(bigfoot, data.bigFootSlot.position, Quaternion.identity);
                        newBigfoot.transform.SetParent(data.bigFootSlot);
                        CharacterData bigFootData = newBigfoot.GetComponent<CharacterData>();
                        combatants.Add(bigFootData);
                        if (data.team == 0)
                        {
                            teamOne.Add(bigFootData);
                        }
                        if (data.team == 1)
                        {
                            teamTwo.Add(bigFootData);
                        }
                        bigFootData.team = data.team;
                    }
                }

                else
                {
                    combatant.GetComponent<CharacterFunctions>().ReduceTurnCooldown(data.speed);
                }
            }
        }
    }

    public void WinCombat()
    {
        partyTwo.GetComponent<PartyFunctions>().Die();
        playerStats.GainXP(xpReward);
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
        RespawnPlayer();
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

        if (winEvents.Contains("digi win event"))
        {
            DigiWinEvent();
        }

        winEvents.Clear();
    }

    public void LoseEventCheck()
    {
        if (loseEvents.Contains("honey lose event"))
        {
            HoneyLoseEvent();
        }

        if (loseEvents.Contains("digi lose event"))
        {
            DigiLoseEvent();
        }

        loseEvents.Clear();
    }

    public void HoneyWinEvent()
    {
        CompanionData honey = new CompanionData
        {
            characterName = "Honey",
            maxHealth = 80,
            health = 80,
            defence = 0,
            accuracy = 100,
            damage = 15,
            speed = 12,
            turnCoolDown = 5000,
        };
        FindAnyObjectByType<CombatUI>().combatFinishMessage = "The feral cat calms down after eating the cat food and you see a familiar face... IT IS HONEY OH MY FUCKING GOD IT IS HONEY! " +
                                                              "It is clear that honey doesn't want to fight you anymore. Congratulations you have won!";
        overworldUI.HoneyUnlockedMessage();
        playerStats.unlockedCompanions.Add(honey);
        honey.skills.Add(SkillDatabase.longClaws);
        honey.skills.Add(SkillDatabase.swipe);

        gameState.progress += 1;
        gameState.checkpoint = gameState.checkpointList[gameState.progress];
    }

    public void HoneyLoseEvent()
    {
        FindAnyObjectByType<CombatUI>().combatFinishMessage = "You just got one shot lmao! Maybe you could look around to see if there is anything that can help you?"; // I am referencing the combatUI like this because I am fucking afraid of circular references.
    }                                                                                                                                                                   // Of course referencing it like this doesn't change much but it makes me feel safe

    public void DigiWinEvent()
    {
        CompanionData digi63 = new CompanionData
        {
            characterName = "Digi63",
            maxHealth = 110,
            health = 110,
            defence = 5,
            accuracy = 100,
            damage = 10,
            speed = 8,
            turnCoolDown = 5000,
        };
        FindAnyObjectByType<CombatUI>().combatFinishMessage = "The cyborg has been defeated! But it isn't long before he starts rising again, as you prepare yourself for another round you realise that " +
                                                              "he doesn't want to fight. The corruption is gone! Now that everyone has calmed down it is not hard for you to recognise who this is...";

        overworldUI.DigiUnlockedMessage();
        playerStats.unlockedCompanions.Add(digi63);
        digi63.skills.Add(SkillDatabase.boomer);
        digi63.skills.Add(SkillDatabase.empGrenade);
    }

    public void DigiLoseEvent()
    {
        FindAnyObjectByType<CombatUI>().combatFinishMessage = "Well... it seems beating a terminator wasn't as easy as feeding a cat. Who would have guessed? But don't worry, you can just get a level or something and try again! " +
                                                              "Eventually you can get powerful enough to win no matter how bad you are at the game.";
    }

    public void RespawnPlayer()
    {
        playerParty.transform.position = gameState.checkpoint;
        Vector3Int playerGridPos = tilemapManager.tilemap.WorldToCell(gameState.checkpoint);
        entityTracker.UpdateEntityPosition(playerParty, playerGridPos);
        playerParty.GetComponent<PartyFunctions>().currentGridPosition = playerGridPos;
    }
}
