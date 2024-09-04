using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterUI : MonoBehaviour
{
    public CharacterData ownData;
    public PrefabLoader prefabLoader;
    public ImageLoader imageLoader;
    
    public Slider healthBar;
    public Slider cooldownBar;

    public TMP_Text healthText;

    public List<Image> statusIcons;

    private void Start()
    {
        prefabLoader = FindAnyObjectByType<PrefabLoader>();
        imageLoader = FindAnyObjectByType<ImageLoader>();
        cooldownBar.maxValue = 3000;
        if(ownData.characterName == "Feral Cat")
        {
            cooldownBar.maxValue = 6000;
        }

        UpdateHealthBar(ownData.health);
    }
    public void UpdateHealthBar(int newHealthValue) //this is quite flexible though there is almost no case where I would need to manually enter a valuer, I'll let this stay for now.
    {
        if (ownData.invulnerable)
        {
            healthText.text = "???????";
        }

        else
        {
            healthBar.value = newHealthValue;
            healthText.text = newHealthValue + "/" + ownData.maxHealth;
        }
    }

    public void UpdateCooldownBar(int newCooldownValue)
    {
        cooldownBar.value = newCooldownValue;   
    }

    public void UpdateStatusIcons()
    {
        foreach (Image icon in statusIcons)
        {
            Destroy(icon.gameObject);
        }

        statusIcons.Clear();

        foreach (StatusEffect globalStatus in ownData.globalStatusEffects) //what the fuck is a concat????
        {
            Image statusIcon = Instantiate(prefabLoader.statusIcon);
            statusIcon.transform.SetParent(transform);
            statusIcon.transform.localPosition = new Vector3(-80 + (statusIcons.Count * 30), -175, 0);
            statusIcons.Add(statusIcon);

            if (globalStatus.statusName == "stun") //There is no need to check this separately for each foreach (hehe) loop, fix it if you have time
            {
                statusIcon.sprite = imageLoader.stunIcon;
            }
            else if (globalStatus.statusName == "bleed")
            {
                statusIcon.sprite = imageLoader.bleedIcon;
            }
            else if (globalStatus.statusName == "ted audience")
            {
                statusIcon.sprite = imageLoader.tedAudienceIcon;
            }
            else if (globalStatus.statusName == "raid target")
            {
                statusIcon.sprite = imageLoader.raidTargetIcon;
            }

            else if (globalStatus.statusName == "corpse paint")
            {
                statusIcon.sprite = imageLoader.corpseIcon;
            }

            else if (globalStatus.statusName == "engine started")
            {
                statusIcon.sprite = imageLoader.startenginesIcon;
            }

            else if (globalStatus.statusName == "burnout smoke")
            {
                statusIcon.sprite = imageLoader.burnoutIcon;
            }

            else if (globalStatus.statusName == "cute")
            {
                statusIcon.sprite = imageLoader.justBeCuteIcon;
            }

            else if (globalStatus.statusName == "ultra instinct")
            {
                statusIcon.sprite = imageLoader.ultraInstinctIcon;
            }

            else if (globalStatus.statusName == "silence")
            {
                statusIcon.sprite = imageLoader.silenceIcon;
            }

            else if (globalStatus.statusName == "rapped")
            {
                statusIcon.sprite = imageLoader.rapIcon;
            }
        }
        
        foreach (StatusEffect selfStatus in ownData.selfStatusEffects)
        {
            Image statusIcon = Instantiate(prefabLoader.statusIcon);
            statusIcon.transform.SetParent(transform);
            statusIcon.transform.localPosition = new Vector3(-80 + (statusIcons.Count * 30), -175, 0);
            statusIcons.Add(statusIcon);
            
            if (selfStatus.statusName == "stun") 
            {
                statusIcon.sprite = imageLoader.stunIcon;
            }
            else if (selfStatus.statusName == "bleed")
            {
                statusIcon.sprite = imageLoader.bleedIcon;
            }
            else if (selfStatus.statusName == "ted audience")
            {
                statusIcon.sprite = imageLoader.tedAudienceIcon;
            }
            else if (selfStatus.statusName == "raid target")
            {
                statusIcon.sprite = imageLoader.raidTargetIcon;
            }

            else if (selfStatus.statusName == "one peace")
            {
                statusIcon.sprite = imageLoader.peaceIcon;
            }

            else if (selfStatus.statusName == "thottery")
            {
                statusIcon.sprite = imageLoader.thotwisIcon;
            }

            else if (selfStatus.statusName == "return to trad")
            {
                statusIcon.sprite = imageLoader.tradwisIcon;
            }

            else if (selfStatus.statusName == "permacloud")
            {
                statusIcon.sprite = imageLoader.permaCloudIcon;
            }

            else if (selfStatus.statusName == "simp")
            {
                statusIcon.sprite = imageLoader.simpIcon;
            }
        }
    }
}
