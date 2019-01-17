using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice ;

[RequireComponent(typeof(Outline))]
public class Script_Distortable : MonoBehaviour
{
    public Script_Scriptable_Item item_scriptable;

    public Script_Scriptable_Item GetScriptableItem()
    {
        return item_scriptable;
    }
}
