using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

[RequireComponent(typeof(Outline))]
public class Script_Interactable : MonoBehaviour
{
    public enum InteractableType {readable, hideable, holdable, talkable}

    public InteractableType object_type;

    public void Interact(Script_Player player)
    {
        switch(object_type.ToString(""))
        {
            case "readable":
                Read();
                break;

            case "hideable":
                Hide();
                break;

            case "holdable":
                Hold(player);
                break;

            case "talkable":
                Talk();
                break;
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

    private void Hide()
    {
        Debug.Log("hide");   // do something
    }

    private void Hold(Script_Player player)
    {
        Debug.Log("hold");   // do something
        player.Hold();

    }
}
