using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class Script_Cinematic_Trigger : MonoBehaviour
{
    private PlayableDirector p_playabledirector;


    void Start()
    {
        p_playabledirector = GetComponent<PlayableDirector>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayCinematic();
        }
    }

    public void PlayCinematic()
    {
        p_playabledirector.Play();
        Script_Game_Manager.Instance.SetTimePause();
        Script_Game_Manager.Instance.EnterInACinematic();
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
