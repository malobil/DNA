using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Trigger_Stairs : MonoBehaviour
{
    public int idx_stair;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            GetStairIndex();
        }
    }

    public void GetStairIndex()
    {
        Script_Game_Manager.Instance.ChangeFloorLevel(idx_stair);
    }
}
