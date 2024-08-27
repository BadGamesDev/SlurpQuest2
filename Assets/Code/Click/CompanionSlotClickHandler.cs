using UnityEngine;
using UnityEngine.EventSystems;

public class CompanionSlotClickHandler : MonoBehaviour, IPointerClickHandler
{
    public PrefabLoader prefabLoader;
    public OverworldUI overWorldUI;
    public PartyData playerParty;
    public PlayerStats playerStats;
    public GameObject slot;
    public int slotIndex;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (slotIndex == 1)
        { 
            slot = playerParty.pos2;
        }

        if (slotIndex == 2)
        {
            slot = playerParty.pos3;
        }

        if (!string.IsNullOrEmpty(overWorldUI.pickedCompanion.characterName) && slot == null) //checking name rather tha companion itself to prevent some werid bugs, treating the symptom rather than the disease
        { //THERE IS SOME FUCKING BUG HERE AND I DON'T KNOW WHY IT IS HAPPENING
            GameObject prefab = new GameObject();

            if (overWorldUI.pickedCompanion.characterName == "Honey")
            {
                prefab = prefabLoader.HoneyPrefab;
            }

            if (overWorldUI.pickedCompanion.characterName == "Digi63")
            {
                prefab = prefabLoader.Digi63Prefab;
            }

            if (overWorldUI.pickedCompanion.characterName == "Jaydizz")
            {
                prefab = prefabLoader.JaydizzPrefab;
            }

            if (overWorldUI.pickedCompanion.characterName == "Cndk99")
            {
                prefab = prefabLoader.Cndk99Prefab;
            }

            if (overWorldUI.pickedCompanion.characterName == "OneViolence")
            {
                prefab = prefabLoader.OneViolencePrefab;
            }

            if (slotIndex == 1)
            {
                playerParty.pos2 = prefab;
                CharacterData oldData = playerParty.pos2.GetComponent<CharacterData>();
                CopyCharacterData(oldData, overWorldUI.pickedCompanion);
                overWorldUI.dismissCompanion0.gameObject.SetActive(true);
                overWorldUI.statButton0.gameObject.SetActive(true);

                overWorldUI.member2.sprite = oldData.avatar;
            }
            else if (slotIndex == 2)
            {
                playerParty.pos3 = prefab;
                CharacterData oldData = playerParty.pos3.GetComponent<CharacterData>();
                CopyCharacterData(oldData, overWorldUI.pickedCompanion);
                overWorldUI.dismissCompanion1.gameObject.SetActive(true);
                overWorldUI.statButton1.gameObject.SetActive(true);

                overWorldUI.member3.sprite = oldData.avatar;
            }
            playerStats.activeCompanions.Add(overWorldUI.pickedCompanion);
            overWorldUI.pickedCompanion = null;
            overWorldUI.CheckSelectableCompanions();
        }
    }

    public void CopyCharacterData(CharacterData oldData, CompanionData newData)
    {
        oldData.characterName = newData.characterName;
        oldData.level = newData.level;
        oldData.maxHealth = newData.maxHealth;
        oldData.health = newData.health;
        oldData.defence = newData.defence;
        oldData.accuracy = newData.accuracy;
        oldData.damage = newData.damage;
        oldData.speed = newData.speed;
        oldData.turnCoolDown = newData.turnCoolDown;
    }
}
