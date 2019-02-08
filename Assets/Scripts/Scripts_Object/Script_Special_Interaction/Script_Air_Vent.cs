using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Script_Air_Vent : Script_ISpecialInteraction
{
    public Sprite anim_animator ;
    public GameObject s_corresponding_air_vent;

    public override void SpecialInteraction(Script_Scriptable_Item obj_player_hold_item)
    {
        if(obj_player_hold_item == obj_corresponding_item_info)
        {
            GetComponent<Script_Interactable>().AllowInteraction();

            if (s_corresponding_air_vent != null)
            {
                s_corresponding_air_vent.GetComponent<Script_Interactable>().AllowInteraction();
                s_corresponding_air_vent.GetComponent<SpriteRenderer>().sprite = s_corresponding_air_vent.GetComponent<Script_Air_Vent>().anim_animator;
                Destroy(s_corresponding_air_vent.GetComponent<Script_Air_Vent>());
            }

            GetComponent<SpriteRenderer>().sprite = anim_animator;
            Script_Player.Instance.DestroyHoldObject();
            Destroy(this);
        }
    }

}
