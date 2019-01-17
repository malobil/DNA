using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Cinematic_Manager : MonoBehaviour
{
    public enum CinematicType { Maincamera }

    public CinematicType c_cinematic_type;
    public Camera c_main_camera;
    public Camera c_cinematic_camera;

    public void Start()
    {
        switch (c_cinematic_type.ToString(""))
        {
            case "Maincamera":
            BackToMainCamera();
            break;
        }
    }

    public void BackToMainCamera()
    {
        Script_Game_Manager.Instance.SetTimeResume();
    }
}
