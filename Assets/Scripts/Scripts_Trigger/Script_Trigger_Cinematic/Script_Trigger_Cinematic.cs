using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Script_Trigger_Cinematic : MonoBehaviour
{
    public PlayableDirector p_playable_director;
    public List <TimelineAsset> p_timeline;
    public int i_cinematic_to_play;
    
    public void PlayTimeline()
    {
        //p_playable_director.Play (i_cinematic_to_play);
    }


}
