using UnityEngine;
using UnityEngine.EventSystems;

public class CompanionSlotClickHandler : MonoBehaviour, IPointerClickHandler
{
    public PrefabLoader prefabLoader;
    public OverworldUI overWorldUI;
    public PartyData playerParty;
    public PlayerStats playerStats;
    public int slotIndex;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (overWorldUI.pickedCompanion != null)
        {
            if (slotIndex == 1)
            {
                playerParty.pos2 = prefabLoader.HoneyPrefab; //add an  if check for the prefab
                CharacterData oldData = playerParty.pos2.GetComponent<CharacterData>();
                CopyCharacterData(oldData, overWorldUI.pickedCompanion);
            }
            else if (slotIndex == 2)
            {
                playerParty.pos3 = prefabLoader.HoneyPrefab;
                CharacterData oldData = playerParty.pos3.GetComponent<CharacterData>();
                CopyCharacterData(oldData, overWorldUI.pickedCompanion);
            }
            playerStats.activeCompanions.Add(overWorldUI.pickedCompanion);
            overWorldUI.pickedCompanion = null;
            overWorldUI.CheckSelectableCompanions();
        }
    }

    public void CopyCharacterData(CharacterData oldData, CompanionData newData)
    {
        oldData.characterName = newData.characterName;
        oldData.maxHealth = newData.maxHealth;
        oldData.health = newData.health;
        oldData.defence = newData.defence;
        oldData.accuracy = newData.accuracy;
        oldData.damage = newData.damage;
        oldData.speed = newData.speed;
        oldData.turnCoolDown = newData.turnCoolDown;
    }
}
