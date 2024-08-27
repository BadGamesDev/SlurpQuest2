using UnityEngine;

public class SkillDatabase : MonoBehaviour //got forced me to put this shit in the editor to humble me for not learning all the global shit properly
{
    public ImageLoader imageLoader;

    public static Skill sevenYears = new() { skillName = "7 Fucking Years", skillDesc = "Slurp gets more resilient with time! She gainst 1 defence each turn. At turn 7 she stops gaining defence but unlocks her ultimate ability if she is at least level 4.",hostile = false };
    public static Skill dancingMaster = new() { skillName = "Dancing Master", skillDesc = "Slurp starts doing some wild dance moves. This ability deals double damage but has a chance to injure Slurp instead of hitting the enemy.", hostile = true };
    public static Skill tedTalk = new() { skillName = "Ted Talk", skillDesc = "Slurp starts giving a long ass Ted talk. This skill reduces the defence of the whole enemy team out of sheer boredom and also deals a small amount of true damage. But it has a chance of effecting your own team too!", hostile = true };
    public static Skill raid = new() { skillName = "Raid", skillDesc = "Slurp picks an enemy as the raid target! Until her next turn all attacks and abilities against the target deal double damage.", hostile = true };
    public static Skill finalForm = new() { skillName = "Final Form", skillDesc = "Slurp ascends into her final form! She can pick one of her three alter egos: Thoswis, Clownwis, or Tradwis. Thotwis charms a random enemy each turn, Tradwis heals her whole team each turn, Clownwis just doesn't fucking care and gives everyone random status effects each turn." , hostile = false };

    public static Skill longClaws = new() { skillName = "Long Claws", skillDesc = "Whenever honey attacks an enemy, she inflicts bleed thanks to her long claws. Bleed deals damage based on the targets max health and ignores defence so it is higly effective against tanky enemies.", hostile = true };
    public static Skill swipe = new() { skillName = "Swipe", skillDesc = "Honey swipes her claws dealing damage to every enemy at once. This skill does not inflict bleed despite the fact that it would make sense. Because the developer thought it would be over powered.", hostile = true };
    public static Skill justBeCute = new() { skillName = "Just Be Cute", skillDesc = "Honey starts acting all cute and shit. Making the enemies hesitate when attacking her. This gives her a big defensive boost.", hostile = false };
    public static Skill snuggle = new() { skillName = "Snuggle", skillDesc = "Honey snuggles with an ally! Healing them with the power of friendship.", hostile = false };
    public static Skill ultraInstinct = new() { skillName = "Ultra Instinct", skillDesc = "No more cute Honey! She activates ultra instinct mode, greatly boosting her speed and damage!", hostile = false };

    public static Skill boomer = new() { skillName = "Boomer", skillDesc = "Digi is a boomer, as a result he is resistant to every status effect. He simply doesn't give a shit about your zoomer ass stuns or bleeds.", hostile = false };
    public static Skill empGrenade = new() { skillName = "EMP Grenade", skillDesc = "Digi throws one of his home made EMP grenades at a target to stun them. This is not how EMP works but I wanted to give Digi a skill that is all techy and shit.", hostile = true };
    public static Skill silence = new() { skillName = "Silence", skillDesc = "Digi silences an enemy. Preventing them from using any of their skills. This skill can not work against bosses because holy shit I really don't want to deal with all those bugs.", hostile = true };
    public static Skill banHammer = new() { skillName = "Ban Hammer", skillDesc = "Digi tries to execute an enemy by permabanning them. The target needs to be already weakened or this skill doesn't work. The permaban threshold is 30% of their max health.", hostile = true };
    public static Skill findBigfoot = new() { skillName = "Find Bigfoot", skillDesc = "Digi goes on a long and arduous journey to find Bigfoot! This skill has a random wait time which can be quite long and you lose the battle if everyone else dies before Digi comes back. So be careful!", hostile = false };

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

        boomer.skillIcon = imageLoader.boomerIcon;
        empGrenade.skillIcon = imageLoader.empIcon;
        silence.skillIcon = imageLoader.silenceIcon;
        banHammer.skillIcon = imageLoader.banhammerIcon;
        findBigfoot.skillIcon = imageLoader.bigfootIcon;
    }
}
