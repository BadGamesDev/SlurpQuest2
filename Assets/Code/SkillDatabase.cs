using UnityEngine;

public class SkillDatabase : MonoBehaviour //got forced me to put this shit in the editor to humble me for not learning all the global shit properly
{
    public ImageLoader imageLoader;

    public static Skill sevenYears = new() { skillName = "7 Fucking Years", hostile = false };
    public static Skill dancingMaster = new() { skillName = "Dancing Master", hostile = true };
    public static Skill tedTalk = new() { skillName = "Ted Talk", hostile = true };
    public static Skill raid = new() { skillName = "Raid", hostile = true };
    public static Skill finalForm = new() { skillName = "Final Form", hostile = false };

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
    public static Skill rap = new() { skillName = "Rap", hostile = true };

    public static Skill herbalMedicine = new() { skillName = "Herbal Medicine", hostile = true };
    public static Skill ghouldMaxxing = new() { skillName = "GhoulMaxxing", hostile = false };

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
