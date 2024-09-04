using UnityEngine;

public class ObjectFunctions : MonoBehaviour
{
    public PlayerStats playerStats;
    public ObjectData ownData; //I'm not a big fan of naming everything like this, it should probably be something like objectData to make it clearer as characters etc. also have the same shit
    public OverworldUI overworldUI;

    public bool delete;

    private void Start()
    {
        delete = false;
        playerStats = FindObjectOfType<PlayerStats>();
        overworldUI = FindObjectOfType<OverworldUI>();
    }

    public void Update()
    {
        if (delete && overworldUI.textQueue.Count == 0)
        {
            Destroy(gameObject);
        }
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

                ownData.item = 0;
            }
            else if (ownData.item == 2)
            {
                playerStats.pizza += 1;
                overworldUI.AddMessage("You have opened a chest and found a slice of Pizza 5/5. HOLY FUCKING SHIT!");

                ownData.item = 0;
            }
            else if (ownData.item == 3)
            {
                playerStats.gamblingChip += 1;
                overworldUI.AddMessage("You have opened a chest and found a gambling chip! Hell yeah!");

                ownData.item = 0;
            }
            else if (ownData.item == 4)
            {
                overworldUI.AddMessage("Zepper: Slurp! What are you doing here? The curse is too strong! Everyone else got corrupted by it except me. I think it is because of this... *zepper points at the Pizza 5/5 shirt he is wearing*");
                overworldUI.AddMessage("Zepper: You know what? If you are going to continue, you should take it, you need it more than I do!");
                overworldUI.AddMessage("Zepper gives his shirt to you. Not only does is negate the effects of the curse you were feeling, but it also gives you a great surge of power. This will surely help you in the fight ahead.");
                overworldUI.AddMessage("Zepper: Go ahead Slurp, don't worry I'll be fine. Now go and save everyone!");

                CompanionData slurp = overworldUI.FindUnlockedCompanionByName("Slurp");
                
                slurp.maxHealth += 1000;
                slurp.health += 1000;
                slurp.defence += 50;
                slurp.speed += 5;
                slurp.damage += 400;

                ownData.item = 5;
            }

            else if (ownData.item == 5)
            {
                overworldUI.AddMessage("Zepper: Slurp I know I said I would be fine but I really need you to hurry the fuck up or else I will literally die here. FUCKING GOOOOO!");
            }

            else if (ownData.item == 6)
            {
                CompanionData cndk99 = new CompanionData
                {
                    characterName = "Cndk99",
                    maxHealth = 80,
                    health = 80,
                    defence = 0,
                    accuracy = 100,
                    damage = 15,
                    speed = 12,
                    turnCoolDown = 3000,
                };

                cndk99.skills.Add(SkillDatabase.washedUpBoyfriend);
                cndk99.skills.Add(SkillDatabase.rap);

                overworldUI.AddMessage("Cndk99: Oh, hey Slurp! Nice to see you here. I was just chilling in the snow. I really like this place. The dark lord was going to corrupt me too but he said that I was too annoying so he just left me here.");
                overworldUI.AddMessage("Cndk99: I guess I should join you if you are going to fight him. You have no chance without my skills!");
                overworldUI.AddMessage("Damn! This guy looks really strong. I bet he is extremely smart too! The rest of the game will be so easy with him on your side!");
                playerStats.unlockedCompanions.Add(cndk99);
                overworldUI.LevelUpCheck();
                delete = true;
            }

            else if (ownData.item == 7)
            {
                overworldUI.AddMessage("Hank: Fucker is finally dead! But this is not the end. (Developer's note: I actually asked Krinkels if Hank can speak. He told me \"Allegedly yes\".)");
                overworldUI.AddMessage("Hank: I am... sorry Slurp...");
                overworldUI.AddMessage("Hank: But I have to go. You will be in danger as long as I'm with you.");
                overworldUI.AddMessage("OH GOD OH FUCK HANK IS ABOUT TO LEAVE! WHAT WILL YOU DO?");
                overworldUI.hankChoice = true;
            }

            else if (ownData.item == 8)
            {
                playerStats.GainXP(100);
                playerStats.noLifePoints += 100;
                overworldUI.AddMessage("You have opened the chest and found 100 nolifepoints. Nice!");
                ownData.item = 0;
            }

            else if (ownData.item == 9)
            {
                overworldUI.AddMessage("The Trickster: I'd love to chat but you should go. The portal is open.");
            }

            else if (ownData.item == 10)
            {
                playerStats.GainXP(300);
                playerStats.noLifePoints += 300;
                overworldUI.AddMessage("You have opened the chest and found 300 nolifepoints. Nice!");
                ownData.item = 0;
            }

            else if (ownData.item == 11)
            {
                overworldUI.AddMessage("mrinkredible18: Slurp! You did it! You saved everyone! But anyways don't let me keep you, everyone is waiting, just keep moving, you'll see them.");
                overworldUI.AddMessage("mrinkredible18: Oh also, you might have noticed I don't have legs. That is simply because everyone in Slurp Quest™ universe has evolved beyond the need for legs. We just move by floating. This also explains why there are no walking animations.");
                overworldUI.AddMessage("mrinkredible18: Yes you heard that right. Things aren't this way because cndk99 is a lazy fuck who can't animate shit. Everything actually has a lore reason!");
                ownData.item = 12;
            }

            else if (ownData.item == 12)
            {
                overworldUI.AddMessage("mrinkredible18: Come on Slurp hurry up! They are all waiting for you!");
            }

            else if (ownData.item == 13)
            {
                overworldUI.AddMessage("Krye: I guess I should thank you Slurp! Now I can finally go back to my girlfriend. I'd love to stay for the celebration but my 100% real girlfriend goes to another school so I should be on my way.");
                ownData.item = 14;
            }

            else if (ownData.item == 14)
            {
                overworldUI.AddMessage("Krye: I know you want to hear all about my girlfriend but please, I'm really in a hurry.");
            }

            else if (ownData.item == 15)
            {
                overworldUI.AddMessage("Please enter the twitch username of the coolest person ever to get access to super duper chest of infinite riches:");
                overworldUI.passwordField.gameObject.SetActive(true);
            }

            else if (ownData.item == 16)
            {
                overworldUI.AddMessage("This chest is completely empty. Just like the was majority of online content these days.");
            }
        }
    }
}
