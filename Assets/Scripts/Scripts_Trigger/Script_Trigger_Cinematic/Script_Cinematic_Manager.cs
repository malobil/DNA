using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Cinematic_Manager : MonoBehaviour
{
    public enum CinematicType { Resumegame }

    public CinematicType c_cinematic_type;

    public void OnEnable()
    {
        switch (c_cinematic_type.ToString(""))
        {
            case "Resumegame":
            BackToMainCamera();
            break;
        }
    }

    public void BackToMainCamera()
    {
        Script_Game_Manager.Instance.SetTimeResume();
    }
}
