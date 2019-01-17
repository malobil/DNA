using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Player_Interaction_Trigger : Script_ITrigger
{
    public Script_Player associate_player;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Script_Interactable>() || other.GetComponent<Script_Distortable>())
        {
            associate_player.AddInteractibleObject(other.gameObject);
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Script_Interactable>() || other.GetComponent<Script_Distortable>())
        {
            associate_player.RemoveInteractibleObject(other.gameObject);
        }
    }
}
