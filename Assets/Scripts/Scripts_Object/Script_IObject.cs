using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_IObject : MonoBehaviour
{
    public virtual void Use(GameObject player_target)
    {

    }

    public void DestroySelf()
    {
        Script_Player.Instance.DestroyHoldObject();
    }
}
