using UnityEngine;
using UnityEngine.Tilemaps;

public class PartyAI : MonoBehaviour
{
    public GameState gameState;
    public Tilemap tilemap;
    public EntityManager entityManager;
    public float moveTime;
    public float moveDelay;

    private bool isMoving = false;
    private Vector3Int currentGridPosition;

    void Start()
    {
        tilemap = FindObjectOfType<Tilemap>();
        entityManager = FindObjectOfType<EntityManager>();
        gameState = FindObjectOfType<GameState>();

        currentGridPosition = tilemap.WorldToCell(transform.position);
        transform.position = tilemap.GetCellCenterWorld(currentGridPosition);
        StartCoroutine(Wander());
    }

    System.Collections.IEnumerator Wander()
    {
        while (true)
        {
            yield return new WaitForSeconds(moveDelay);
            
            if (!gameState.overworldPaused)
            {
                while (!isMoving)
                {
                    Vector3Int direction = GetRandomDirection();
                    Vector3Int targetGridPosition = currentGridPosition + direction;

                    if (IsTileWalkable(targetGridPosition) && !IsTileOccupied(targetGridPosition))
                    {
                        entityManager.UpdateEntityPosition(this.gameObject, targetGridPosition);
                        StartCoroutine(MoveTo(targetGridPosition));
                    }
                }
            }
            else
            {
                yield return null; // Wait for the next frame
            }
        }
    }

    Vector3Int GetRandomDirection()
    {
        int direction = Random.Range(0, 4);
        switch (direction)
        {
            case 0: return Vector3Int.up;
            case 1: return Vector3Int.down;
            case 2: return Vector3Int.left;
            case 3: return Vector3Int.right;
            default: return Vector3Int.zero;
        }
    }

    bool IsTileWalkable(Vector3Int gridPosition)
    {
        TileBase tile = tilemap.GetTile(gridPosition);

        if (tile != null && tile.name == "Tiles_0")
        {
            return true;
        }
        return false;
    }

    bool IsTileOccupied(Vector3Int gridPosition)
    {
        return entityManager.IsTileOccupied(gridPosition) != null;
    }

    System.Collections.IEnumerator MoveTo(Vector3Int targetGridPosition)
    {
        isMoving = true;
        Vector3 targetWorldPosition = tilemap.GetCellCenterWorld(targetGridPosition);
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(startPosition, targetWorldPosition, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetWorldPosition;
        currentGridPosition = targetGridPosition;
        isMoving = false;
    }
}
