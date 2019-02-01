using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_PressurePlate : MonoBehaviour
{
    public float f_weight_to_activate = 10f;
    public Script_Trigger_Door_Manager associate_door;
    private float f_current_weight;
    private bool b_is_active = false;

    private List<GameObject> obj_in = new List<GameObject>() ;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponentInParent<Script_ItemInfo>())
        {
            AddAnObjectIn(collision.gameObject);
            collision.gameObject.GetComponentInParent<Script_ItemInfo>().AddAPressurePlate(this);
        }

        if(collision.CompareTag("Player"))
        {
            AddAnObjectIn(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponentInParent<Rigidbody2D>())
        {
            RemoveAnObjectIn(collision.gameObject);
        }

        if (collision.CompareTag("Player"))
        {
            RemoveAnObjectIn(collision.gameObject);
        }
    }

    public void AddAnObjectIn(GameObject object_to_add)
    {
        if(!obj_in.Contains(object_to_add))
        {
            obj_in.Add(object_to_add);
        }
        
        foreach(GameObject obj in obj_in)
        {
            f_current_weight += obj.GetComponentInParent<Rigidbody2D>().mass;
        }

        if (f_current_weight >= f_weight_to_activate && !b_is_active)
        {
            Activate();
        }
    }

    public void RemoveAnObjectIn(GameObject object_to_remove)
    {
        if (obj_in.Contains(object_to_remove))
        {
            f_current_weight -= object_to_remove.GetComponentInParent<Rigidbody2D>().mass;
            obj_in.Remove(object_to_remove);
        }

        if (f_current_weight < f_weight_to_activate && b_is_active)
        {
            Disable();
        }
    }

    private void Activate()
    {
        b_is_active = true;
        associate_door.ActivateDoor();
        Debug.Log("Activate");
    }

    private void Disable()
    {
        b_is_active = false;
        associate_door.DisableDoor();
        Debug.Log("Disable");
    }
}
