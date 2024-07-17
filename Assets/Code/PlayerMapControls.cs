using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMapControls : MonoBehaviour
{
    public GameState gameState;
    public CombatManager combatManager;
    public EntityManager entityManager;
    public Tilemap tilemap;
    public float moveTime = 0.1f;

    private bool isMoving = false;
    private Vector3Int currentGridPosition;

    void Start()
    {
        currentGridPosition = tilemap.WorldToCell(transform.position);
        transform.position = tilemap.GetCellCenterWorld(currentGridPosition);
    }

    void Update()
    {
        if (!isMoving && !gameState.overworldPaused)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Move(Vector3Int.up);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                Move(Vector3Int.down);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                Move(Vector3Int.left);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                Move(Vector3Int.right);
            }
        }
    }

    void Move(Vector3Int direction)
    {
        Vector3Int targetGridPosition = currentGridPosition + direction;

        if (IsTileOccupied(targetGridPosition))
        {
            GameObject entity = entityManager.IsTileOccupied(targetGridPosition); //holy fuck the "IsTileOccupied" method is bad. Find a better way if you have time 
            Debug.Log(entity.name);
            combatManager.StartCombat(this.gameObject, entity);
        }

        else if (IsTileWalkable(targetGridPosition))
        {
            entityManager.UpdateEntityPosition(this.gameObject, targetGridPosition);
            StartCoroutine(MoveTo(targetGridPosition));
        }
    }

    bool IsTileWalkable(Vector3Int gridPosition)
    {
        TileBase tile = tilemap.GetTile(gridPosition);

        if (tile != null && (tile.name == "Tiles_0" || tile.name == "Tiles_3")) //not sure if I need the parantheses here
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