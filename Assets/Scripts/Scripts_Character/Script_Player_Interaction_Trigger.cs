using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Player_Interaction_Trigger : Script_ITrigger
{
    public Script_IPlayer associate_player;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Script_IObject>())
        {
            associate_player.AddInteractibleObject(other.GetComponent<Script_IObject>());
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Script_IObject>())
        {
            associate_player.RemoveInteractibleObject(other.GetComponent<Script_IObject>());
        }
    }
}
