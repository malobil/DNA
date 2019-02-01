using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_ItemInfo : MonoBehaviour
{
    public Script_Scriptable_Item item_info;

    private Script_PressurePlate pressure_plate;

    public Script_Scriptable_Item GetItemInfo()
    {
        return item_info;
    }

    public void AddAPressurePlate(Script_PressurePlate plate)
    {
        pressure_plate = plate;
    }

    public void RemovePressurePlate()
    {
        pressure_plate.RemoveAnObjectIn(gameObject);
        pressure_plate = null ;
    }

    private void OnDisable()
    {
        if(pressure_plate != null)
        {
            RemovePressurePlate();
        }
    }
}
