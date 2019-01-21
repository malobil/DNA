using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Script_Note_Localisation: MonoBehaviour
{
    public TextMeshProUGUI text_title;
    public TextMeshProUGUI text_description;
    public string s_note_key;

    private string s_note_title;
    private string s_note_description;

    private void Start()
    {
        s_note_title = Script_CSV_Manager.Instance.GetNoteDescription(s_note_key);
        s_note_description = Script_CSV_Manager.Instance.GetNoteDescription(s_note_key);
        text_title.text = s_note_title;
        text_description.text = s_note_description ;
    }
}
