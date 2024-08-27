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
    public int progress;
    public Vector3Int checkpoint;
    public List<Vector3Int> checkpointList;

    public bool metHusk;
    public bool metShill;
    public bool metTroll;
    public bool metBot;
    public bool metFeralCat;
    public bool metCyborgHunter;
    public bool metMaddizz;
    public bool metTheWarlock;
    public bool metAsmongold;
    public bool metHeado;
    public bool metTheAuditor;

    private void Start()
    {
        checkpointList.Add(new Vector3Int(-1, -7, 0));
        checkpointList.Add(new Vector3Int(0, 21, 0));
        checkpointList.Add(new Vector3Int(83, -7, 0));
        checkpointList.Add(new Vector3Int(185, -7, 0));
        checkpointList.Add(new Vector3Int(308, -7, 0));
        checkpointList.Add(new Vector3Int(316, 72, 0));
        checkpointList.Add(new Vector3Int(408, 2, 0));

        checkpoint = checkpointList[0];
    }
}
