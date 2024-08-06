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
    }
    public void UpdateHealthBar(int newHealthValue) //this is quite flexible though there is almost no case where I would need to manually enter a valuer, I'll let this stay for now.
    {
        healthBar.value = newHealthValue;
        healthText.text = newHealthValue + "/" + healthBar.maxValue;
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
            statusIcon.transform.localPosition = new Vector3(-80 + (statusIcons.Count * 30), -110, 0);
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
        }
        
        foreach (StatusEffect selfStatus in ownData.selfStatusEffects)
        {
            Image statusIcon = Instantiate(prefabLoader.statusIcon);
            statusIcon.transform.SetParent(transform);
            statusIcon.transform.localPosition = new Vector3(-80 + (statusIcons.Count * 30), -110, 0);
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
        }
    }
}
