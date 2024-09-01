using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheAuditorAI : MonoBehaviour
{
    public GameObject hank;
    public GameObject postalGuy;

    public AudioManager audioManager;
    public ImageLoader imageLoader;
    public CinematicManager cinematicManager;
    public PrefabLoader prefabLoader;
    public CombatManager combatManager;
    public CombatFunctions combatFunctions;
    public CombatUI combatUI;
    public CharacterData ownData;
    public List<string> moves;
    public int turnNumber;

    public float dialogueCooldown;
    public int dialoguStage;

    void Start()
    {
        combatManager = FindObjectOfType<CombatManager>();
        combatFunctions = FindObjectOfType<CombatFunctions>();
        combatUI = FindObjectOfType<CombatUI>();
        prefabLoader = FindObjectOfType<PrefabLoader>();
        cinematicManager = FindObjectOfType<CinematicManager>();
        imageLoader = FindObjectOfType<ImageLoader>();
        audioManager = FindObjectOfType<AudioManager>();

        moves.Add("attack");
        moves.Add("hank comes to help");
        moves.Add("attack");
        moves.Add("lose halo");
        moves.Add("attack");
        moves.Add("attack");
        moves.Add("attack");
        moves.Add("attack");
        moves.Add("attack");
        moves.Add("attack");

        hank = prefabLoader.HankPrefab;
        postalGuy = prefabLoader.PostalGuyPrefab;
    }

    public void FixedUpdate()//horrible for performance
    {
        if (combatManager.turnHaver == ownData && !ownData.selfStatusEffects.Contains(StatusEffectDatabase.stun) && combatManager.teamOne.Count != 0)
        {
            dialogueCooldown -= Time.deltaTime;

            if (turnNumber < moves.Count)
            {
                if (moves[turnNumber] == "attack")
                {
                    CharacterData target = combatManager.teamOne[Random.Range(0, combatManager.teamOne.Count)];
                    combatFunctions.Attack(ownData, target);
                }

                else if (moves[turnNumber] == "hank comes to help")
                {
                    if (dialogueCooldown <= 0 && dialoguStage == 0)
                    {
                        audioManager.auditorTheme.Stop();
                        audioManager.hankTheme.Play();
                        combatManager.combatPauseCooldown = 5;
                        dialogueCooldown = 5;
                        combatUI.combatText.text = "The Auditor: Wait... Who the fuck changed my boss music?";
                        dialoguStage += 1;
                    }

                    if (dialogueCooldown <= 0 && dialoguStage == 1)
                    {
                        combatManager.combatPauseCooldown = 5;
                        dialogueCooldown = 5;
                        combatUI.combatText.text = "The Auditor: NO! It can not be! I have already killed you!";

                        GameObject combatant5 = Instantiate(hank, combatManager.spawnSlots[1].position, Quaternion.identity);
                        combatant5.transform.SetParent(combatManager.spawnSlots[1]);
                        CharacterData combatant5Data = combatant5.GetComponent<CharacterData>();
                        combatManager.combatants.Add(combatant5Data);
                        combatant5Data.team = 0;
                        combatManager.teamOne.Add(combatant5Data);
                        combatant5Data.turnCoolDown += Random.Range(100, 1001);
                        dialoguStage += 1;
                    }

                    if (dialogueCooldown <= 0 && dialoguStage == 2)
                    {
                        combatManager.combatPauseCooldown = 8;
                        dialogueCooldown = 8;
                        combatUI.combatText.text = "OH MY FUCKING GOD! IT IS HANK J. WIMBLETON FROM THE HIT SERIES MADNESS COMBAT CREATED BY KRINKELS AND UPLOADED TO NEWGROUNDS IN 2002! HOLY FUCKING SHIT IT IS HAPPENING!";
                        dialoguStage += 1;
                    }

                    if (dialogueCooldown <= 0 && dialoguStage == 3)
                    {
                        combatManager.combatPauseCooldown = 5;
                        dialogueCooldown = 5;
                        combatUI.combatText.text = "Hank: I have found a... friend on my way. Hope you don't mind him.";
                        dialoguStage += 1;
                    }

                    if (dialogueCooldown <= 0 && dialoguStage == 4)
                    {
                        combatManager.combatPauseCooldown = 5;
                        dialogueCooldown = 5;
                        combatUI.combatText.text = "HOLY SHIT IT IS THE POSTAL GUY! WHAT A FUCKING TEAM!!!!!";
                        GameObject combatant6 = Instantiate(postalGuy, combatManager.spawnSlots[2].position, Quaternion.identity);
                        combatant6.transform.SetParent(combatManager.spawnSlots[2]);
                        CharacterData combatant6Data = combatant6.GetComponent<CharacterData>();
                        combatManager.combatants.Add(combatant6Data);
                        combatant6Data.team = 0;
                        combatManager.teamOne.Add(combatant6Data);
                        combatant6Data.turnCoolDown += Random.Range(100, 1001);

                        dialoguStage += 1;
                    }

                    if (dialogueCooldown <= 0 && dialoguStage == 5)
                    {
                        combatManager.combatPauseCooldown = 5;
                        dialogueCooldown = 5;
                        combatUI.combatText.text = "TheAuditor: As if this means anything! Now I can kill all of you at once!";
                        dialoguStage += 1;
                    }

                    if (dialogueCooldown <= 0 && dialoguStage == 6)
                    {
                        CharacterData target = combatManager.teamOne[Random.Range(0, combatManager.teamOne.Count)];
                        combatFunctions.Attack(ownData, target);
                        dialoguStage = 0;
                    }
                }

                else if (moves[turnNumber] == "lose halo")
                {
                    if (dialogueCooldown <= 0 && dialoguStage == 0)
                    {
                        combatManager.combatPauseCooldown = 5;
                        dialogueCooldown = 5;
                        combatUI.combatText.text = "The Auditor: You can not harm me! Give up already!";
                        Debug.Log("stage2");
                        dialoguStage += 1;
                    }

                    if (dialogueCooldown <= 0 && dialoguStage == 1)
                    {
                        cinematicManager.auditorCutscene = true;
                        combatManager.combatPauseCooldown = 10;
                        dialogueCooldown = 10;
                        transform.GetComponent<Image>().sprite = imageLoader.AuditorNoHalo;
                        ownData.invulnerable = false;
                        combatUI.combatText.text = "The Auditor lost his halo! Now you can murder the fuck out of him!";
                        dialoguStage += 1;
                    }

                    if (dialogueCooldown <= 0 && dialoguStage == 2)
                    {
                        dialoguStage = 0;
                    }
                }
            }

            else
            {
                CharacterData target = combatManager.teamOne[Random.Range(0, combatManager.teamOne.Count)];
                combatFunctions.Attack(ownData, target);
                Debug.Log("No moves left");
            }

            if (dialogueCooldown <= 0)
            {
                combatManager.turnHaver = null;
                turnNumber += 1;
            }
        }
    }
}