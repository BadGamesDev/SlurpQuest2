using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    public EntityTracker entityTracker;
    public Tilemap tilemap;

    public bool IsTileWalkable(Vector3Int gridPosition)
    {
        TileBase tile = tilemap.GetTile(gridPosition);

        if (tile != null && (tile.name == "grass" || tile.name == "sand" || tile.name == "bridge")) //Tiles should have a "isWalkable" bool or something instead of this dumb shit I'm doing
        {
            return true;
        }
        return false;
    }

    public bool IsTilePortal(Vector3Int gridPosition)
    {
        TileBase tile = tilemap.GetTile(gridPosition);

        if (tile != null && (tile.name == "grassPortal" || tile.name == "sandPortal" || tile.name == "snowPortal" || tile.name == "corruptionPortal")) //Tiles should have a "isWalkable" bool or something instead of this dumb shit I'm doing
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
