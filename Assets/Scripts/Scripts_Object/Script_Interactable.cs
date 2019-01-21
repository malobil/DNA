using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

[RequireComponent(typeof(Outline))]
public class Script_Interactable : MonoBehaviour
{
    public enum InteractableType {readable, holdable, talkable, teleport}

    public InteractableType object_type;

    public bool b_can_interact = true;
    public Transform t_teleport_point;

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
        Debug.Log("read");   // do something
    }

    private void Talk()
    {
        Debug.Log("talk");   // do something
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
