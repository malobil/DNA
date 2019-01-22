using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;
using UnityEditor;
using TMPro;

public enum InteractableType { readable, holdable, talkable, teleport }


[RequireComponent(typeof(Outline))]
public class Script_Interactable : MonoBehaviour
{
  

    public InteractableType object_type;

    public bool b_can_interact = true;
    public Transform t_teleport_point;

    [Header("Dialogue")]

    public GameObject obj_dialog_box;
    public TextMeshProUGUI txt_dialog;
    public List<string> s_dialog_key;
    private int i_current_dialog_key = 0;

    //private bool b_already_talk;

    [Header("Note")]
    public string note_key;

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
            }
        }
    }

    private void Read()
    {
        Script_UI_Manager.Instance.ShowNote(note_key);
    }

    public void Talk()
    {
        if(i_current_dialog_key < s_dialog_key.Count)
        {
            txt_dialog.text = Script_CSV_Manager.Instance.GetDialogDescription(s_dialog_key[i_current_dialog_key]);
            Script_Game_Manager.Instance.SetTimePause();
            obj_dialog_box.SetActive(true);
            Script_Player.Instance.EnterADialog(this);
            i_current_dialog_key++;
        }
        else
        {
            Script_Game_Manager.Instance.SetTimeResume();
            obj_dialog_box.SetActive(false);
            Script_Player.Instance.LeaveADialog();
            i_current_dialog_key = 0;
            return;
        }
        
    }

    private void Teleport()
    {
        Debug.Log("teleport");   // do something
        Script_Player.Instance.Teleport(t_teleport_point.position);
    }

    private void Hold(Script_Player player)
    {
        //Debug.Log("hold");
        player.Hold();
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
