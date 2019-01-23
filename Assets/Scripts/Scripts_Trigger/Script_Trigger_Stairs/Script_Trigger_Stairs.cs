using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Trigger_Stairs : MonoBehaviour
{
    public int i_idx_stair;
    public string s_scene_to_load;
    public Transform t_apparition_point;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Script_Game_Manager.Instance.ChangeGroundLevel(i_idx_stair, s_scene_to_load);
        }
    }

    public Transform GetApparitionPoint()
    {
        return t_apparition_point;
    }

    public int GetStairIndex()
    {
        return i_idx_stair;
    }

}
