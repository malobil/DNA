using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

[RequireComponent(typeof(Outline))]
public class Script_ObjectRelatedInteraction : MonoBehaviour
{
    public Script_Scriptable_Item obj_corresponding_item_info ;

    public virtual void SpecialInteraction(Script_Scriptable_Item obj_player_hold_item)
    {

    }

    public virtual void EnableSpecialIndication()
    {

    }

    public virtual void DisableSpecialIndication()
    {

    }

    public bool CheckIfPlayerHaveCorrectItem(Script_Scriptable_Item player_hold_item)
    {
        if(player_hold_item == obj_corresponding_item_info)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
