using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Script_Alter_Wall_Security))]
public class Script_Alterable : MonoBehaviour
{
    public Script_Scriptable_Item item_scriptable;

    private void Start()
    {
        if(GetComponent<Script_Distortable>())
        {
            item_scriptable = GetComponent<Script_Distortable>().GetScriptableItem();
        }
    }

    public Script_Scriptable_Item GetScriptableItem()
    {
        return item_scriptable;
    }
}
