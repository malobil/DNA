using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_IObject : MonoBehaviour
{
    public virtual void Use(GameObject player_target)
    {
        Attack();
    }

    public virtual void Attack()
    {
        Debug.Log("Attack");
    }

    public void DestroySelf()
    {
        Script_Player.Instance.DestroyHoldObject();
    }
}
