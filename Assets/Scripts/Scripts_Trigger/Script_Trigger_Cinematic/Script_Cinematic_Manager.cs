using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(Script_Cinematic_Manager))]
public class Script_Cinematic_Manager_Editor : Editor
{
    Script_Cinematic_Manager s_Script;
    private void OnEnable()
    {
        s_Script = (Script_Cinematic_Manager)target;
    }

    public override void OnInspectorGUI()
    {
        Undo.RecordObject(target, "Script_Cinematic_Manager");
        s_Script.c_cinematic_type = (CinematicType)EditorGUILayout.EnumPopup("Cinematic Type", s_Script.c_cinematic_type);

        switch (s_Script.c_cinematic_type)
        {
            case CinematicType.Resumegame:
                s_Script.Yes = EditorGUILayout.IntField("Yes", s_Script.Yes);
                break;
            case CinematicType.AAA:
                s_Script.AYAYA = (GameObject)EditorGUILayout.ObjectField("AYAYA", s_Script.AYAYA, typeof(GameObject), true);
                break;
            default:
                break;
        }
    }
}

public enum CinematicType { Resumegame,AAA }

public class Script_Cinematic_Manager : MonoBehaviour
{

    public CinematicType c_cinematic_type;
    public int Yes;
    public GameObject AYAYA;

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
