using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Script_Alter_Wall_Security))]
[RequireComponent(typeof(Script_ItemInfo))]
public class Script_Alterable : MonoBehaviour
{
    private Script_Scriptable_Item item_scriptable;

    private void Start()
    {
        item_scriptable = GetComponent<Script_ItemInfo>().GetItemInfo();
    }

    public Script_Scriptable_Item GetScriptableItem()
    {
        return item_scriptable;
    }
}
