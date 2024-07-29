using UnityEngine;

public class ObjectFunctions : MonoBehaviour
{
    public PlayerStats playerStats;
    public ObjectData ownData; //I'm not a big fan of naming everything like this, it should probably be something like objectData to make it clearer as characters etc. also have the same shit
    public OverworldUI overworldUI;
    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        overworldUI = FindObjectOfType<OverworldUI>();
    }
    public void GetInteracted()
    {
        if (ownData.objectType == "chest")
        {
            if(ownData.item == 0)
            {
                overworldUI.AddMessage("The chest is empty (Because you know, YOU just looted it).");
            }
            else if (ownData.item == 1) //I wouldn't need these disguting if statements with a proper inventory system but I really don't want to do it for such simple stuff
            {
                playerStats.catFood += 1;
                overworldUI.AddMessage("You have opened a chest and found a can of cat food!");
            }
            else if (ownData.item == 2)
            {
                playerStats.pizza += 1;
                overworldUI.AddMessage("You have opened a chest and found a slice of Pizza 5/5. HOLY SHIT!");
            }
            else if (ownData.item == 3)
            {
                playerStats.pizza += 1;
                overworldUI.AddMessage("You have opened a chest and found a can of beans.");
            }
            else if (ownData.item == 4)
            {
                playerStats.pizza += 1;
                overworldUI.AddMessage("You have opened a chest and found a bubble blower");
            }
            else if (ownData.item == 5)
            {
                playerStats.pizza += 1;
                overworldUI.AddMessage("You have opened a chest and found");
            }
            ownData.item = 0;
        }
    }
}
