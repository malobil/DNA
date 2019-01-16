using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Script_Cinematic_Trigger : MonoBehaviour
{
    public TimelineAsset t_timeline;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Script_Cinematic_Controller.Instance.PlayCinematic(t_timeline);
        }
    }
}
