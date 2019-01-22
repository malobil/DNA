using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Guard_Controller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(Script_Player.Instance.ItemIsThrowing())
        {
            MoveToTarget();
        }
    }

    public void MoveToTarget()
    {

    }
}
