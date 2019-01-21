using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Script_Tutorial_Localisation : MonoBehaviour
{
    public TextMeshProUGUI text_title;
    public TextMeshProUGUI text_description;
    public string s_tutorial_key;

    private string s_tuto_title;
    private string s_tuto_description;

    private void Start()
    {
        s_tuto_title = Script_CSV_Manager.Instance.GetTutorialTitle(s_tutorial_key);
        s_tuto_description = Script_CSV_Manager.Instance.GetTutorialDescription(s_tutorial_key);
        text_title.text = s_tuto_title;
        text_description.text = s_tuto_description;
    }
}
