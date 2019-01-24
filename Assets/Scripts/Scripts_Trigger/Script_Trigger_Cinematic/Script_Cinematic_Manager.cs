using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CanEditMultipleObjects]
public enum CinematicType { Resumegame, MaincharacterLight}

public class Script_Cinematic_Manager : MonoBehaviour
{

    public CinematicType c_cinematic_type;

    public Light l_light_main_character;

    public void OnEnable()
    {
        switch (c_cinematic_type.ToString(""))
        {
            case "Resumegame":
            BackToMainCamera();
            break;

            case "MaincharacterLight":
                ActivateN12Light();
                break;
        }
    }

    public void BackToMainCamera()
    {
        Script_Game_Manager.Instance.SetTimeResume();
        Script_Game_Manager.Instance.LeaveACinematic();
    }

    public void ActivateN12Light()
    {
        l_light_main_character.enabled = true;
    }
}
