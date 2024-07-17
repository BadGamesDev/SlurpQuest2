using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EntityManager : MonoBehaviour
{
    public Tilemap tilemap;
    private Dictionary<GameObject, Vector3Int> entityPositions;

    public int level1Parties;
    public int level2Parties;
    public int level3Parties;
    public int level4Parties;
    public int level5Parties;

    void Start()
    {
        entityPositions = new Dictionary<GameObject, Vector3Int>();
        StoreEntityPositions();
    }

    void StoreEntityPositions()
    {
        GameObject[] allEntities = GameObject.FindGameObjectsWithTag("Entity");

        foreach (GameObject entity in allEntities)
        {
            Vector3Int gridPosition = tilemap.WorldToCell(entity.transform.position);
            entityPositions[entity] = gridPosition;
        }
    }

    public Vector3Int GetEntityGridPosition(GameObject entity)
    {
        if (entityPositions.TryGetValue(entity, out Vector3Int position))
        {
            return position;
        }
        else
        {
            return Vector3Int.zero;
        }
    }

    public void UpdateEntityPosition(GameObject entity, Vector3Int newPos)
    {
        if (entityPositions.ContainsKey(entity))
        {
            entityPositions[entity] = newPos;
        }
        else
        {
            entityPositions.Add(entity, newPos);
        }
    }

    public GameObject IsTileOccupied(Vector3Int gridPosition)
    {
        foreach (var pair in entityPositions)
        {
            if (pair.Value == gridPosition)
            {
                return pair.Key;
            }
        }
        return null;
    }
}