using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Guard_View : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Script_Game_Manager.Instance.GameOver();
        }
    }
}
