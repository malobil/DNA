using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable/Item", order = 1)]

public class Script_Scriptable_Item : ScriptableObject
{
    public Sprite s_item_sprite;
    public string s_item_CSV_name_key;
    public string s_item_CSV_description_key;
    public int i_item_level;
    public GameObject g_item_prefab;
}
