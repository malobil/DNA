using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Trigger_Door_Detection : Script_ITrigger
{
    public Script_Trigger_Door_Manager s_associate_door_manager;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Ennemy"))
        {
            s_associate_door_manager.VerifyCard(other.gameObject);
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Ennemy"))
        {
           s_associate_door_manager.RemoveCharacterInList(other.gameObject);
        }
    }
}
