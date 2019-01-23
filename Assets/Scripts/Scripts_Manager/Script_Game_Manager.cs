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
    private bool b_game_is_over = false;

    private Vector2 v_last_checkpoint;

    private int i_last_stair_idx = -1;

    public Vector2 GetLastCheckpointPosition()
    {
        return v_last_checkpoint;
    }

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
        if(Input.GetKeyDown("p"))
        {
            GameOver();
        }
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

    public void GameOver()
    {
        SetTimePause();
        Script_UI_Manager.Instance.ShowUIGameOver();
        b_game_is_over = true;
    }

    public bool GetGameOver()
    {
        return b_game_is_over;
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

    #region Save & Load

    public void Save(float f_player_x_position , float f_player_y_position , int i_dna_level)
    {
        PlayerPrefs.SetFloat("Player_x_position", f_player_x_position);
        PlayerPrefs.SetFloat("Player_y_position", f_player_y_position);
        PlayerPrefs.SetInt("Player_level", i_dna_level);
        PlayerPrefs.SetInt("Player_floor", SceneManager.GetActiveScene().buildIndex);

        Debug.Log("SAVE");
    }

    public void Load()
    {
        float f_player_x_position = PlayerPrefs.GetFloat("Player_x_position");
        float f_player_y_position = PlayerPrefs.GetFloat("Player_y_position");
        int i_player_level = PlayerPrefs.GetInt("Player_level");
        int i_player_floor = PlayerPrefs.GetInt("Player_floor");

        Script_Player.Instance.LoadData(f_player_x_position, f_player_y_position, i_player_level);

        Debug.Log("LOAD");
    }

    #endregion

    #region Change Level

    public GameObject[] t_stair_list;

    public void ChangeGroundLevel(int i_stair_number, string s_scene_to_load)
    {
        i_last_stair_idx = i_stair_number;
        LoadAScene(s_scene_to_load);
    }

    public void RegisterStair()
    {
        t_stair_list = GameObject.FindGameObjectsWithTag("Stair");
    }

    public void SendPlayerToEntryPoint()
    {
        foreach(GameObject stairs in t_stair_list)
        {
            if (stairs.GetComponent<Script_Trigger_Stairs>().GetStairIndex() == i_last_stair_idx)
            {
                Script_Player.Instance.transform.position = stairs.GetComponent<Script_Trigger_Stairs>().GetApparitionPoint().position;
            }
        }
    }
    #endregion

    public void LoadAScene(string s_scene_to_load)
    {
        SceneManager.LoadScene(s_scene_to_load);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void Retry()
    {
        SetTimeResume();
        Script_UI_Manager.Instance.UnshowUIGameOver();
        Load();
    }

    // called when a scene is load
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(i_last_stair_idx > -1)
        {
            RegisterStair();
            SendPlayerToEntryPoint();
        }
    }
}

[Serializable]
public class Tutorial
{
    public bool b_do_interaction_tuto;
    public bool b_do_throw_tuto;
    public bool b_do_special_interaction_tuto;
}


