using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Button : MonoBehaviour
{
    public Script_Trigger_Door_Manager s_script_trigger_door_manager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player") || !other.gameObject.CompareTag("Ennemy"))
        {
            if(other.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 0)
            {
                ChangeStateDoor();
            }
        }
    }

    private void ChangeStateDoor()
    {
        s_script_trigger_door_manager.ActivateDoor();
    }
}
