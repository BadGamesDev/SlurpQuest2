using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CinematicManager : MonoBehaviour
{
    public GameState gameState;
    public OverworldUI overworldUI;
    public GameObject cinematicScreen;
    public GameObject cinematicImage;
    public TMP_Text cinematicText;

    public float cooldown;
    public int wallOfTextNumber;

    //void Start()
    //{
    //    gameState.globalPaused = true;
    //    cinematicScreen.SetActive(true);

    //    cinematicText.text = "Hey Slurp! Please pretend this is a really cool cutscene instead of some shitty walls of text. " +
    //                         "Anyways here is the plot: After the events of the first SlurpQuest™ Slurp starts living happily ever after with Hank J Wimbleton from the hit series madness combat. " +
    //                         "But unfortunately we needed a sequel...";
    //    cooldown = 14;
    //    wallOfTextNumber += 1;
    //}

    //private void Update()
    //{
    //    if (cooldown > 0)
    //    {
    //        cooldown -= Time.deltaTime;
    //    }

    //    if (wallOfTextNumber == 1 && cooldown <= 0)
    //    {
    //        cinematicText.text = "So uhh... after the first game you get attacked by a... you get attacked by the Lord of Decay! Yeah that's a cool fucking name! " +
    //                "You can not remember what happened after that, but when you woke up Hank was gone. Then your twitch followers decided to avenge you and attacked the Lord of Decay... ";
    //        cooldown = 12;
    //        wallOfTextNumber += 1;
    //    }

    //    if (wallOfTextNumber == 2 && cooldown <= 0)
    //    {
    //        cinematicText.text = "But his power was too much to handle! Despite figting bravely they have lost. As punishment the dark lord corrupted them all using his dark magic! Now they all serve him in his realm. After that, the dark lord took over all of twitch!";
    //        cooldown = 12;
    //        wallOfTextNumber += 1;
    //    }

    //    if (wallOfTextNumber == 3 && cooldown <= 0)
    //    {
    //        cinematicText.text = "You got beaten, Hank is gone, your followers are gone, and the dark lord has completely taken over twitch. It has never been so fucking over...";
    //        cooldown = 9;
    //        wallOfTextNumber += 1;
    //    }

    //    if (wallOfTextNumber == 4 && cooldown <= 0)
    //    {
    //        cinematicText.text = "But! As a wise man once said:                                                                      I get knocked down, but I get up again\r\nYou are never gonna keep me down\r\nI get knocked down, but I get up again\r\nYou are never gonna keep me down\r\nI get knocked down, but I get up again\r\nYou are never gonna keep me down\r\nI get knocked down, but I get up again\r\nYou are never gonna keep me down";
    //        cooldown = 8;
    //        wallOfTextNumber += 1;
    //    }

    //    if (wallOfTextNumber == 5 && cooldown <= 0)
    //    {
    //        gameState.globalPaused = false;
    //        cinematicText.text = null;
    //        cinematicScreen.SetActive(false);

    //        overworldUI.AddMessage("You start your journey in the nostalgia forest! A peaceful place from a simpler time. But be careful! Who knows what this place holds after so many years?");
    //        wallOfTextNumber += 1;
    //    }
    //}
}
