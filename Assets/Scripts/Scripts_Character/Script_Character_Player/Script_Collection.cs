using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Collection : MonoBehaviour
{
    public List<Script_Scriptable_Item> l_item_in_collection;

    public static Script_Collection Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void AddItemToCollection(Script_Scriptable_Item item_to_add)
    {
        if(!l_item_in_collection.Contains(item_to_add))
        {
            l_item_in_collection.Add(item_to_add);
            Script_UI_Manager.Instance.AddTileToCollection(item_to_add);
        }
    }
}
