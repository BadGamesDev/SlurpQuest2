using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EntityTracker : MonoBehaviour
{
    public Tilemap tilemap;
    public Dictionary<GameObject, Vector3Int> entityPositions;

    public int[] partyCounts;

    void Start()
    {
        entityPositions = new Dictionary<GameObject, Vector3Int>();
        StoreEntityPositions();
    }

    void StoreEntityPositions()
    {
        List<GameObject> allEntities = new List<GameObject>();

        allEntities.AddRange(GameObject.FindGameObjectsWithTag("Player"));

        allEntities.AddRange(GameObject.FindGameObjectsWithTag("Npc"));

        allEntities.AddRange(GameObject.FindGameObjectsWithTag("Object"));


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
}