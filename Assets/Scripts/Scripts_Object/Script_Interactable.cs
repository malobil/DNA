using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;
using UnityEditor;
using TMPro;
using UnityEngine.Timeline;


//[CanEditMultipleObjects]
#region EditorGUILayout
/*[CustomEditor(typeof(Script_Interactable))]
public class Script_Interactable_Editor : Editor
{
    Script_Interactable s_script_interactable;

    private void OnEnable()
    {
        s_script_interactable = (Script_Interactable)target;
    }

    public override void OnInspectorGUI()
    {
        Undo.RecordObject(target, "Script_Interactable");
        s_script_interactable.object_type = (InteractableType)EditorGUILayout.EnumPopup("Interactable Type", s_script_interactable.object_type);

        switch (s_script_interactable.object_type)
        {
            case InteractableType.readable:
                s_script_interactable.note_key = EditorGUILayout.TextField("Note Key", s_script_interactable.note_key);
                break;
            case InteractableType.holdable:
                
                break;
            case InteractableType.talkable:
                s_script_interactable.obj_dialog_box = (GameObject)EditorGUILayout.ObjectField("Dialogue Interface", s_script_interactable.obj_dialog_box, typeof(GameObject), true);
                s_script_interactable.txt_dialog = (TextMeshProUGUI)EditorGUILayout.ObjectField("Dialogue Text", s_script_interactable.txt_dialog,typeof(TextMeshProUGUI),true);
                //s_script_interactable.s_dialog_key = EditorGUILayout.("Dialogue Key", s_script_interactable.s_dialog_key);
                break;
            case InteractableType.teleport:
                
                break;
            default:
                break;
        }
    }
}*/

#endregion

public enum InteractableType { readable, holdable, talkable, teleport, card, launchCinematic, special, button }
public enum CardType { blue }

[RequireComponent(typeof(Outline))]
public class Script_Interactable : MonoBehaviour
{
    public InteractableType object_type;

    [Header("Note")]
    public string note_key_title;
    public string note_key;

    public bool b_can_interact = true;

    [Header("Teleport")]
    public Transform t_teleport_point;
    public GameObject obj_discover_land;

    [Header("Dialogue")]

    public GameObject obj_dialog_box;
    public TextMeshProUGUI txt_dialog;
    public List<string> s_dialog_key;
    private int i_current_dialog_key = 0;

    [Header("Card")]
    public CardType card;

    [Header("Cinematic")]
    public TimelineAsset cinematic_to_play;


    [Header("Spawner")]
    public GameObject obj_to_spawn;
    public Transform t_spawn_point;

    public void Interact(Script_Player player)
    {
        if(b_can_interact)
        {
            switch (object_type.ToString(""))
            {
                case "readable":
                    Read();
                    break;

                case "holdable":
                    Hold(player);
                    break;

                case "talkable":
                    Talk();
                    break;

                case "teleport":
                    Teleport();
                    break;

                case "card":
                    AddCard();
                    break;

                case "launchCinematic":
                    Script_Cinematic_Controller.Instance.PlayCinematic(cinematic_to_play);
                    break;

                case "button":
                    ActiveButton();
                    break;

                case "special":
                    GetComponent<Script_IObject>().Use(null);
                    break;
            }
        }
    }

    private void Read()
    {
        Script_UI_Manager.Instance.ShowNote(note_key_title,note_key);
    }

    private void ActiveButton()
    {
        GetComponent<Script_Button>().ChangeStateDoor();
    }

    public void Talk()
    {
        if(i_current_dialog_key == 0)
        {
            Script_Game_Manager.Instance.SetTimePause();
            Script_Player.Instance.EnterADialog(this);
            obj_dialog_box.SetActive(true);
        }

        if (i_current_dialog_key < s_dialog_key.Count)
        {
            NextDialog();
        }
        else
        {
            StopDialog();
        }  
    }

    private void AddCard()
    {
        Script_Player.Instance.ObtainACard(card.ToString(""));
        Destroy(gameObject);
    }

    private void NextDialog()
    {
        txt_dialog.text = Script_Localization_Manager.Instance.GetLocalisedText(s_dialog_key[i_current_dialog_key]);
        i_current_dialog_key++;
    }

    private void StopDialog()
    {
        Script_Game_Manager.Instance.SetTimeResume();
        Script_Player.Instance.LeaveADialog();
        obj_dialog_box.SetActive(false);
        i_current_dialog_key = 0;
    }

    private void Teleport()
    {
        Script_Player.Instance.Teleport(t_teleport_point.position);

        if(obj_discover_land != null && obj_discover_land.activeSelf)
        {
            obj_discover_land.SetActive(false);
        }
    }

    private void Hold(Script_Player player)
    {
        Script_Player.Instance.Hold(gameObject);
    }

    public void AllowInteraction()
    {
        b_can_interact = true;
    }

    public bool GetInteractPermission()
    {
        return b_can_interact;
    }
}
