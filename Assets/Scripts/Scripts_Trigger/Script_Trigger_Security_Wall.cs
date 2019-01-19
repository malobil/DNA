using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Trigger_Security_Wall : Script_ITrigger
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.isTrigger)
        {
            Debug.Log("IN WALL");
            Script_Player.Instance.GoOutOfWall();
        }
    }
}
