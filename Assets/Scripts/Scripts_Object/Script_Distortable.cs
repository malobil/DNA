using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice ;

[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(Script_ItemInfo))]
public class Script_Distortable : MonoBehaviour
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
