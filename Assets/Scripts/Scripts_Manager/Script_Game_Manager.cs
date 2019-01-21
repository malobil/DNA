using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Script_Game_Manager : MonoBehaviour
{
    public static Script_Game_Manager Instance { get; private set; }


    public Tutorial tutorials_state;

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

    private void Update()
    {
        /*if(Input.GetKeyDown("p"))
        {
            GameOver();
        }*/
    }

    #region Pause
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
    #endregion

    #region Game State
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

    #endregion

    public void RestartScene()
    {
        SetTimeResume();
        Script_UI_Manager.Instance.UnshowUIGameOver();
        SceneManager.LoadScene("Scene_Main_Menu");
    }

    public void GameOver()
    {
        SetTimePause();
        Script_UI_Manager.Instance.ShowUIGameOver();
    }

    #region Tutorial

    public void LaunchInteractionTutorial()
    {
        SetTimePause();
        Script_UI_Manager.Instance.LaunchInteractionTutorial();
        tutorials_state.b_do_interaction_tuto = true;
    }

    public void LaunchThrowTutorial()
    {
        SetTimePause();
        Script_UI_Manager.Instance.LaunchThrowTutorial();
        tutorials_state.b_do_throw_tuto = true;
    }

    public void LaunchSpecialInteractionTutorial()
    {
        SetTimePause();
        Script_UI_Manager.Instance.LaunchSpecialInteractionTutorial();
        tutorials_state.b_do_special_interaction_tuto = true;
    }

    public Tutorial GetTutorialState()
    {
        return tutorials_state;
    }

    #endregion
}

[Serializable]
public class Tutorial
{
    public bool b_do_interaction_tuto;
    public bool b_do_throw_tuto;
    public bool b_do_special_interaction_tuto;
}
