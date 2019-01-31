using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_MainMenu_Manager : MonoBehaviour
{
    private void Start()
    {
        if(Script_Game_Manager.Instance != null)
        {
            Script_Game_Manager.Instance.SetTimeResume();
            Destroy(Script_Game_Manager.Instance.gameObject);
        }

        if (Script_UI_Manager.Instance != null)
        {
            Destroy(Script_UI_Manager.Instance.gameObject);
        }

        if (Script_Player.Instance != null)
        {
            Destroy(Script_Player.Instance.gameObject);
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadAScene(string s_scene_to_load)
    {
        SceneManager.LoadScene(s_scene_to_load);
    }
}
