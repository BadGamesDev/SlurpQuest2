using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CinematicManager : MonoBehaviour
{
    public ImageLoader imageLoader;
    public GameState gameState;
    public OverworldUI overworldUI;
    public GameObject cinematicScreen;
    public Image cinematicImage;
    public TMP_Text cinematicText;

    public float cooldown;
    public int wallOfTextNumber;

    public bool introCutscene;
    public bool asmonCutscene;
    public bool auditorCutscene;

    private void Start()
    {
        introCutscene = true;
    }

    private void Update()
    {
        //StartingScene();
        AsmonScene();
        AuditorScene();
    }

    public void StartingScene()
    {
        if (introCutscene)
        {
            gameState.globalPaused = true;
            cinematicScreen.SetActive(true);

            cooldown -= Time.deltaTime;

            if (wallOfTextNumber == 0 && cooldown <= 0)
            {
                cinematicText.text = "Hey Slurp! Please pretend this is a really cool cutscene instead of some shitty walls of text. " +
                         "Anyways here is the plot: After the events of the first SlurpQuest™ Slurp starts living happily ever after with Hank J Wimbleton from the hit series madness combat. " +
                         "But unfortunately we needed a sequel...";
                cooldown = 14;
                wallOfTextNumber += 1;
            }

            if (wallOfTextNumber == 1 && cooldown <= 0)
            {
                cinematicText.text = "So uhh... after the first game you get attacked by a... you get attacked by the Lord of Decay! Yeah that's a cool fucking name! " +
                        "You can not remember what happened after that, but when you woke up Hank was gone. Then your twitch followers decided to avenge you and attacked the Lord of Decay... ";
                cooldown = 12;
                wallOfTextNumber += 1;
            }

            if (wallOfTextNumber == 2 && cooldown <= 0)
            {
                cinematicText.text = "But his power was too much to handle! Despite figting bravely they have lost. As punishment the dark lord corrupted them all using his dark magic! Now they all serve him in his realm. After that, the dark lord took over all of twitch!";
                cooldown = 12;
                wallOfTextNumber += 1;
            }

            if (wallOfTextNumber == 3 && cooldown <= 0)
            {
                cinematicText.text = "You got beaten, Hank is gone, your followers are gone, and the dark lord has completely taken over twitch. It has never been so fucking over...";
                cooldown = 9;
                wallOfTextNumber += 1;
            }

            if (wallOfTextNumber == 4 && cooldown <= 0)
            {
                cinematicText.text = "But! As a wise man once said:                                                                      I get knocked down, but I get up again\r\nYou are never gonna keep me down\r\nI get knocked down, but I get up again\r\nYou are never gonna keep me down\r\nI get knocked down, but I get up again\r\nYou are never gonna keep me down\r\nI get knocked down, but I get up again\r\nYou are never gonna keep me down";
                cooldown = 8;
                wallOfTextNumber += 1;
            }

            if (wallOfTextNumber == 5 && cooldown <= 0)
            {
                gameState.globalPaused = false;
                cinematicText.text = null;
                cinematicScreen.SetActive(false);

                overworldUI.AddMessage("You start your journey in the nostalgia forest! A peaceful place from a simpler time. But be careful! Who knows what this place holds after so many years?");
                wallOfTextNumber = 0;
                introCutscene = false;
            }
        }
    }

    public void AsmonScene()
    {
        if (asmonCutscene)
        {
            gameState.globalPaused = true;
            cinematicScreen.SetActive(true);

            cooldown -= Time.deltaTime;

            if (wallOfTextNumber == 0 && cooldown <= 0)
            {
                cooldown = 8;
                wallOfTextNumber += 1;
            }

            if (wallOfTextNumber == 1 && cooldown <= 0)
            {
                cinematicImage.sprite = imageLoader.AsmonScene;
                cinematicText.text = "Slurp... Is it... over?";
                cooldown = 8;
                wallOfTextNumber += 1;
            }

            if (wallOfTextNumber == 2 && cooldown <= 0)
            {
                cinematicImage.sprite = imageLoader.AsmonScene;
                cinematicText.text = "I see only darkness before me...";
                cooldown = 8;
                wallOfTextNumber += 1;
            }

            if (wallOfTextNumber == 3 && cooldown <= 0)
            {
                cinematicText.text = "I... do not expect you to forgive me... not after all I have done.";
                cooldown = 8;
                wallOfTextNumber += 1;
            }

            if (wallOfTextNumber == 4 && cooldown <= 0)
            {

                cinematicText.text = "All those streamers I have bullied...";
                cooldown = 8;
                wallOfTextNumber += 1;
            }

            if (wallOfTextNumber == 5 && cooldown <= 0)
            {

                cinematicText.text = "All those dreams I have destroyed...";
                cooldown = 8;
                wallOfTextNumber += 1;
            }

            if (wallOfTextNumber == 6 && cooldown <= 0)
            {
                cinematicText.text = "But you need to know... I was not the one...";
                cooldown = 8;
                wallOfTextNumber += 1;
            }

            if (wallOfTextNumber == 7 && cooldown <= 0)
            {
                cinematicText.text = "...who took Hank, and all your friends.";
                cooldown = 8;
                wallOfTextNumber += 1;
            }

            if (wallOfTextNumber == 8 && cooldown <= 0)
            {
                cinematicText.text = "You must go on Slurp... you must stop him...";
                cooldown = 8;
                wallOfTextNumber += 1;
            }

            if (wallOfTextNumber == 9 && cooldown <= 0)
            {
                cinematicText.text = "You... are the only one who can!";
                cooldown = 8;
                wallOfTextNumber += 1;
            }

            if (wallOfTextNumber == 10 && cooldown <= 0)
            {
                cinematicText.text = "And now I go... To touch grass...";
                cooldown = 8;
                wallOfTextNumber += 1;
            }

            if (wallOfTextNumber == 11 && cooldown <= 0)
            {
                asmonCutscene = false;
                gameState.globalPaused = false;
                cinematicScreen.SetActive(false);
                wallOfTextNumber = 0;
            }
        }
    }

    public void AuditorScene()
    {
        if (auditorCutscene)
        {
            gameState.globalPaused = true;
            cinematicScreen.SetActive(true);

            cooldown -= Time.deltaTime;

            if (wallOfTextNumber == 0 && cooldown <= 0)
            {
                cinematicImage.sprite = imageLoader.AuditorScene1;
                cinematicText.text = null;
                cooldown = 3;
                wallOfTextNumber += 1;
            }

            if (wallOfTextNumber == 1 && cooldown <= 0)
            {
                cinematicImage.sprite = imageLoader.AuditorScene2;
                cooldown = 2;
                wallOfTextNumber += 1;
            }

            if (wallOfTextNumber == 2 && cooldown <= 0)
            {
                cinematicImage.sprite = imageLoader.AuditorScene3;
                cinematicText.text =  "Now it's up to you Hank...";
                cooldown = 6;
                wallOfTextNumber += 1;
            }

            if (wallOfTextNumber == 3 && cooldown <= 0)
            {
                auditorCutscene = false;
                gameState.globalPaused = false;
                cinematicScreen.SetActive(false);
                wallOfTextNumber = 0;
            }
        }
    }
}
