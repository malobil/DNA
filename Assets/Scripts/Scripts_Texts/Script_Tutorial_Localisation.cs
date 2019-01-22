using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Script_Tutorial_Localisation : MonoBehaviour
{
    public TextMeshProUGUI text_title;
    public TextMeshProUGUI text_description;

    public string s_title_key;
    public string s_description_key ;

    private void Start()
    {
        text_title.text = Script_Localization_Manager.Instance.GetLocalisedText(s_title_key);
        text_description.text = Script_Localization_Manager.Instance.GetLocalisedText(s_description_key);
    }
}
