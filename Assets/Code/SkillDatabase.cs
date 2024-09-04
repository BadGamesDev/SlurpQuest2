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

    public static Skill polePosition = new() { skillName = "Pole Position", skillDesc = "As a famous racer, Jaydizz always makes the first move in combat!", hostile = false };
    public static Skill startYourEngines = new() { skillName = "Start Your Engines", skillDesc = "Jaydizz starts his engines to double his already high speed! His enemies won't even be able to see what hit them!", hostile = false };
    public static Skill burnout = new() { skillName = "Burnout", skillDesc = "Jaydizz can use the smoke from his tires to hide himself, giving a massive boost to his dodge rating!", hostile = false };
    public static Skill pitStop = new() { skillName = "Pit Stop", skillDesc = "Jaydizz can use pitstop to completely heal himself! This is a very powerful ability but has a long cooldown.", hostile = false };
    public static Skill dizzOrNoDizz = new() { skillName = "Dizz Or No Dizz", skillDesc = "It is dizz or no dizz everybody! Jaydizz presents 3 boxes and you click on them. The last one you click will be your reward. Who knows what it contains?", hostile = true };
    
    public static Skill washedUpBoyfriend = new() { skillName = "Washed Up Boyfriend", skillDesc = "Cndk is the winner of the 2023 Slurpwis boyfriend contest. Unfortunately this has absolutely no effect.", hostile = false };
    public static Skill rap = new() { skillName = "Rapper", skillDesc = "Cndk99 aka Yano is a famous rapper, he can start rapping in combat to reduce the defence of everyone to 0 including your own team. Because everyone wants this shit to be over as soon as possible.", hostile = true };
    public static Skill clownmaxxing = new() { skillName = "Clownmaxxing", skillDesc = "Cndk tries too hard to be funny, as a result he deals cringe damage to everyone in combat including your own team.", hostile = true };
    public static Skill godComplex = new() { skillName = "God Complex", skillDesc = "Cndk has an extremely inflated ego. He can start talking about how good looking, smart, and funny he is which will have no effect on combat. This is a completely useless skill.", hostile = false };
    public static Skill extremeLaziness = new() { skillName = "Extreme Laziness", skillDesc = "Cndk started losing his mind while trying to complete this fucking piece of shit game, so this skill just makes him do a normal attack because he couldn't be bothered adding another cool ultimate ability that would fuck up his codebase even more. HOLY SHIT I REALLY NEED THIS WHOLE THING TO BE OVER SOON!", hostile = true };

    public static Skill ghouldMaxxing = new() { skillName = "GhoulMaxxing", skillDesc = "OneViolence has mastered the art of ghoulmaxxing. He gains nourishment from the corpses of the fallen. Whenever someone dies, OneViolence will get fully healed.", hostile = false };
    public static Skill herbalMedicine = new() { skillName = "Herbal Medicine", skillDesc = "The dark arts are not the only thing OneViolence excells at. He can also use herbal medicine to heal an all or himself.", hostile = false };
    public static Skill corpsePaint = new() { skillName = "Corpse Paint", skillDesc = "OneViolence can apply corpse paint on himself or an ally, increasing their attack and speed for the rest of combat by a small amount. It also looks sick as fuck.", hostile = false };
    public static Skill onePeace = new() { skillName = "One Peace", skillDesc = "Violence is cool! But sometimes peace can be what you need. OneViolence can declare peace, making everyone unable to attack or use any harmful ability for a short while.", hostile = false };
    public static Skill oneViolence = new() { skillName = "One Violence", skillDesc = "ONCE PER COMBAT ONEVIOLENCE PICKS ONE ENEMY AND DEALS ONE OR ONE ONE OR ONE ONE ONE OR ONE ONE ONE ONE DAMAGE!", hostile = true };

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

        polePosition.skillIcon = imageLoader.positionIcon;
        startYourEngines.skillIcon = imageLoader.startenginesIcon;
        burnout.skillIcon = imageLoader.burnoutSkillIcon;
        pitStop.skillIcon = imageLoader.pitstopIcon;
        dizzOrNoDizz.skillIcon = imageLoader.dizzIcon;

        washedUpBoyfriend.skillIcon = imageLoader.boyfriendIcon;
        rap.skillIcon = imageLoader.rapIcon;
        clownmaxxing.skillIcon = imageLoader.clownmaxxIcon;
        godComplex.skillIcon = imageLoader.godcomplexIcon;
        extremeLaziness.skillIcon = imageLoader.lazyIcon;

        ghouldMaxxing.skillIcon = imageLoader.ghoulIcon;
        herbalMedicine.skillIcon = imageLoader.herbalIcon;
        corpsePaint.skillIcon = imageLoader.corpseIcon;
        onePeace.skillIcon = imageLoader.peaceIcon;
        oneViolence.skillIcon = imageLoader.violenceIcon;
    }
}
