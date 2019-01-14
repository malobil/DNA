using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Trigger_Door : Script_ITrigger
{
    #region Door

    public GameObject g_door;

    #endregion

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TriggerEvent();
        }
    }

    public override void TriggerEvent()
    {
        Debug.Log("TRIGERRED");
    }
}
