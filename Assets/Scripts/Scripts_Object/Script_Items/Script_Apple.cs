using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Apple : Script_IObject
{
    public GameObject obj_eaten_apple_prefab;

    public override void Use(GameObject player_target)
    {
        GameObject eaten_Apple = Instantiate(obj_eaten_apple_prefab, transform.position, Quaternion.identity);
        Script_Player.Instance.DestroyHoldObject();
        Script_Player.Instance.Hold(eaten_Apple);
        
    }
}
