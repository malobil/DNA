using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Script_Air_Vent : Script_ISpecialInteraction
{
    public Animator anim_animator ;
    public Script_Air_Vent s_corresponding_air_vent;

    public override void SpecialInteraction()
    {
        anim_animator.SetTrigger("Open");
        Destroy(obj_indication_UI);
        GetComponent<Script_Interactable>().AllowInteraction();

        if(s_corresponding_air_vent != null)
        {
            s_corresponding_air_vent.SpecialInteraction();
        }
    }
}
