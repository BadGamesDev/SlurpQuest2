using UnityEngine;

public class PartyFunctions : MonoBehaviour
{
    public GameState gameState;
    public OverworldUI overworldUI;
    public AudioManager audioManager;
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
        overworldUI = FindObjectOfType<OverworldUI>();
        audioManager = FindObjectOfType<AudioManager>();

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
                    combatManager.IntroduceCombat(entityOnTile);
                    gameState.waitingCombat = true;
                    gameState.partiesWaitingCombat.Add(gameObject);
                    gameState.partiesWaitingCombat.Add(entityOnTile.gameObject);
                }
                else if (entityOnTile.CompareTag("Object"))
                {
                    entityOnTile.GetComponent<ObjectFunctions>().GetInteracted();
                }
            }
        }

        else if (tilemapManager.IsTilePortal(targetGridPosition)) //I should probably make sure only the player can enter the portal, but if Slurp manages to fuck the game up so hard that an npc enters... Well I guess she deserved it lmao
        {
            if (gameState.portalIntroduction)
            {
                gameState.progress += 1;
                gameState.checkpoint = gameState.checkpointList[gameState.progress];

                transform.position = gameState.checkpoint;
                Vector3Int playerGridPos = tilemapManager.tilemap.WorldToCell(gameState.checkpoint);
                entityTracker.UpdateEntityPosition(gameObject, playerGridPos);
                GetComponent<PartyFunctions>().currentGridPosition = playerGridPos;

                gameState.portalIntroduction = false;

                if (gameState.progress == 2)
                {
                    audioManager.forestTheme.Stop();
                    audioManager.desertTheme.Play();

                    overworldUI.AddMessage("This is the content desert. It used to be a lush paradise but now that the dark lord has taken over, it has become a barren and desolate place. Any creature who managed to survive here is sure to be very dangerous!");
                }

                if (gameState.progress == 3)
                {
                    audioManager.desertTheme.Stop();
                    audioManager.snowTheme.Play();

                    overworldUI.AddMessage("Huh! I don't remember this place. It sure is cold though.");
                }

                if (gameState.progress == 4)
                {
                    audioManager.snowTheme.Stop();
                    audioManager.corruptionTheme.Play();

                    overworldUI.AddMessage("You have managed to come to the very heart of Twitch! This is where all the great streamers and their viewers come from. The dark lord's presence has completely twisted and corrupted this place.");
                }

                if (gameState.progress == 5)
                {
                    GameObject.Find("PlayerParty").GetComponent<PartyData>().pos2 = null;
                    GameObject.Find("PlayerParty").GetComponent<PartyData>().pos3 = null;

                    FindObjectOfType<PlayerStats>().activeCompanions.Remove(FindObjectOfType<PlayerStats>().activeCompanions[0]);
                    FindObjectOfType<PlayerStats>().activeCompanions.Remove(FindObjectOfType<PlayerStats>().activeCompanions[1]);
                    FindObjectOfType<PlayerStats>().unlockedCompanions.Remove(FindObjectOfType<PlayerStats>().unlockedCompanions[1]);
                    FindObjectOfType<PlayerStats>().unlockedCompanions.Remove(FindObjectOfType<PlayerStats>().unlockedCompanions[2]);
                    FindObjectOfType<PlayerStats>().unlockedCompanions.Remove(FindObjectOfType<PlayerStats>().unlockedCompanions[3]);
                    FindObjectOfType<PlayerStats>().unlockedCompanions.Remove(FindObjectOfType<PlayerStats>().unlockedCompanions[4]);
                    FindObjectOfType<PlayerStats>().unlockedCompanions.Remove(FindObjectOfType<PlayerStats>().unlockedCompanions[5]);

                    overworldUI.member2 = null;
                    overworldUI.member3 = null;
                    overworldUI.AddMessage("The curse feels suffocating here. You don't know how much longer you can keep going.");
                }

                if (gameState.progress == 7)
                {
                    audioManager.forestTheme.Stop();
                    audioManager.creditTheme.Play();

                    FindObjectOfType<CinematicManager>().creditsScene = true;
                }
            }

            else if (!gameState.portalIntroduction && gameState.portalCount == 0)
            {
                gameState.trickster1.SetActive(true);

                overworldUI.AddMessage("This looks like some sort of portal. You try entering it but the magic of the portal refuses to allow you through.");
                overworldUI.AddMessage("Mysterious voice: Psst! Slurp!");
                overworldUI.AddMessage("The Trickster: They call me the Trickster, I've been watching you fight and you have some nice moves! Who knows? Maybe you do have a chance against the dark lord!");
                overworldUI.AddMessage("The Trickster: I used to work for him, I know all about these portals. Here, let me open it up for you! *Trickster says some words in a language you do not know*");
                overworldUI.AddMessage("The Trickster: Alright it should work now. Try entering the portal again!");

                gameState.portalIntroduction = true;
                gameState.portalCount += 1;
            }

            else if (!gameState.portalIntroduction && gameState.portalCount == 1)
            {
                gameState.trickster2.SetActive(true);

                overworldUI.AddMessage("The Trickster: That was nice!");
                overworldUI.AddMessage("The Trickster: Time to opened up this one too. But I must warn you, you might need some warm clothes where you are going.");

                gameState.portalIntroduction = true;
                gameState.portalCount += 1;
            }

            else if (!gameState.portalIntroduction && gameState.portalCount == 2)
            {
                gameState.trickster3.SetActive(true);

                overworldUI.AddMessage("The Trickster: Nice work! Time to leave this frozen place.");
                overworldUI.AddMessage("The Trickster: You must be careful, the corruption is getting stronger and stronger as you get closer to the dark lord.");

                gameState.portalIntroduction = true;
                gameState.portalCount += 1;
            }

            else if (!gameState.portalIntroduction && gameState.portalCount == 3)
            {
                gameState.trickster4.SetActive(true);

                overworldUI.AddMessage("The Trickster: What Asmongold told you was true. There is one more portal you need to go through.");
                overworldUI.AddMessage("The Trickster: But this time you must do it alone! The corruption is far too strong for your friends to handle.");

                gameState.portalIntroduction = true;
                gameState.portalCount += 1;
            }

            else if (!gameState.portalIntroduction && gameState.portalCount == 4)
            {
                overworldUI.AddMessage("It looks like you have power over all these portals now. You can go through without anyone elses help.");

                gameState.portalIntroduction = true;
                gameState.portalCount += 1;
            }

            else if (!gameState.portalIntroduction && gameState.portalCount == 5)
            {
                overworldUI.AddMessage("Well I guess this is goodbye. Enter the portal when you want to finish the game.");

                gameState.portalIntroduction = true;
                gameState.portalCount += 1;
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
