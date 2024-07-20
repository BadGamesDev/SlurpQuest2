using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    public EntityTracker entityTracker;
    public Tilemap tilemap;

    public bool IsTileWalkable(Vector3Int gridPosition)
    {
        TileBase tile = tilemap.GetTile(gridPosition);

        if (tile != null && (tile.name == "Tiles_0" || tile.name == "Tiles_3")) //not sure if I need the parantheses here
        {
            return true;
        }
        return false;
    }

    public GameObject IsTileOccupied(Vector3Int gridPosition)
    {
        foreach (var pair in entityTracker.entityPositions)
        {
            if (pair.Value == gridPosition)
            {
                return pair.Key;
            }
        }
        return null;
    }
}
