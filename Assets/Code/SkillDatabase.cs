using UnityEngine;

public class SkillDatabase : MonoBehaviour //got forced me to put this shit in the editor to humble me for not learning all the global shit properly
{
    public ImageLoader imageLoader;

    public static Skill sevenYears = new() { skillName = "7 Fucking Years", skillDesc = "Slurp gets more resilient with time! She gainst 1 defence each turn. At turn 7 she stops gaining defence but unlocks her ultimate ability if she is at least level 4.",hostile = false };
    public static Skill dancingMaster = new() { skillName = "Dancing Master", skillDesc = "Slurp starts doing some wild dance moves. This ability deals double damage but has a chance to injure Slurp instead of hitting the enemy.", hostile = true };
    public static Skill tedTalk = new() { skillName = "Ted Talk", skillDesc = "Slurp starts giving a long ass Ted talk. This skill reduces the defence of the whole enemy team out of sheer boredom and also deals a small amount of true damage. But it has a chance of effecting your own team too!", hostile = true };
    public static Skill raid = new() { skillName = "Raid", skillDesc = "Slurp picks an enemy as the raid target! Until her next turn all attacks and abilities against the target deal double damage.", hostile = true };
    public static Skill finalForm = new() { skillName = "Final Form", skillDesc = "Slurp ascends into her final form! She can pick one of her three alter egos: Thoswis, Clownwis, or Tradwis. Thotwis charms a random enemy each turn, Tradwis heals her whole team each turn, Clownwis just doesn't fucking care and gives everyone random status effects each turn." , hostile = false };

    public static Skill longClaws = new() { skillName = "Long Claws", hostile = false };
    public static Skill swipe = new() { skillName = "Swipe", hostile = true };
    public static Skill justBeCute = new() { skillName = "Just Be Cute", hostile = false };
    public static Skill snuggle = new() { skillName = "Snuggle", hostile = false };
    public static Skill ultraInstinct = new() { skillName = "Ultra Instinct", hostile = false };

    public static Skill boomer = new() { skillName = "Boomer", hostile = false };
    public static Skill empGrenade = new() { skillName = "EMP Grenade", hostile = true };
    public static Skill silence = new() { skillName = "Silence", hostile = true };
    public static Skill banHammer = new() { skillName = "Ban Hammer", hostile = true };
    public static Skill findBigfoot = new() { skillName = "Find Bigfoot", hostile = false };

    public static Skill polePosition = new() { skillName = "Pole Position", hostile = false };
    public static Skill startYourEngines = new() { skillName = "Start Your Engines", hostile = false };
    public static Skill burnout = new() { skillName = "Burnout", hostile = false };
    public static Skill pitStop = new() { skillName = "Pit Stop", hostile = false };
    public static Skill dizzOrNoDizz = new() { skillName = "Dizz Or No Dizz", hostile = true };
    
    public static Skill washedUpBoyfriend = new() { skillName = "Washed Up Boyfriend", hostile = false };
    public static Skill rap = new() { skillName = "Rapper", hostile = true };
    public static Skill clownmaxxing = new() { skillName = "Clownmaxxing", hostile = true };
    public static Skill godComplex = new() { skillName = "God Complex", hostile = false };
    public static Skill extremeLaziness = new() { skillName = "Extreme Laziness", hostile = true };

    public static Skill ghouldMaxxing = new() { skillName = "GhoulMaxxing", hostile = false };
    public static Skill herbalMedicine = new() { skillName = "Herbal Medicine", hostile = false };
    public static Skill corpsePaint = new() { skillName = "Corpse Paint", hostile = false };
    public static Skill onePeace = new() { skillName = "One Peace", hostile = false };
    public static Skill oneViolence = new() { skillName = "One Violence", hostile = true };

    public static Skill suckLifeForce = new() { skillName = "Suck Life", hostile = true };

    public static Skill summonBot = new() { skillName = "Summon Bot", hostile = false };

    void Start()
    {
        imageLoader = FindAnyObjectByType<ImageLoader>();
        AssignSkillIcons();
    }

    void AssignSkillIcons()
    {
        sevenYears.skillIcon = imageLoader.sevenYearsIcon;
        dancingMaster.skillIcon = imageLoader.dancingMasterIcon;
        tedTalk.skillIcon = imageLoader.tedTalkIcon;
        raid.skillIcon = imageLoader.raidIcon;
        finalForm.skillIcon = imageLoader.finalFormIcon;

        longClaws.skillIcon = imageLoader.longClawsIcon;
        swipe.skillIcon = imageLoader.swipeIcon;
        justBeCute.skillIcon = imageLoader.justBeCuteIcon;
        snuggle.skillIcon = imageLoader.snuggleIcon;
        ultraInstinct.skillIcon = imageLoader.ultraInstinctIcon;
    }
}
