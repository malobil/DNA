using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Checkpoint : MonoBehaviour
{
    public GameObject g_game_manager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Script_Player.Instance.CallSave();
            Debug.Log("SAVE POSITION");
        }
    }
}
