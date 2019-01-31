using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_PressurePlate : MonoBehaviour
{
    public float f_weight_to_activate = 10f;
    public Script_Trigger_Door_Manager associate_door;
    private float f_current_weight;
    private bool b_is_active = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Script_ItemInfo>())
        {
            AddWeight(collision.gameObject.GetComponentInParent<Rigidbody2D>().mass);
            collision.gameObject.GetComponent<Script_ItemInfo>().AddAPressurePlate(this);
        }

        if(collision.CompareTag("Player"))
        {
            AddWeight(100f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>())
        {
            RemoveWeight(collision.gameObject.GetComponentInParent<Rigidbody2D>().mass);
        }

        if (collision.CompareTag("Player"))
        {
            RemoveWeight(100f);
        }
    }

    public void AddWeight(float f_weight_to_add)
    {
        f_current_weight += f_weight_to_add;

        if (f_current_weight >= f_weight_to_activate && !b_is_active)
        {
            Activate();
        }
    }

    public void RemoveWeight(float f_weight_to_remove)
    {
        f_current_weight -= f_weight_to_remove;

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
