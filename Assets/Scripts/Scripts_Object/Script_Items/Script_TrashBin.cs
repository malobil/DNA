using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_TrashBin : Script_IObject
{
    public List<Script_Scriptable_Item> list_object_in;

    public override void Use(GameObject player_target)
    {
       if(list_object_in.Count > 0)
       {
            Debug.Log("PickUp");
            GameObject obj_pick = Instantiate(list_object_in[list_object_in.Count - 1].g_item_prefab,transform.position,Quaternion.identity);
            Script_Player.Instance.Hold(obj_pick);
            list_object_in.Remove(list_object_in[list_object_in.Count - 1]);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8 && list_object_in.Count < 3)
        {
            list_object_in.Add(collision.gameObject.GetComponent<Script_ItemInfo>().item_info);
            Destroy(collision.gameObject);
        }
    }
}
