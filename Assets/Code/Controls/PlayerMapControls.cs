using UnityEngine;

public class PlayerMapControls : MonoBehaviour
{
    public GameState gameState;
    public CombatManager combatManager;
    public EntityTracker entityManager;
    public TilemapManager tilemapManager;
    public PartyFunctions partyFunctions;

    void Update()
    {
        if (!partyFunctions.isMoving && !gameState.overworldPaused)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                partyFunctions.TryToMove(Vector3Int.up);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                partyFunctions.TryToMove(Vector3Int.down);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                partyFunctions.TryToMove(Vector3Int.left);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                partyFunctions.TryToMove(Vector3Int.right);
            }
        }
    }
}