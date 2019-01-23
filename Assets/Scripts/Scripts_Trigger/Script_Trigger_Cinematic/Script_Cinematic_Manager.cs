using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CanEditMultipleObjects]
public enum CinematicType { Resumegame, LightClip }

public class Script_Cinematic_Manager : MonoBehaviour
{

    public CinematicType c_cinematic_type;

    public void OnEnable()
    {
        switch (c_cinematic_type.ToString(""))
        {
            case "Resumegame":
            BackToMainCamera();
            break;

            case "LightClip":
                
                break;
        }
    }

    public void BackToMainCamera()
    {
        Script_Game_Manager.Instance.SetTimeResume();
        Script_Game_Manager.Instance.LeaveACinematic();
    }

    public void ChangeIntensityLight()
    {

    }
}
