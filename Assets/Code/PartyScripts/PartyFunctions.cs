using UnityEngine;

public class PartyFunctions : MonoBehaviour
{
    public EntityTracker entityTracker;
    public TilemapManager tilemapManager;
    public CombatManager combatManager;

    public PartyData ownData;
    
    public Vector3Int currentGridPosition;
    private Vector3Int targetGridPosition;
    private Vector3 targetWorldPosition;

    public bool isMoving;

    void Start()
    {
        tilemapManager = FindObjectOfType<TilemapManager>();
        entityTracker = FindObjectOfType<EntityTracker>();
        combatManager = FindObjectOfType<CombatManager>();

        currentGridPosition = tilemapManager.tilemap.WorldToCell(transform.position);
        transform.position = tilemapManager.tilemap.GetCellCenterWorld(currentGridPosition);
    }

    private void FixedUpdate()
    {
        if (isMoving == true)
        {
            Move();
        }
    }

    public void TryToMove(Vector3Int direction)
    {
        targetGridPosition = currentGridPosition + direction;
        GameObject entityOnTile = tilemapManager.IsTileOccupied(targetGridPosition);

        if (entityOnTile != null)
        {
            if(CompareTag("Player"))
            {
                if (entityOnTile.CompareTag("Npc"))
                {
                    combatManager.StartCombat(gameObject, entityOnTile);
                }
                else if (entityOnTile.CompareTag("Object"))
                {
                    entityOnTile.GetComponent<ObjectFunctions>().GetInteracted();
                }
            }
        }

        else if (tilemapManager.IsTileWalkable(targetGridPosition))
        {
            StartMovingTo(direction);
        }
    }

    public void StartMovingTo(Vector3Int direction)
    {
        isMoving = true;
        targetWorldPosition = tilemapManager.tilemap.CellToWorld(currentGridPosition + direction);
        currentGridPosition = targetGridPosition;
        entityTracker.UpdateEntityPosition(gameObject, currentGridPosition);
    }

    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetWorldPosition, 4 * Time.deltaTime); //magic number but it is ok.
        if(transform.position == targetWorldPosition)
        {
            isMoving = false;
        }
    }

    public void Die()
    {
        entityTracker.entityPositions.Remove(gameObject);
        if (ownData.level != 0)//this works nicely though I feel like it could be simpler
        {
            entityTracker.partyCounts[ownData.level - 1] -= 1;
        }
        Destroy(gameObject);
    }
}
