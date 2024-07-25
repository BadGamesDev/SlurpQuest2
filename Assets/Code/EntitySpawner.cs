using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EntitySpawner : MonoBehaviour
{
    public GameState gameState;
    public TilemapManager tileManager;
    public EntityTracker entityTracker;
    public Tilemap tilemap;
    public GameObject npcPartyPrefab;
    public GameObject huskPrefab;

    public void Update()
    {
        if (gameState.progress == 0 || gameState.progress == 1) //grass level
        {
            if (entityTracker.partyCounts[0] < 10)
            {
                SpawnHostileParty(1);
            }
        }

        else if (gameState.progress == 2) //desert level
        {
            if (entityTracker.partyCounts[1] < 10)
            {
                SpawnHostileParty(2);
            }
        }

        else if (gameState.progress == 3) //ice level
        {
            if (entityTracker.partyCounts[2] < 10)
            {
                SpawnHostileParty(3);
            }
        }

        else if (gameState.progress == 4) //corruption level
        {
            if (entityTracker.partyCounts[3] < 10)
            {
                SpawnHostileParty(4);
            }
        }
    }

    public void SpawnHostileParty(int level)
    {
        if (level == 1)
        {
            int randomX = Random.Range(-9, 9);
            int randomY = Random.Range(-2, 8);
            Vector2 randomPosition = new Vector2(randomX + 0.5f, randomY + 0.5f); //maybe change some stuff so that you don't have to add 0.5 to everything?

            Vector3Int gridPosition = tilemap.WorldToCell(randomPosition); //player map controls already have a method for checking if a tile is walkable and if there is someone at the tile
                                                                           //it might be a good idea to use the same methods everywhere instead of writing again
            TileBase spawntile = tilemap.GetTile(gridPosition);

            if (spawntile != null && spawntile.name == "grass" && !tileManager.IsTileOccupied(gridPosition))
            {
                GameObject newParty = Instantiate(npcPartyPrefab, randomPosition, Quaternion.identity);
                PartyData newPartyData = newParty.GetComponent<PartyData>();

                newPartyData.level = 1;
                newPartyData.pos1 = huskPrefab;

                entityTracker.partyCounts[0] += 1;
                entityTracker.UpdateEntityPosition(newParty, gridPosition);
            }
        }
        
        else if (level == 2)
        {
            int randomX = Random.Range(72, 94);
            int randomY = Random.Range(0, 22);
            Vector2 randomPosition = new Vector2(randomX + 0.5f, randomY + 0.5f);

            Vector3Int gridPosition = tilemap.WorldToCell(randomPosition);
            TileBase spawntile = tilemap.GetTile(gridPosition);

            if (spawntile != null && spawntile.name == "sand" && !tileManager.IsTileOccupied(gridPosition))
            {
                GameObject newParty = Instantiate(npcPartyPrefab, randomPosition, Quaternion.identity);
                PartyData newPartyData = newParty.GetComponent<PartyData>();

                newPartyData.level = 2;
                newPartyData.pos1 = huskPrefab;

                entityTracker.partyCounts[1] += 1;
                entityTracker.UpdateEntityPosition(newParty, gridPosition);
            }
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
