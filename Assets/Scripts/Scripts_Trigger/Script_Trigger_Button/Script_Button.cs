﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Button : MonoBehaviour
{
    public Script_Trigger_Door_Manager s_script_trigger_door_manager;
    private bool b_have_been_use = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player") || !other.gameObject.CompareTag("Ennemy"))
        {
            if(other.gameObject.GetComponent<Rigidbody2D>() && other.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 0)
            {
                ChangeStateDoor();
                Debug.Log("Open");
            }
        }
    }

    public void ChangeStateDoor()
    {
        s_script_trigger_door_manager.ActivateDoor();
    }
}
