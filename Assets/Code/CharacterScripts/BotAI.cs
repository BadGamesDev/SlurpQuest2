using UnityEngine;

public class BotAI : MonoBehaviour
{
    public GameObject bot;
    public PrefabLoader prefabLoader;
    public CombatManager combatManager;
    public CombatFunctions combatFunctions;
    public CombatUI combatUI;
    public CharacterData ownData;

    void Start()
    {
        combatManager = FindObjectOfType<CombatManager>();
        combatFunctions = FindObjectOfType<CombatFunctions>();
        prefabLoader = FindObjectOfType<PrefabLoader>();
        combatUI = FindObjectOfType<CombatUI>();

        bot = prefabLoader.BotPrefab;
    }

    public void FixedUpdate()
    {
        if (combatManager.turnHaver == ownData && !ownData.selfStatusEffects.Contains(StatusEffectDatabase.stun) && ownData.team == 1 && combatManager.teamOne.Count != 0)
        {
            if (combatManager.teamTwo.Count < 3)
            {
                int slotToSpawn = 0;

                if (combatManager.spawnSlots[4].childCount == 0)
                {
                    slotToSpawn = 4;
                }

                else if (combatManager.spawnSlots[5].childCount == 0) //I can just make this an else statement but better safe than sorry
                {
                    slotToSpawn = 5;
                }
                
                GameObject newBot = Instantiate(bot, combatManager.spawnSlots[slotToSpawn].position, Quaternion.identity);
                newBot.transform.SetParent(combatManager.spawnSlots[slotToSpawn]);
                CharacterData newBotData = newBot.GetComponent<CharacterData>();
                combatManager.combatants.Add(newBotData);
                newBotData.team = 1;
                combatManager.teamTwo.Add(newBotData);
                newBotData.turnCoolDown = 3000;

                combatManager.combatPauseCooldown += 2;
                combatUI.combatText.text = "The bot summoned another bot!";
            }
            else
            {
                CharacterData target = combatManager.teamOne[Random.Range(0, combatManager.teamOne.Count)];
                combatFunctions.Attack(ownData, target);
            }
            combatManager.turnHaver = null;
        }
    }
}
