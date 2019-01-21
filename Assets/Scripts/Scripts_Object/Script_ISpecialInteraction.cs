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
        obj_indication_UI.SetActive(true);
    }

    public virtual void DisableSpecialIndication()
    {
        obj_indication_UI.SetActive(false);
    }
}
