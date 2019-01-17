using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Script_Cinematic_Trigger : MonoBehaviour
{
    public TimelineAsset t_timeline;
    private bool b_player_already_collide = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (b_player_already_collide)
            {
                Script_Cinematic_Controller.Instance.PlayCinematic(t_timeline);
                b_player_already_collide = false;
            }
        }
    }
}
