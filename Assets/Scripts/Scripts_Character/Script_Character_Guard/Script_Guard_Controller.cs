using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Guard_Controller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Objet") && Script_Player.Instance.ItemIsThrowing())
        {
            MoveToTarget(other.transform);
        }
    }

    public void MoveToTarget(Transform t_target_position)
    {

    }
}
