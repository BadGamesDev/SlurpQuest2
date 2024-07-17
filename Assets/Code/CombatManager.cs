using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{ 
    public GameState gameState;
    public Camera mainCamera;

    public GameObject combatUI;
    public Transform[] spawnSlots;
    public List<CharacterData> combatants;
    public List<CharacterData> teamOne;
    public List<CharacterData> teamTwo;
    public CharacterData turnHaver;
    public Skill selectedSkill; //I guess this is the right place for this variable

    public void FixedUpdate()
    {
        if (turnHaver == null)
        {
            UpdateCombat(); //updating combat based on fixedupdate speed is kinda dumb, using time might be better but this works for now
        }
    }

    public void StartCombat(GameObject sideOne, GameObject sideTwo) //this method can be simplified by directly taking in the data script instead of the GameObject
    {
        gameState.overworldPaused = true;
        combatUI.SetActive(true);

        PartyData sideOneData = sideOne.GetComponent<PartyData>();
        PartyData sideTwoData = sideTwo.GetComponent<PartyData>();

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
        RollInitiative();
        UpdateCombat();
    }

    public void RollInitiative() // I will probably add an initiative stat if I have the time
    {
        foreach (CharacterData combatant in combatants)
        {
            int rollResult = Random.Range(0, 2501);
            combatant.turnCoolDown -= rollResult;
        }
    }

    public void UpdateCombat()
    {
        foreach (CharacterData combatant in combatants)
        {
            combatant.turnCoolDown -= combatant.speed;
            if (combatant.turnCoolDown <= 0)
            {
                turnHaver = combatant;
                combatant.turnCoolDown = 5000; //harmless magic number but should probably change it to a maxCooldown variable or something
            }
            combatant.GetComponent<CharacterFunctions>().reduceTurnCooldown(combatant.speed);
        }
    }

    public void WinCombat() 
    {
        gameState.overworldPaused = false;
        teamOne.Clear();
        foreach(CharacterData combatant in combatants)
        {
            Destroy(combatant.gameObject);
        }
        combatants.Clear();
        combatUI.SetActive(false);
        Debug.Log("WON CELEBRATE");
    }

    public void LoseCombat()
    {
        gameState.overworldPaused = false;
        teamTwo.Clear();
        foreach (CharacterData combatant in combatants)
        {
            Destroy(combatant.gameObject);
        }
        combatants.Clear();
        combatUI.SetActive(false);

        Debug.Log("LOST GET FUCKED");
    }
}
