using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class CombatManager : MonoBehaviour
{
    public GameState gameState;
    public PlayerStats playerStats;
    public TilemapManager tilemapManager; //all this tilemap and entitytracker stuff really makes the spaghetti worse. The best solution is simply moving the respawn function somewhere else I guess.
    public EntityTracker entityTracker;
    public Camera mainCamera;
    public GameObject playerParty;
    public CombatUI combatUIScript;

    public bool peace;

    public AudioManager audioManager;
    public CinematicManager cinematicManager;
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

    public int slurpPassive; //I have completely given up, I don't want to write smart code I want to finish the god damn game.
    
    public float combatPauseCooldown;

    public void FixedUpdate()
    {
        if (gameState.inCombat == true && turnHaver == null && gameState.combatPaused == false)
        {
            if (combatPauseCooldown <= 0)
            {
                UpdateCombat(); //updating combat based on fixedupdate speed is kinda dumb, using deltatime or something might be better but this works for now
            }
            else
            {
                combatPauseCooldown -= Time.deltaTime;
            }
        }
    }

    public void IntroduceCombat(GameObject enemy)
    {
        PartyData enemyData = enemy.GetComponent<PartyData>();
        if (enemyData.pos1.name == "Husk")
        {
            overworldUI.AddMessage("Husk: Is... this... Justin... Tv...");
            if (gameState.metHusk == false)
            {
                overworldUI.AddMessage("This is a husk! An abandoned twitch account left to rot and decay, both alive and dead at the same time. Husks are mostly too weak and slow to become a problem but they might be dangerous in large groups");
                gameState.metHusk = true;
            }
        }

        else if (enemyData.pos1.name == "Shill")
        {
            overworldUI.AddMessage("Shill: Hey guys! Please follow and subscribe to my twitch channel, I have the best content!");
            if (gameState.metShill == false)
            {
                overworldUI.AddMessage("A Shameless shill, you must be very careful! They are known for sucking the life forces of other people.");
                gameState.metShill = true;
            }
        }

        else if (enemyData.pos1.name == "Troll")
        {
            overworldUI.AddMessage("Snow Troll: Trold ser dig! Trold smadrer dig! Trold dræber dig!");
            if (gameState.metTroll == false)
            {
                overworldUI.AddMessage("You might think this is the part where I gave up trying to find funny and thematic enemies and you would be right. But I guess it still kinda works as a reference to online trolls or something.");
                gameState.metTroll = true;
            }
        }

        else if (enemyData.pos1.name == "Bot")
        {
            overworldUI.AddMessage("Bot: Hello real person, I am a real person just like you. ");
            if (gameState.metBot == false)
            {
                overworldUI.AddMessage("A viewbot, created in the darkest pits of this corrupt realm to serve the dark lord. You are getting close!");
                gameState.metBot = true;
            }
        }

        else if (enemyData.pos1.name == "FeralCat")
        {
            if (gameState.metFeralCat == true)
            {
                overworldUI.AddMessage("Feral Cat: REEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE!!!!! (The cat is even louder this time).");
            }

            if (gameState.metFeralCat == false)
            {
                overworldUI.AddMessage("Feral Cat: REEEEEEEEEEEEEEEEEEEEE!!!!!");
                overworldUI.AddMessage("A feral cat, driven mad by the corrupting curse of the dark lord like so many other beings in this realm. For a moment you sense something familiar in it's eyes, but there is no time to think! Get ready for a fight!");
                gameState.metFeralCat = true;
            }
        }

        else if (enemyData.pos1.name == "CyborgHunter")
        {
            if (gameState.metCyborgHunter == true)
            {
                overworldUI.AddMessage("Cyborg Hunter: Subject came back with an alt account. IP ban required.");
            }

            if (gameState.metCyborgHunter == false)
            {
                overworldUI.AddMessage("Cyborg Hunter: Streamer detected. Termination protocol activated. Please do not resist. You shall be permanently banned soon."); // making this binary could be really fun
                overworldUI.AddMessage("A half man half machine monstrosity!");
                gameState.metCyborgHunter = true;
            }
        }

        else if (enemyData.pos1.name == "MadDizz")
        {
            if (gameState.metMaddizz == true)
            {
                overworldUI.AddMessage("MadDizz: WHO THE HELL ARE YOU? WHY DO YOU LOOK SO MUCH LIKE THE LAST GUY I BEAT? Doesn't matter... PREPARE TO GET DIZZED!");
            }

            if (gameState.metMaddizz == false)
            {
                overworldUI.AddMessage("MadDizz: There has been too much violence. Too much pain. None here are without sin. But I have an honorable compromise. Just walk away. " +
                       "Give me your nolifepoints, your companions, your hat, and the whole twitch channel, and I'll spare your life. " +
                       "Just walk away. I will give you safe passage in the wasteland. Just walk away and there will be an end to the horror." +
                       "(I can't believe you have been alive for almost 30 years and still haven't watched the road warrior... But it is my game so I'm putting this reference anyway. Fuck you!)");

                overworldUI.AddMessage("A streamer who has been driven mad by the curse. You should be careful, there is no telling what this guy could do!");
                gameState.metMaddizz = true;
            }
        }

        else if (enemyData.pos1.name == "TheWarlock")
        {
            if (gameState.metTheWarlock == true)
            {
                overworldUI.AddMessage("The Warlock: Kan du komme tilbage fra døden? Hvilken spektakulær besværgelse. Jeg skal lære det med det samme!");
            }

            if (gameState.metTheWarlock == false)
            {
                overworldUI.AddMessage("The Warlock: Tillykke! Du har fuldstændig spildt din tid ved at oversætte dette! Gå nu tilbage til spillet.");
                overworldUI.AddMessage("Yeah I don't have any idea what the fuck is going on this time...");
                gameState.metTheWarlock = true;
            }
        }

        else if (enemyData.pos1.name == "Asmongold")
        {
            if (gameState.metAsmongold == true)
            {
                overworldUI.AddMessage("So it is true after all... You have the ability to come back from death! Come then, let me kill you again and forever!");
            }

            if (gameState.metAsmongold == false)
            {
                overworldUI.AddMessage("The Lord of Decay: You have managed to make it all the way to me! But this is where your foolish journey ends. I'm a god. How can you kill a god? What a grand and intoxicating innocence. How could you be so naive? " +
                                   "Countless insignificant streamers like you tried standing against me and countless streamers like you got defeated, crushed and broken. I am your doom! I am the lord of decay, known by a thousand names in a thousand worlds. But you might call me...");
                overworldUI.AddMessage("                                                                 " +
                                       "ASMONGOLD!");
                gameState.metAsmongold = true;
            }
        }

        else if (enemyData.pos1.name == "Heado")
        {
            if (gameState.metHeado == true)
            {
                overworldUI.AddMessage("Wait what? I was supposed to lose that fight. What did you do?");
            }

            if (gameState.metHeado == false)
            {
                overworldUI.AddMessage("The Trickster: You fool! You have fallen into my trap, I am more powerful than ever in this realm, and you don't have any of your friends or mods to save you this time! It was I, Heado, all along!");
                overworldUI.AddMessage("Damn, looks like the trickster was just another alt of Heado...");
                gameState.metHeado = true;
            }
        }

        else if (enemyData.pos1.name == "TheAuditor")
        {
            if (gameState.metTheAuditor == true)
            {
                overworldUI.AddMessage("You have lost! You will lose again! Why do you keep trying? What is the point? Don't you see there is no hope!");
            }

            if (gameState.metTheAuditor == false)
            {
                overworldUI.AddMessage("The Auditor: So you have managed to kill the lord of decay? I must thank you for getting rid of that filth. Asmongold had his uses of course, but I no longer needed him... and now, it is just you and me. Time to end this.");
                overworldUI.AddMessage("Looks like this is it Slurp, this is the final fight. The fate of everyone rests on your shoulders now.");
                gameState.metTheAuditor = true;
            }
        }

        else
        {
            Debug.Log("who the fuck is this");
        }
    }

    public void StartCombat(GameObject sideOne, GameObject sideTwo) //this method can be simplified by directly taking in the data script instead of the GameObject
    {
        peace = false;
        xpReward = 0;
        gameState.overworldPaused = true;
        combatUI.SetActive(true);

        partyOne = sideOne;
        partyTwo = sideTwo;

        PartyData sideOneData = partyOne.GetComponent<PartyData>();
        PartyData sideTwoData = partyTwo.GetComponent<PartyData>();

        sideOneData.pos1.GetComponent<CharacterData>().skill4Cooldown = 7;

        if (sideTwoData.pos1.GetComponent<CharacterData>().characterName == "Cyborg Hunter")
        {
            audioManager.forestTheme.Stop();
            audioManager.cyborgTheme.Play();
        }

        if (sideTwoData.pos1.GetComponent<CharacterData>().characterName == "MadDizz")
        {
            audioManager.desertTheme.Stop();
            audioManager.maddizTheme.Play();
            audioManager.engineTheme.Play();
        }

        if (sideTwoData.pos1.GetComponent<CharacterData>().characterName == "The Warlock")
        {
            audioManager.snowTheme.Stop();
            audioManager.warlockTheme.Play();
        }

        if (sideTwoData.pos1.GetComponent<CharacterData>().characterName == "Asmongold")
        {
            audioManager.corruptionTheme.Stop();
            audioManager.asmonTheme.Play();
        }

        if (sideTwoData.pos1.GetComponent<CharacterData>().characterName == "TheAuditor")
        {
            audioManager.corruptionTheme.Stop();
            audioManager.auditorTheme.Play();
        }

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
            if (combatant.characterName == "Jaydizz" || combatant.characterName == "MadDizz")
            {
                combatant.GetComponent<CharacterFunctions>().ReduceTurnCooldown(2990);
            }

            else
            {
                int rollResult = UnityEngine.Random.Range(0, 1501);
                combatant.GetComponent<CharacterFunctions>().ReduceTurnCooldown(rollResult);
                Debug.Log(rollResult + combatant.characterName);
            }
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
                    bool simp = false;

                    if (combatant.skill1Cooldown > 0)
                    {
                        combatant.skill1Cooldown -= 1;
                    }
                    
                    if (combatant.skill2Cooldown > 0)
                    {
                        combatant.skill2Cooldown -= 1;
                    }
                    
                    if (combatant.skill3Cooldown > 0)
                    {
                        combatant.skill3Cooldown -= 1;
                    }
                    
                    if (combatant.skill4Cooldown > 0)
                    {
                        combatant.skill4Cooldown -= 1;
                    }

                    foreach (StatusEffect status in combatant.selfStatusEffects)
                    { 
                        if(status.statusName == "simp")
                        {
                            simp = true;
                        }
                    }

                    if (simp == false || combatant.isBoss)
                    {
                        turnHaver = combatant;
                    }

                    if (simp == true && !combatant.isBoss)
                    {
                        int tries = 0;
                        CharacterData target = null;

                        if (teamTwo.Count == 1)
                        {
                            FindObjectOfType<CombatManager>().combatPauseCooldown = 2.4f;
                            combatUIScript.combatText.text = combatant.characterName + " just gave up his turn because he thinks he has a chance with Slurp!";
                        }

                        else
                        {
                            while (target == null && tries < 999)
                            {
                                target = teamTwo[UnityEngine.Random.Range(0, teamTwo.Count)];

                                if (target == combatant)
                                {
                                    target = null;
                                }

                                else
                                {
                                    FindObjectOfType<CombatManager>().combatPauseCooldown = 2.4f;
                                    target.GetComponent<CharacterFunctions>().TakeDamage(combatant.damage, false);
                                    combatUIScript.combatText.text = combatant.characterName + " attacked " + target.characterName + " because he thinks he has a chance with Slurp!";
                                }
                                tries += 1;
                            }
                        }
                    }

                    combatant.GetComponent<CharacterFunctions>().ResetTurnCooldown();
                    
                    if(combatant.characterName == "Slurp") //taking all this space in the main update function because of a shitty passive ability...
                    { 
                        if (slurpPassive < 7)
                        {
                            combatant.defence += 1;
                            slurpPassive += 1;
                        }

                        foreach (CharacterData retard in combatants)
                        {
                            StatusEffect raid = new();
                            foreach (StatusEffect effekt in retard.globalStatusEffects)
                            {
                                if (effekt.statusName == "raid target")
                                {
                                    raid = effekt;
                                }
                            }
                            retard.globalStatusEffects.Remove(raid);
                            retard.GetComponent<CharacterUI>().UpdateStatusIcons();
                        }
                    }

                    List<StatusEffect> selfStatusToRemove = new();
                    foreach (StatusEffect status in combatant.selfStatusEffects)
                    {
                        if (status.statusName == "return to trad")
                        {
                            if(combatant.team == 0) //no one on the enemy team will evet use this skill so there is no need to check it like this but who knpws what the future brings
                            {
                                foreach(CharacterData member in teamOne)
                                {
                                    member.GetComponent<CharacterFunctions>().GetHealed(combatant.level * 5);
                                }
                            }
                            else if(combatant.team == 1)
                            {
                                foreach (CharacterData member in teamTwo)
                                {
                                    member.GetComponent<CharacterFunctions>().GetHealed(combatant.level * 5);
                                }
                            }
                        }

                        if (status.statusName == "thottery")
                        {
                            bool done = false;
                            int tries = 0;
                            while (!done && tries < 999)
                            {
                                if (combatant.team == 0) //no one on the enemy team will evet use this skill so there is no need to check it like this but who knpws what the future brings
                                {
                                    CharacterData target = teamTwo[UnityEngine.Random.Range(0, teamTwo.Count)];
                                    if (!target.isBoss)
                                    {
                                        target.GetComponent<CharacterFunctions>().GetInflicted("simp", 1);
                                        done = true;
                                    }

                                    tries += 1;
                                }
                                else if (combatant.team == 1)
                                {
                                    CharacterData target = teamOne[UnityEngine.Random.Range(0, teamOne.Count)];
                                    if (!target.isBoss)
                                    {
                                        target.GetComponent<CharacterFunctions>().GetInflicted("simp", 1);
                                        done = true;
                                    }

                                    tries += 1;
                                }
                            }
                        }

                        if (status.statusName == "clownmaxxing")
                        {
                            List<StatusEffect> allStatusEffects = new List<StatusEffect>
                            {
                                StatusEffectDatabase.simp,
                                StatusEffectDatabase.silence,
                                StatusEffectDatabase.bleed,
                                StatusEffectDatabase.burnoutSmoke,
                                StatusEffectDatabase.cute,
                                StatusEffectDatabase.engineStarted,
                                StatusEffectDatabase.stun,
                                StatusEffectDatabase.raidTarget,
                            };
                            
                            foreach (CharacterData member in combatants)
                            {
                                StatusEffect randeffect = allStatusEffects[UnityEngine.Random.Range(0, allStatusEffects.Count)];
                                member.GetComponent<CharacterFunctions>().GetInflicted(randeffect.statusName, 1);
                            }
                        }

                        if (status.statusName == "permacloud")
                        {
                            List<CharacterData> charactersToKill = new();

                            foreach (CharacterData character in teamOne)
                            {

                                if (character.health <= 50)
                                {
                                    charactersToKill.Add(character);
                                }
                                else
                                {
                                    character.GetComponent<CharacterFunctions>().TakeDamage(50, true);
                                }
                            }

                            foreach (CharacterData character in teamTwo)
                            {
                                character.GetComponent<CharacterFunctions>().GetHealed(50);
                            }

                            if (charactersToKill.Count > 0)
                            {
                                foreach (CharacterData yeetCandidate in charactersToKill)
                                {
                                    yeetCandidate.GetComponent<CharacterFunctions>().Die();
                                }
                            }
                        }

                        combatant.GetComponent<CharacterFunctions>().StatusTick(status.statusName);
                        status.tickCount -= 1;

                        if (status.tickCount <= 0)
                        {
                            selfStatusToRemove.Add(status); //I might actually delete the whole project if I mix global and self statuses again, am I retarded? Am I not supposed to do this kind of work?
                        }
                    }

                    foreach(StatusEffect statusToYeet in selfStatusToRemove)
                    {
                        if (statusToYeet.statusName == "one peace") //I HAVE LOST MY MIND TRYING TO FIX A BUG AND IT WAS ALL BECAUSE I HAVE WRITTEN ONE PIECE INSTEAD OF ONE PEACE HOLY SHIT I WANT TO KILL MYSELF
                        {
                            peace = false;
                            if(combatant.characterName == "The Warlock")
                            {
                                FindObjectOfType<AudioManager>().onePeaceTheme.Stop();
                                FindObjectOfType<AudioManager>().warlockTheme.Play();
                            }
                        }

                        //if (statusToYeet.statusName == "simp" && combatant.isBoss) FUCK THIS SKILL
                        //{
                        //    combatant.damage += 5 * playerStats.level;
                        //}

                        combatant.selfStatusEffects.Remove(statusToYeet);
                        combatant.GetComponent<CharacterUI>().UpdateStatusIcons();
                    }
                }
                
                else
                {
                    List<StatusEffect> globalStatusToRemove = new();
                    List<CharacterData> combatantsToKill = new();

                    foreach (StatusEffect status in combatant.globalStatusEffects)
                    {
                        status.tickCooldown -= 10;
                        if (status.tickCooldown <= 0)
                        {
                            combatant.GetComponent<CharacterFunctions>().StatusTick(status.statusName);
                            status.tickCount -= 1;
                            if (status.tickCount <= 0)
                            {
                                globalStatusToRemove.Add(status);
                                status.tickCooldown = 3000; //There is normally no need for this as the status is removed anyways, but if I don't do this the status will start with 0 cooldown to the next tick so uhh... I need this
                                
                                if(status.statusName == "engine started")
                                {
                                    combatant.speed /= 2;
                                }

                                if (status.statusName == "burnout smoke")
                                {
                                    combatant.dodge -= 60; //who ever you are, I am sorry that you have to read this shit...
                                }

                                if (status.statusName == "cute")
                                {
                                    combatant.defence -= 5 * (combatant.level + 1);
                                }

                                if (status.statusName == "ultra instinct")
                                {
                                    combatant.damage -= 15 + 8 * combatant.level;
                                    combatant.speed -= 5 + combatant.level * 2;
                                }

                                if (status.statusName == "corpse paint")
                                {
                                    combatant.damage += 5 + combatant.level * 2;
                                    combatant.speed += combatant.level * 1;
                                }

                                if (status.statusName == "bleed") //carried it over here to avoid an enumeration error, bleed damage was normally applied in character functions. The whole "statusTick" function is meaningless now (apart from stun)
                                {                                 // my fucking god I did all of this shit and yet the errors persist
                                    int bleedDamage = Convert.ToInt32(combatant.maxHealth / 20);

                                    if (bleedDamage < combatant.health)
                                    {
                                        combatant.GetComponent<CharacterFunctions>().TakeDamage(bleedDamage, true);
                                    }

                                    else
                                    {
                                        combatantsToKill.Add(combatant);
                                    }
                                }
                            }
                            else
                            {
                                status.tickCooldown = 3000;

                                if (status.statusName == "bleed")
                                {
                                    int bleedDamage = Convert.ToInt32(combatant.maxHealth / 20);

                                    if (bleedDamage < combatant.health)
                                    {
                                        combatant.GetComponent<CharacterFunctions>().TakeDamage(bleedDamage, true);
                                    }

                                    else
                                    { 
                                        combatantsToKill.Add(combatant); 
                                    }
                                }
                            }
                        }
                    }

                    if (combatantsToKill.Count > 0)
                    {
                        combatPauseCooldown += 0.5f; //this doesn't do shit, update still gives me enumeration error
                        foreach (CharacterData dudeToKill in combatantsToKill)
                        {
                            dudeToKill.GetComponent<CharacterFunctions>().Die();
                        }
                    }

                    if (globalStatusToRemove.Count > 0)
                    {
                        foreach (StatusEffect statusToYeet in globalStatusToRemove)
                        {
                            combatant.globalStatusEffects.Remove(statusToYeet);
                            combatant.GetComponent<CharacterUI>().UpdateStatusIcons();
                        }
                    }

                    combatant.GetComponent<CharacterFunctions>().ReduceTurnCooldown(combatant.speed);
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
                    //foreach (StatusEffect status in data.selfStatusEffects) NOT TICKING ANY STATUS EFFECTS WHILE THE CHARACTER IS BENCHED BECAUSE OF THE IMMENSE POTENTIAL FOR BUGS. FUCK YOUR REALISM!
                    //{
                    //    combatant.GetComponent<CharacterFunctions>().StatusTick(status.statusName);
                    //    status.tickCount -= 1;
                    //    if (status.tickCount <= 0)
                    //    {
                    //        data.selfStatusEffects.Remove(status);
                    //    }
                    //}

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

                        newBigfoot.GetComponent<CharacterFunctions>().ChangeMaxHealth(200 + 25 * playerStats.level);
                        bigFootData.damage += 5 * playerStats.level;

                        if (bench[0].GetComponent<CharacterData>().characterName == "Cyborg Hunter") 
                        {
                            bigFootData.maxHealth = 200;
                            bigFootData.health = 200;
                            bigFootData.damage = 25;
                        }

                        CombatUI combatUI = FindObjectOfType<CombatUI>();
                        combatPauseCooldown = 2;
                        combatUI.combatText.text = "HOLY SHIT IT IS BIGFOOT!";
                    }
                }

                else if (data.bigFootTurns > 0)
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
        playerStats.noLifePoints += xpReward; //you see the difference between the beautiful xp method and the disgusting nolifepoints line? This is my journey. I have started with noble intentions...
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

        if (bench.Count != 0)
        {
            foreach (GameObject combatant in bench)
            {
                Destroy(combatant);
            }
        }

        combatants.Clear();
        bench.Clear();
        combatPauseCooldown = 0;
        slurpPassive = 0;
        gameState.pizzaEaten = 0;
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

        if (winEvents.Contains("jaydizz win event"))
        {
            JaydizzWinEvent();
        }

        if (winEvents.Contains("oneViolence win event"))
        {
            OneViolenceWinEvent();
        }

        if (winEvents.Contains("asmongold win event"))
        {
            AsmongoldWinEvent();
        }

        if (winEvents.Contains("heado win event"))
        {
            HeadoWinEvent();
        }

        if (winEvents.Contains("auditor win event"))
        {
            AuditorWinEvent();
        }

        else if (winEvents.Count == 0)
        {
            FindAnyObjectByType<CombatUI>().combatFinishMessage = ("Congratulations! You have defeated the enemy and gained " + xpReward + " nolifepoints!"); 
        }

        overworldUI.LevelUpCheck();
        winEvents.Clear();
        loseEvents.Clear();
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

        if (loseEvents.Contains("jaydizz lose event"))
        {
            JaydizzLoseEvent();
        }

        if (loseEvents.Contains("oneViolence lose event"))
        {
            OneViolenceLoseEvent();
        }

        if (loseEvents.Contains("asmongold lose event"))
        {
            AsmongoldLoseEvent();
        }

        if (loseEvents.Contains("heado lose event"))
        {
            HeadoLoseEvent();
        }

        if (loseEvents.Contains("auditor lose event"))
        {
            AuditorLoseEvent();
        }

        else if (loseEvents.Count == 0)
        {
            FindAnyObjectByType<CombatUI>().combatFinishMessage = ("Congratulations! You have managed to lose the fight!");
        }

        winEvents.Clear();
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
            turnCoolDown = 3000,
        };
        FindAnyObjectByType<CombatUI>().combatFinishMessage = "The feral cat calms down after eating the cat food and you see a familiar face... IT IS HONEY OH MY FUCKING GOD IT IS HONEY! " +
                                                              "It is clear that honey doesn't want to fight you anymore. Congratulations you have won!";
        overworldUI.HoneyUnlockedMessage();
        playerStats.unlockedCompanions.Add(honey);
        playerStats.GainXP(500);
        playerStats.noLifePoints += 500; //gainxp should probably handle this on it's own tbh. There is no case where you only gain money.
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
            turnCoolDown = 3000,
        };
        FindAnyObjectByType<CombatUI>().combatFinishMessage = "The cyborg has been defeated! But it isn't long before he starts rising again, as you prepare yourself for another round you realise that " +
                                                              "he doesn't want to fight. The corruption is gone! Now that everyone has calmed down it is not hard for you to recognise who this is...";

        overworldUI.DigiUnlockedMessage();
        playerStats.unlockedCompanions.Add(digi63);
        digi63.skills.Add(SkillDatabase.boomer);
        digi63.skills.Add(SkillDatabase.empGrenade);

        audioManager.forestTheme.Play();
        audioManager.cyborgTheme.Stop();
    }

    public void DigiLoseEvent()
    {
        FindAnyObjectByType<CombatUI>().combatFinishMessage = "Well... it seems beating a terminator wasn't as easy as feeding a cat. Who would have guessed? But don't worry, you can just get a level or something and try again! " +
                                                              "Eventually you can get powerful enough to win no matter how bad you are at the game.";
        
        audioManager.forestTheme.Play();
        audioManager.cyborgTheme.Stop();
    }

    public void JaydizzWinEvent()
    {
        CompanionData jaydizz = new CompanionData
        {
            characterName = "Jaydizz",
            maxHealth = 70,
            health = 70,
            defence = 0,
            accuracy = 100,
            damage = 10,
            speed = 15,
            turnCoolDown = 3000,
        };
        FindAnyObjectByType<CombatUI>().combatFinishMessage = "Looks like he was not fast enough! Once again, you have beaten the corruption out of someone.";

        overworldUI.JaydizzUnlockedMessage();
        playerStats.unlockedCompanions.Add(jaydizz);
        jaydizz.skills.Add(SkillDatabase.polePosition);
        jaydizz.skills.Add(SkillDatabase.startYourEngines);

        audioManager.desertTheme.Play();
        audioManager.maddizTheme.Stop();
    }

    public void JaydizzLoseEvent()
    {
        FindAnyObjectByType<CombatUI>().combatFinishMessage = "You got dizzed by the dizz. Time to try again!";

        audioManager.desertTheme.Play();
        audioManager.maddizTheme.Stop();
    }

    public void OneViolenceWinEvent()
    {
        CompanionData oneViolence = new CompanionData
        {
            characterName = "OneViolence",
            maxHealth = 100,
            health = 100,
            defence = 5,
            accuracy = 90,
            damage = 10,
            speed = 10,
            turnCoolDown = 3000,
        };
        FindAnyObjectByType<CombatUI>().combatFinishMessage = "I guess they should start calling you the lockpicker. Because you know, he was a war-lock and then you beat him so it's like you picked the war-lock." +
                                                              "Get it? It is a pun and a really funny one. I'm so fucking good at this humor thing!";

        overworldUI.OneViolenceUnlockedMessage();
        playerStats.unlockedCompanions.Add(oneViolence);
        oneViolence.skills.Add(SkillDatabase.ghouldMaxxing);
        oneViolence.skills.Add(SkillDatabase.herbalMedicine);

        audioManager.snowTheme.Play();
        audioManager.warlockTheme.Stop();
    }

    public void OneViolenceLoseEvent()
    {
        FindAnyObjectByType<CombatUI>().combatFinishMessage = "The demonic powers of the warlock were simply too much for you to handle!";

        audioManager.snowTheme.Play();
        audioManager.warlockTheme.Stop();
    }

    public void AsmongoldWinEvent()
    {
        FindAnyObjectByType<CombatUI>().combatFinishMessage = "You did it. Asmongold is no more!";
        cinematicManager.asmonCutscene = true;

        audioManager.asmonDeathTheme.Play();
        audioManager.asmonTheme.Stop();
    }

    public void AsmongoldLoseEvent()
    {
        FindAnyObjectByType<CombatUI>().combatFinishMessage = "Damn... maybe you really are just wasting your life huh?";

        audioManager.corruptionTheme.Play();
        audioManager.asmonTheme.Stop();
    }

    public void HeadoWinEvent()
    {
        FindAnyObjectByType<CombatUI>().combatFinishMessage = "Looks like Tricky permanently banned Heado. He seems friendly for now but you should probably start running away before he changes his mind.";
    }

    public void HeadoLoseEvent()
    {
        FindAnyObjectByType<CombatUI>().combatFinishMessage = "Yeah I really don't know what the fuck happened, this fight was supposed to be a guaranteed win. Try again please and don't do whatever the fuck you just did.";
    }

    public void AuditorWinEvent()
    {
        FindAnyObjectByType<CombatUI>().combatFinishMessage = "YOU DID IT! YOU BEAT THE ACTUAL FINAL BOSS! IT IS DONE! YOU GOT HANK BACK! YOU DESTROYED THE CURSE AND SAVED EVERYONE! WE ARE SO FUCKING BACK!";

        audioManager.corruptionTheme.Play();
        audioManager.hankTheme.Stop();
    }

    public void AuditorLoseEvent()
    {
        FindAnyObjectByType<CombatUI>().combatFinishMessage = "Hey Slurp! I know the fight looks impossible but just focus on surviving until [REDACTED FOR SPOILERS].";

        audioManager.corruptionTheme.Play();
        audioManager.auditorTheme.Stop();
        audioManager.hankTheme.Stop(); //shouldn't be possible to lose with hank but just in case.
    }

    public void RespawnPlayer()
    {
        playerParty.transform.position = gameState.checkpoint;
        Vector3Int playerGridPos = tilemapManager.tilemap.WorldToCell(gameState.checkpoint);
        entityTracker.UpdateEntityPosition(playerParty, playerGridPos);
        playerParty.GetComponent<PartyFunctions>().currentGridPosition = playerGridPos;
    }
}
