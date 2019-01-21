using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

[RequireComponent(typeof(Outline))]
public class Script_ISpecialInteraction : MonoBehaviour
{
    public GameObject obj_indication_UI;

    public virtual void SpecialInteraction()
    {
        Debug.Log("USE ME SPECIAL");
    }

    public virtual void EnableSpecialIndication()
    {
        if (obj_indication_UI != null)
        {
            obj_indication_UI.SetActive(true);
        }
    }

    public virtual void DisableSpecialIndication()
    {
        if(obj_indication_UI != null)
        {
            obj_indication_UI.SetActive(false);
        }
    }
}
