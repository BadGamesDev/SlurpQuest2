using UnityEngine;
using UnityEngine.Tilemaps;

public class PartyAI : MonoBehaviour
{
    public GameState gameState;
    public TilemapManager tilemapManager;
    public EntityTracker entityManager;
    public PartyFunctions partyFunctions;

    private Vector3Int currentGridPosition;
    private float moveCooldown;

    void Start()
    {
        tilemapManager = FindObjectOfType<TilemapManager>();
        entityManager = FindObjectOfType<EntityTracker>();
        gameState = FindObjectOfType<GameState>();

        currentGridPosition = tilemapManager.tilemap.WorldToCell(transform.position);
        transform.position = tilemapManager.tilemap.GetCellCenterWorld(currentGridPosition);
    }

    private void FixedUpdate()
    {
        if (!gameState.overworldPaused && !gameState.globalPaused)
        { 
            moveCooldown -= Time.deltaTime;
            if (moveCooldown < 0 && partyFunctions.isMoving == false)
            {
                Vector3Int moveDir = GetRandomDirection();
                partyFunctions.TryToMove(moveDir);
                if(partyFunctions.isMoving == true)
                {
                    moveCooldown = 2;
                }
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
}
