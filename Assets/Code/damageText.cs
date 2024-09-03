using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class damageText : MonoBehaviour
{
    public float cooldown = 0;
    TMP_Text Text;
    
    private void Start()
    {

        Text = GetComponent<TMP_Text>();
        Text.text = "";
        cooldown += 1;
    }
    
    void Update()
    {
        if (Text.text != "")
        {
            cooldown -= Time.deltaTime;
        }

        if (cooldown <= 0)
        {
            Text.text = "";
            transform.SetAsFirstSibling();
        }

        if (Text.text == "")
        {
            cooldown = 1;
        }
    }
}
