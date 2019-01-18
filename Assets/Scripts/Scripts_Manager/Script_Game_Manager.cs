using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Game_Manager : MonoBehaviour
{
    public static Script_Game_Manager Instance { get; private set; }

    private bool b_game_is_pause = false;

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

    public void TogglePause()
    {
        if(GetGameState())
        {
            SetTimeResume();
            Script_UI_Manager.Instance.HideAllMenu();
        }
        else
        {
            SetTimePause();
            Script_UI_Manager.Instance.ShowPauseMenu();
        }
    }

    public void SetTimePause()
    {
        Time.timeScale = 0.0001f;
        b_game_is_pause = true;
    }

    public void SetTimeResume()
    {
        Time.timeScale = 1f;
        b_game_is_pause = false;
    }

    public bool GetGameState()
    {
        return b_game_is_pause;
    }
}
