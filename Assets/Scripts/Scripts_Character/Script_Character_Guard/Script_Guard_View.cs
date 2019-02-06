using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Guard_View : MonoBehaviour
{
    public Script_Guard_Controller associate_guard_controller;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            associate_guard_controller.SeePlayer();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            associate_guard_controller.LostPlayer();
        }
    }
}
