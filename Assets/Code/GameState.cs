using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public bool globalPaused;
    public bool overworldPaused; //do I need pauses like this? I feel like the combat bool is enough %99.99 of the time
    public bool combatPaused;
    public bool inCombat;
    public bool combatFinished;
    public bool waitingCombat;
    public List<GameObject> partiesWaitingCombat;
    public int deathCount;
    public Vector3Int checkpoint;

    public bool metHusk;
    public bool metFeralCat;
    public bool metCyborgHunter;

    private void Start()
    {
        checkpoint = new Vector3Int(-2, -5, 0);
    }
}
