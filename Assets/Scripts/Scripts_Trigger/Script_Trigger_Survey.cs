using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Trigger_Survey : MonoBehaviour
{
    public string s_link_URL;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Application.OpenURL(s_link_URL);
            Script_Game_Manager.Instance.GameOver();
        }
    }
}
