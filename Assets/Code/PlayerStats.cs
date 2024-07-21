using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour //I feel like "PlayerData" would have been a more appropriate name but I'll keep this just out of sentimentality 
{
    public int level;
    public int xp;

    public List<CompanionData> unlockedCompanions;
    public List<CompanionData> activeCompanions;

    public int pizza; //I would have made an inventory script instead but considering I only need one inventory and there aren't any complicated mechanics needed I have just decided to make every item a variable
    public int catFood;
}
