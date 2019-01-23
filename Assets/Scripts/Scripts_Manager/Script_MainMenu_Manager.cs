using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_MainMenu_Manager : MonoBehaviour
{

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadAScene(string s_scene_to_load)
    {
        SceneManager.LoadScene(s_scene_to_load);
    }
}
