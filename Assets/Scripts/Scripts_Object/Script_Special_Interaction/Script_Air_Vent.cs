using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Script_Air_Vent : Script_ISpecialInteraction
{
    public Animator anim_animator ;

    public override void SpecialInteraction()
    {
        anim_animator.SetTrigger("Open");
        Destroy(obj_indication_UI);
        GetComponent<Script_Interactable>().AllowInteraction();
    }
}
