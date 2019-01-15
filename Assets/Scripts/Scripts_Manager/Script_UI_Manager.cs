using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Script_UI_Manager : MonoBehaviour
{
    public static Script_UI_Manager Instance { get; private set; }

    private bool b_is_in_menu = false;

    #region Notes

    [Header ("Note")]
    public TextMeshProUGUI t_note_title;
    public TextMeshProUGUI t_note_description;
    public GameObject g_note;

    #endregion

    public Image hold_object;

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

    public void UnShowNote()
    {
        g_note.SetActive(false);
        b_is_in_menu = false;
    }

    public void NewObjectHold(Sprite new_object)
    {
        hold_object.sprite = new_object;
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0001f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public bool IsInMenu()
    {
        return b_is_in_menu;
    }
}
