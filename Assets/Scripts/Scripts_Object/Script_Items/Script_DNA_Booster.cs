using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_DNA_Booster : Script_IObject
{
    public override void Use(GameObject player_target)
    {
        Script_Player.Instance.LevelUp();
        Script_Player.Instance.DestroyHoldObject();
    }
}
