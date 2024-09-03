using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class EntitySpawner : MonoBehaviour
{
    public GameState gameState;
    public TilemapManager tileManager;
    public EntityTracker entityTracker;
    public ImageLoader imageLoader;
    public Tilemap tilemap;
    
    public GameObject npcPartyPrefab;
    public GameObject huskPrefab;
    public GameObject shillPrefab;
    public GameObject trollPrefab;
    public GameObject botPrefab;

    public void Update()
    {
        if (gameState.progress == 0 || gameState.progress == 1) //grass level
        {
            if (entityTracker.partyCounts[0] < 8)
            {
                SpawnHostileParty(1);
            }
        }

        else if (gameState.progress == 2) //desert level
        {
            if (entityTracker.partyCounts[1] < 12)
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
                newParty.GetComponent<SpriteRenderer>().sprite = imageLoader.Husk;
                newParty.transform.localScale = new Vector2(0.17f, 0.17f);

                newPartyData.level = 1;
                int enemyCount = Random.Range(1, 4);
                
                if (enemyCount == 1)
                {
                    newPartyData.pos1 = huskPrefab;
                }
                else if (enemyCount == 2)
                {
                    newPartyData.pos1 = huskPrefab;
                    newPartyData.pos2 = huskPrefab;
                }
                else if (enemyCount == 3)
                {
                    newPartyData.pos1 = huskPrefab;
                    newPartyData.pos2 = huskPrefab;
                    newPartyData.pos3 = huskPrefab;
                }

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
                newParty.GetComponent<SpriteRenderer>().sprite = imageLoader.Shill;
                newParty.transform.localScale = new Vector2(0.40f, 0.35f);

                newPartyData.level = 2;
                int enemyCount = Random.Range(1, 4);
                
                if (enemyCount == 1)
                {
                    newPartyData.pos1 = shillPrefab;
                }
                
                else if (enemyCount == 2)
                {
                    newPartyData.pos1 = shillPrefab;
                    newPartyData.pos2 = shillPrefab;
                }

                else if (enemyCount == 3)
                {
                    newPartyData.pos1 = shillPrefab;
                    newPartyData.pos2 = shillPrefab;
                    newPartyData.pos3 = shillPrefab;
                }

                entityTracker.partyCounts[1] += 1;
                entityTracker.UpdateEntityPosition(newParty, gridPosition);
            }
        }
        
        else if (level == 3)
        {
            int randomX = Random.Range(180, 220);
            int randomY = Random.Range(-10, 18);
            Vector2 randomPosition = new Vector2(randomX + 0.5f, randomY + 0.5f);

            Vector3Int gridPosition = tilemap.WorldToCell(randomPosition);
            TileBase spawntile = tilemap.GetTile(gridPosition);

            if (spawntile != null && spawntile.name == "snow" && !tileManager.IsTileOccupied(gridPosition))
            {
                GameObject newParty = Instantiate(npcPartyPrefab, randomPosition, Quaternion.identity);
                PartyData newPartyData = newParty.GetComponent<PartyData>();
                newParty.GetComponent<SpriteRenderer>().sprite = imageLoader.Troll;
                newParty.transform.localScale = new Vector2(0.16f, 0.16f);

                newPartyData.level = 3;
                int enemyCount = Random.Range(1, 4);

                if (enemyCount == 1)
                {
                    newPartyData.pos1 = trollPrefab;
                }
                else if (enemyCount == 2)
                {
                    newPartyData.pos1 = trollPrefab;
                    newPartyData.pos2 = trollPrefab;
                }
                else if (enemyCount == 3)
                {
                    newPartyData.pos1 = trollPrefab;
                    newPartyData.pos2 = trollPrefab;
                    newPartyData.pos3 = trollPrefab;
                }

                entityTracker.partyCounts[2] += 1;
                entityTracker.UpdateEntityPosition(newParty, gridPosition);
            }
        }
        
        else if (level == 4)
        {
            int randomX = Random.Range(300, 330);
            int randomY = Random.Range(-2, 20);
            Vector2 randomPosition = new Vector2(randomX + 0.5f, randomY + 0.5f); //maybe change some stuff so that you don't have to add 0.5 to everything?

            Vector3Int gridPosition = tilemap.WorldToCell(randomPosition); //player map controls already have a method for checking if a tile is walkable and if there is someone at the tile
                                                                           //it might be a good idea to use the same methods everywhere instead of writing again
            TileBase spawntile = tilemap.GetTile(gridPosition);

            if (spawntile != null && spawntile.name == "corruption" && !tileManager.IsTileOccupied(gridPosition))
            {
                GameObject newParty = Instantiate(npcPartyPrefab, randomPosition, Quaternion.identity);
                PartyData newPartyData = newParty.GetComponent<PartyData>();
                newParty.GetComponent<SpriteRenderer>().sprite = imageLoader.Bot;
                newParty.transform.localScale = new Vector2(0.15f, 0.12f);

                newPartyData.level = 4;
                newPartyData.pos1 = botPrefab;

                entityTracker.partyCounts[3] += 1;
                entityTracker.UpdateEntityPosition(newParty, gridPosition);
            }
        }
    }
}
