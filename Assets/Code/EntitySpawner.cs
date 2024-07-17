using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EntitySpawner : MonoBehaviour
{
    public EntityManager entityManager;
    public Tilemap tilemap;
    public GameObject npcPartyPrefab;
    public GameObject huskPrefab;

    public void Update()
    {
        if (entityManager.level1Parties < 30)
        {
            SpawnHostileParty(1);
        }
    }

    public void SpawnHostileParty(int level)
    {
        if (level == 1)
        {
            int randomX = Random.Range(-9, 9);
            int randomY = Random.Range(-6, 6);
            Vector2 randomPosition = new Vector2(randomX + 0.5f, randomY + 0.5f); //maybe change some stuff so that you don't have to add 0.5 to everything?

            Vector3Int gridPosition = tilemap.WorldToCell(randomPosition); //player map controls already have a method for checking if a tile is walkable and if there is someone at the tile
                                                                           //it might be a good idea to use the same methods everywhere instead of writing again
            TileBase spawntile = tilemap.GetTile(gridPosition);

            if (spawntile != null && spawntile.name == "Tiles_0" && !entityManager.IsTileOccupied(gridPosition))
            {
                GameObject newParty = Instantiate(npcPartyPrefab, randomPosition, Quaternion.identity);
                PartyData newPartyData = newParty.GetComponent<PartyData>();

                int randomInt = Random.Range(0, 2);

                if (randomInt == 0)
                {
                    newPartyData.pos1 = huskPrefab;
                    newPartyData.pos2 = huskPrefab;
                }

                if (randomInt == 1)
                {
                    newPartyData.pos1 = huskPrefab;
                    newPartyData.pos2 = huskPrefab;
                    newPartyData.pos3 = huskPrefab;
                }

                entityManager.level1Parties += 1;
                entityManager.UpdateEntityPosition(newParty, gridPosition);
            }
        }
        
        else if (level == 2)
        {
            GameObject newParty = Instantiate(npcPartyPrefab);
            PartyData newPartyData = newParty.GetComponent<PartyData>();
        }
        
        else if (level == 3)
        {
            GameObject newParty = Instantiate(npcPartyPrefab);
            PartyData newPartyData = newParty.GetComponent<PartyData>();
        }
        
        else if (level == 4)
        {
            GameObject newParty = Instantiate(npcPartyPrefab);
            PartyData newPartyData = newParty.GetComponent<PartyData>();
        }
        
        else if (level == 5)
        {
            GameObject newParty = Instantiate(npcPartyPrefab);
            PartyData newPartyData = newParty.GetComponent<PartyData>();
        }
    }
}
