using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Script_UI_Manager : MonoBehaviour
{
    public static Script_UI_Manager Instance { get; private set; }

    #region Notes

    [Header ("Note")]
    public TextMeshProUGUI t_note_title;
    public TextMeshProUGUI t_note_description;
    public GameObject g_note;
    private bool b_note_to_show = false;

    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ShowNote (Note_Data c_note_to_show)
    {
        g_note.SetActive(true);
        b_note_to_show = true;
        t_note_title.text = c_note_to_show.s_note_title;
        t_note_description.text = c_note_to_show.s_note_description;
        
        if(Input.GetKeyDown("i") && b_note_to_show)
        {
            g_note.SetActive(false);
            b_note_to_show = false;
        }
    }
}
