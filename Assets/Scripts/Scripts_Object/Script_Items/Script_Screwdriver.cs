using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Screwdriver : Script_IObject
{
    public override void Use(GameObject player_target)
    {
        if (player_target != null && player_target.GetComponent<Script_Air_Vent>())
        {
            player_target.GetComponent<Script_Air_Vent>().SpecialInteraction();
        }
        else
        {
            Attack();
        }
    }
}
