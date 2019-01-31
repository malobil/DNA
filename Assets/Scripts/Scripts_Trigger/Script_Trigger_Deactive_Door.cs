using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Trigger_Deactive_Door : MonoBehaviour
{
    public Script_Trigger_Door_Manager s_script_trigger_door_manager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            s_script_trigger_door_manager.DisableDoor();
            s_script_trigger_door_manager.CloseDoor();
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
