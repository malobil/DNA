using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_UI_Manager : MonoBehaviour
{
    public static Script_UI_Manager Instance { get; private set; }

    #region Notes

    [Header ("Note")]
    public Text t_note_title;
    public Text t_note_description;

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
        t_note_title.text = c_note_to_show.s_note_title;
        t_note_description.text = c_note_to_show.s_note_description;
    }
}
