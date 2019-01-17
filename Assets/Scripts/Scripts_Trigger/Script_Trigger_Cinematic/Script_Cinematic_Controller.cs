using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Script_Cinematic_Controller : MonoBehaviour
{
    public static Script_Cinematic_Controller Instance { get; private set; }

    private PlayableDirector p_playabledirector;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        p_playabledirector = GetComponent<PlayableDirector>();
    }

    public void PlayCinematic(TimelineAsset t_timeline_to_play)
    {
        p_playabledirector.Play(t_timeline_to_play);
        Script_Game_Manager.Instance.SetTimePause();
    }
}
