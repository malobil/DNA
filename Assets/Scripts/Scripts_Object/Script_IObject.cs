using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_IObject : MonoBehaviour
{
    public bool b_can_be_hold = true;
    public bool b_special_case = false;

    public virtual void Interact()
    {
        Debug.Log("INTERACTION");
    }

    public virtual void UnInteract()
    {
        Debug.Log("UnINTERACTION");
    }

}
